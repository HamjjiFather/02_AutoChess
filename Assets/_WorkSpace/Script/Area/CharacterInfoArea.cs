using BaseFrame;
using KKSFramework.GameSystem.GlobalText;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoArea : MonoBehaviour
    {
        #region Fields & Property
        
        public Image characterTypeIcon;
                                                  
        public Text characterNameText;
                                                  
        public StarGradeArea starGradeArea;
                                                  
        public Image characterImage;
                                                  
        public Animator characterAnimator;
                                                  
        public GageElement hpGageElement;
                                                  
        public Text LevelText;
                                                  
        public GageElement expGageElement;
                                                  
        public Image skillIconImage;
                                                  
        public Text skillLevelText;
                                                  
        public Text skillNameText;
                                                  
        public Text skillDescText;
                                                  
        public StatusElement[] baseStatusElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public void SetCharacter (CharacterModel characterModel)
        {
            characterNameText.GetTranslatedString (characterModel.CharacterData.Name);
            starGradeArea.SetGrade (characterModel.StarGrade);
            characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, characterModel.CharacterData.SpriteResName);
            characterAnimator.runtimeAnimatorController =
                ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                    characterModel.CharacterData.AnimatorResName);

            var healthValue = (int)characterModel.GetTotalStatus (StatusType.Health);
            hpGageElement.SetValue (healthValue, healthValue);

            var levelData = characterModel.GetLevelData ();
            LevelText.text = $"Lv. {levelData.LevelString}";
            
            expGageElement.SetValue (characterModel.NowExp (), levelData.ReqExp);

            baseStatusElements[0].SetElement (characterModel.GetBaseStatusModel (StatusType.Health));
            baseStatusElements[1].SetElement (characterModel.GetBaseStatusModel (StatusType.Attack));
            baseStatusElements[2].SetElement (characterModel.GetBaseStatusModel (StatusType.Defense));
            baseStatusElements[3].SetElement (characterModel.GetBaseStatusModel (StatusType.AtSpd));
        }

        #endregion


        #region EventMethods

        #endregion
    }
}