using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class EmployableCharacterListArea : AreaBase<UnityAction<CharacterModel>>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Transform[] _employCharacterPoint;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private readonly List<EmployableCharacterInfoListElement> _employableCharacterElements =
            new List<EmployableCharacterInfoListElement> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (UnityAction<CharacterModel> areaData)
        {
            if (!_characterViewmodel.IsNewEmployment) return;
            _characterViewmodel.IsNewEmployment = false;

            _employableCharacterElements.ForEach (element => ObjectPoolingHelper.Despawn (element.transform));
            _employableCharacterElements.Clear ();
            _employCharacterPoint.ForEach (point => point.GetChild (0).gameObject.SetActive (true));

            _characterViewmodel.AllEmployableCharacterModels.ForEach ((characterModel, i) =>
            {
                var element = ObjectPoolingHelper.Spawn<EmployableCharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (), ResourcesType.Element.ToString (),
                    nameof (EmployableCharacterInfoListElement), _employCharacterPoint[i]);

                _employCharacterPoint[i].GetChild (0).gameObject.SetActive (false);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });

                _employableCharacterElements.Add (element);
            });

            var firstElementData = _employableCharacterElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }


        public void UpdateArea ()
        {
            _employCharacterPoint.ForEach (point => point.GetChild (0).gameObject.SetActive (true));
            
            _characterViewmodel.AllEmployableCharacterModels.ForEach ((characterModel, i) =>
            {
                _employCharacterPoint[i].GetChild (0).gameObject.SetActive (false);
                _employableCharacterElements[i].UpdateElement (characterModel);
            });

            var lastElement = _employableCharacterElements.Last ();
            _employableCharacterElements.Remove (lastElement);
            ObjectPoolingHelper.Despawn (lastElement.transform);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}