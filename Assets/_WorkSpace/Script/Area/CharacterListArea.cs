using System.Collections.Generic;
using System.Linq;
using KKSFramework.Object;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class CharacterListArea : MonoBehaviour
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

        public void SetChracterList (UnityAction<CharacterModel> clickElement)
        {
            if (!_characterViewmodel.IsDataChanged) return;
            _characterViewmodel.IsDataChanged = false;
            
            _listElements.Foreach (element => element.PoolingObject ());
            _listElements.Clear ();

            _characterViewmodel.AllCharacterModels.Foreach (characterModel =>
            {
                CharacterInfoListElement element;

                if (ObjectPoolingManager.Instance.IsExistPooledObject (PoolingObjectType.Prefab, nameof(CharacterInfoListElement))
                )
                {
                    element = ObjectPoolingManager.Instance.ReturnLoadResources<CharacterInfoListElement> (
                        PoolingObjectType.Prefab, nameof(CharacterInfoListElement), contents);
                }
                else
                {
                    var res = ResourcesLoadHelper.GetResources<CharacterInfoListElement> (ResourceRoleType._Prefab,
                        ResourcesType.Element, nameof(CharacterInfoListElement));
                    element = res.InstantiateObject<CharacterInfoListElement> ();
                    element.transform.SetParent (contents);
                    element.GetComponent<RectTransform> ().SetInstantiateTransform ();
                }

                element.SetElement (new CharacterInfoListElementModel
                {
                    CharacterModel =characterModel,
                    ElementClick = clickElement
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