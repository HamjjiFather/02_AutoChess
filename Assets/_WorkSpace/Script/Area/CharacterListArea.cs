using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class CharacterListArea : AreaBase<UnityAction<CharacterModel>>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Transform _contents;
        
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
            
            _listElements.ForEach (element => element.Despawn ());
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.ForEach (characterModel =>
            {
                var element = ObjectPoolingHelper.GetResources<CharacterInfoListElement> (ResourceRoleType.Bundles,
                    ResourcesType.Element, nameof(CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel =characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}