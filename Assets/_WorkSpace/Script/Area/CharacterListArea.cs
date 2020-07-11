using System.Collections.Generic;
using System.Linq;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class CharacterListArea : AreaBase<UnityAction<CharacterModel>>
    {
        #region Fields & Property

        public Transform contents;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private readonly List<CharacterInfoListElement> _listElements = new List<CharacterInfoListElement> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (UnityAction<CharacterModel> areaData)
        {
            if (!_characterViewmodel.IsDataChanged) return;
            _characterViewmodel.IsDataChanged = false;
            
            _listElements.Foreach (element => element.PoolingObject ());
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.GetResources<CharacterInfoListElement> (ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof(CharacterInfoListElement), contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel =characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            _listElements.First ().elementButton.onClick.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}