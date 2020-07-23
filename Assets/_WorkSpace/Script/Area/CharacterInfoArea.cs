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

        public Text LevelText;

        public GageElement expGageElement;

        public SkillInfoArea skillInfoArea;

        public EquipmentInfoElement[] equipmentInfoElement;

        public StatusElement[] baseStatusElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        public CharacterModel AreaData;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterModel areaData)
        {
            AreaData?.DisposeSubscribe ();

            AreaData = areaData;
            AreaData.CharacterInfoSubscribe (ChangeCharacterInfo);

            SetFixedCharacterInfo ();
            ChangeCharacterInfo (areaData);

            void SetFixedCharacterInfo ()
            {
                characterNameText.GetTranslatedString (areaData.CharacterData.Name);
                characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                    ResourcesType.Monster, areaData.CharacterData.SpriteResName);
                characterAnimator.runtimeAnimatorController =
                    ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                        areaData.CharacterData.AnimatorResName);
            }

            void ChangeCharacterInfo (CharacterModel characterModel)
            {
                var levelData = characterModel.GetLevelData ();
                LevelText.text = $"Lv. {levelData.LevelString}";
                expGageElement.SetValue (characterModel.NowExp (), levelData.ReqExp);
                starGradeArea.SetArea (characterModel.StarGrade);

                baseStatusElements[0].SetCharacterElement (StatusType.Health, characterModel);
                baseStatusElements[1].SetCharacterElement (StatusType.Attack, characterModel);
                baseStatusElements[2].SetCharacterElement (StatusType.AbilityPoint, characterModel);
                baseStatusElements[3].SetCharacterElement (StatusType.Defense, characterModel);
                baseStatusElements[4].SetCharacterElement (StatusType.AttackSpeed, characterModel);
                
                skillInfoArea.SetArea (characterModel);
                
                SetEquipment ();
                
                void SetEquipment ()
                {
                    characterModel.EquipmentStatusModel.EquipmentModels.Foreach ((model, index) =>
                    {
                        equipmentInfoElement[index].SetElement (model);
                    });
                }
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}