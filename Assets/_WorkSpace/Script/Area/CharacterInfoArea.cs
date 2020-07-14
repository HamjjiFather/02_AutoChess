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

        public Image equipmentImage;

        public StarGradeArea equipmentStarGradeArea;

        public GameObject equipmentInfoObj;

        public GameObject emptyEquipmentObj;

        public Text LevelText;

        public GageElement expGageElement;

        public SkillInfoArea skillInfoArea;

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
                var levelData = areaData.GetLevelData ();
                LevelText.text = $"Lv. {levelData.LevelString}";
                expGageElement.SetValue (areaData.NowExp (), levelData.ReqExp);
                starGradeArea.SetArea (characterModel.StarGrade);

                baseStatusElements[0].SetCharacterElement (characterModel.GetBaseStatusModel (StatusType.Health),
                    characterModel.GetEquipmnetStatusModel (StatusType.Health));
                baseStatusElements[1].SetCharacterElement (characterModel.GetBaseStatusModel (StatusType.Attack),
                    characterModel.GetEquipmnetStatusModel (StatusType.Attack));
                baseStatusElements[2].SetCharacterElement (characterModel.GetBaseStatusModel (StatusType.AbilityPoint),
                    characterModel.GetEquipmnetStatusModel (StatusType.AbilityPoint));
                baseStatusElements[3].SetCharacterElement (characterModel.GetBaseStatusModel (StatusType.Defense),
                    characterModel.GetEquipmnetStatusModel (StatusType.Defense));
                baseStatusElements[4].SetCharacterElement (characterModel.GetBaseStatusModel (StatusType.AttackSpeed),
                    characterModel.GetEquipmnetStatusModel (StatusType.AttackSpeed));
                
                skillInfoArea.SetArea (characterModel);
                
                SetEquipment ();
                
                void SetEquipment ()
                {
                    var isExist = areaData.IsExistEquipment ();
                    emptyEquipmentObj.SetActive (!isExist);
                    equipmentInfoObj.SetActive (isExist);

                    if (isExist)
                    {
                        var equipmentModel = areaData.EquipmentModel;
                        starGradeArea.SetArea (equipmentModel.StarGrade);
                        equipmentImage.sprite = equipmentModel.IconImageResources;
                    }
                }
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}