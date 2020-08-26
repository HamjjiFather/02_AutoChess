using KKSFramework.DataBind;
using KKSFramework.Localization;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoListElementModel
    {
        public CharacterModel CharacterModel;

        public UnityAction<CharacterModel> ElementClick;
    }

    public class CharacterInfoListElement : PooingComponent, IElementBase<CharacterInfoListElementModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver ("StarGradeArea")]
        private StarGradeArea _starGradeArea;

        [Resolver ("CharacterImage")]
        private Image _characterImage;

        [Resolver ("CharacterNameText")]
        private Text _characterNameText;

        [Resolver ("ElementButton")]
        private Button _elementButton;

        [Resolver ("InBattleObject")]
        private GameObject _inBattleObj;

#pragma warning restore CS0649

        public CharacterInfoListElementModel ElementData { get; set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterInfoListElementModel characterInfoListElementModel)
        {
            ElementData = characterInfoListElementModel;
            _starGradeArea.SetArea (characterInfoListElementModel.CharacterModel.StarGrade);
            _characterImage.sprite = characterInfoListElementModel.CharacterModel.IconImageResources;
            _characterNameText.GetTranslatedString (characterInfoListElementModel.CharacterModel.CharacterData.Name);
            _elementButton.onClick.RemoveAllListeners ();
            _elementButton.onClick.AddListener (() =>
            {
                characterInfoListElementModel.ElementClick.Invoke (characterInfoListElementModel.CharacterModel);
            });
        }

        #endregion
    }
}