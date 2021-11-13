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
            _listElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.Spawn<CharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }
        
        
        public void SetArea (UnityAction<CharacterModel> areaData, bool firstElementInvoke, ref bool changedData)
        {
            if (!changedData) return;
            changedData = false;

            _listElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.Spawn<CharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            if (!firstElementInvoke)
                return;
            
            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }
        
        
        public void SetAreaForced (UnityAction<CharacterModel> areaData, bool firstElementInvoke)
        {
            _listElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.Spawn<CharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            if (!firstElementInvoke)
                return;
            
            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }
        
        
        public void SetArea (UnityAction<CharacterModel> areaData, List<CharacterModel> characterModels, bool firstElementInvoke, ref bool changedData)
        {
            if (!changedData) return;
            changedData = false;

            _listElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            characterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.Spawn<CharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            if (!firstElementInvoke)
                return;
            
            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }
        
        
        public void SetAreaForced (UnityAction<CharacterModel> areaData, List<CharacterModel> characterModels, bool firstElementInvoke)
        {
            _listElements.Foreach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            characterModels.Foreach (characterModel =>
            {
                var element = ObjectPoolingHelper.Spawn<CharacterInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (CharacterInfoListElement), _contents);

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel = characterModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            if (!firstElementInvoke)
                return;
            
            var firstElementData = _listElements.First ().ElementData;
            firstElementData.ElementClick.Invoke (firstElementData.CharacterModel);
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}