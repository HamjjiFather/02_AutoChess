using System.Collections.Generic;
using System.Linq;
using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class EmployableCharacterListArea : AreaBase<UnityAction<CharacterData>>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Transform[] _employCharacterPoint;

        [Inject]
        private CharacterManager _characterViewmodel;

#pragma warning restore CS0649

        private readonly List<EmployableCharacterInfoListElement> _employableCharacterElements =
            new List<EmployableCharacterInfoListElement> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (UnityAction<CharacterData> areaData)
        {
            if (!_characterViewmodel.IsNewEmployment) return;
            _characterViewmodel.IsNewEmployment = false;

            _employableCharacterElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _employableCharacterElements.Clear ();
            _employCharacterPoint.Foreach (point => point.GetChild (0).gameObject.SetActive (true));

            _characterViewmodel.AllEmployableCharacterModels.Foreach ((characterModel, i) =>
            {
                var element = ObjectPoolingHelper.Spawn<EmployableCharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (), ResourcesType.Element.ToString (),
                    nameof (EmployableCharacterInfoListElement), _employCharacterPoint[i]);

                _employCharacterPoint[i].GetChild (0).gameObject.SetActive (false);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterData = characterModel,
                    ElementClick = areaData
                });

                _employableCharacterElements.Add (element);
            });

            if (!_employableCharacterElements.Any ())
                return;
            
            var firstElementData = _employableCharacterElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterData);
        }


        public void UpdateArea ()
        {
            _employCharacterPoint.Foreach (point => point.GetChild (0).gameObject.SetActive (true));

            if (_characterViewmodel.AllEmployableCharacterModels.Any ())
            {
                _characterViewmodel.AllEmployableCharacterModels.Foreach ((characterModel, i) =>
                {
                    _employCharacterPoint[i].GetChild (0).gameObject.SetActive (false);
                    _employableCharacterElements[i].UpdateElement (characterModel);
                });

                var lastElement = _employableCharacterElements.Last ();
                _employableCharacterElements.Remove (lastElement);
                ObjectPoolingHelper.Despawn (lastElement.transform);
            }
            else
            {
                _employableCharacterElements.Foreach (element =>
                {
                    ObjectPoolingHelper.Despawn (element.transform);
                });
                _employableCharacterElements.Clear ();
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}