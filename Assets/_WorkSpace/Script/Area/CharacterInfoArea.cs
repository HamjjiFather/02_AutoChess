using BaseFrame;
using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoArea : AreaBase<CharacterModel>
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

        public SkillInfoArea skillInfoArea;
                                                  
        public StatusElement[] baseStatusElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterModel areaData)
        {
            characterNameText.GetTranslatedString (areaData.CharacterData.Name);
            starGradeArea.SetArea (areaData.StarGrade);
            characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, areaData.CharacterData.SpriteResName);
            characterAnimator.runtimeAnimatorController =
                ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                    areaData.CharacterData.AnimatorResName);

            var healthValue = (int)areaData.GetTotalStatusValue (StatusType.Health);
            hpGageElement.SetValue (healthValue, healthValue);

            var levelData = areaData.GetLevelData ();
            LevelText.text = $"Lv. {levelData.LevelString}";
            
            expGageElement.SetValue (areaData.NowExp (), levelData.ReqExp);

            baseStatusElements[0].SetElement (areaData.GetBaseStatusModel (StatusType.Health));
            baseStatusElements[1].SetElement (areaData.GetBaseStatusModel (StatusType.Attack));
            baseStatusElements[2].SetElement (areaData.GetBaseStatusModel (StatusType.AbilityPoint));
            baseStatusElements[3].SetElement (areaData.GetBaseStatusModel (StatusType.Defense));
            baseStatusElements[4].SetElement (areaData.GetBaseStatusModel (StatusType.AttackSpeed));
        }
        
        #endregion


        #region EventMethods

        #endregion


        
    }
}