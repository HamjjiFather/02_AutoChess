using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    public class EmployableCharacterInfoListElement : MonoBehaviour, IElementBase<CharacterInfoListElementModel>, IResolveTarget
    {
        #region Fields & Property

        private Context _context => GetComponent<Context> ();

#pragma warning disable CS0649

        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private Animator _characterAnimator;

#pragma warning restore CS0649

        public CharacterInfoListElementModel ElementData { get; set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterInfoListElementModel characterInfoListElementModel)
        {
            _context.Resolve (true);
            ElementData = characterInfoListElementModel;
            _characterImage.Value = characterInfoListElementModel.CharacterData.IconImageResources;
            _characterAnimator.runtimeAnimatorController = ElementData.CharacterData.CharacterAnimatorResources;
            GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
            GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
        }


        public void UpdateElement (CharacterData characterData)
        {
            ElementData.CharacterData = characterData;
            _characterImage.Value = characterData.IconImageResources;
            _characterAnimator.runtimeAnimatorController = ElementData.CharacterData.CharacterAnimatorResources;
            GetComponent<RectTransform> ().sizeDelta = Vector2.zero;
            GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;
        }

        #endregion
    }
}