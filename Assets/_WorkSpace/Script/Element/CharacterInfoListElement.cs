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
    
    public class CharacterInfoListElement : PooingComponent, IElementBase<CharacterInfoListElementModel>
    {
        #region Fields & Property

        public StarGradeArea starGradeArea;

        public Image characterImage;

        public Text characterNameText;

        public Button elementButton;

        public GameObject inBattleObj;

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        public CharacterInfoListElementModel ElementData { get; set; }

        #endregion


        #region UnityMethods

        #endregion


        #region Methods


        public void SetElement (CharacterInfoListElementModel characterInfoListElementModel)
        {
            starGradeArea.SetArea (characterInfoListElementModel.CharacterModel.StarGrade);
            characterImage.sprite = characterInfoListElementModel.CharacterModel.IconImageResources;
            characterNameText.GetTranslatedString (characterInfoListElementModel.CharacterModel.CharacterData.Name);
            
            elementButton.onClick.RemoveAllListeners ();
            elementButton.onClick.AddListener (() =>
            {
                characterInfoListElementModel.ElementClick.Invoke (characterInfoListElementModel.CharacterModel);
            });
        }

        #endregion
    }
}