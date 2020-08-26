using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Localization;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoArea : AreaBase<CharacterModel>
    {
        #region Fields & Property

        public EquipmentInfoElement[] equipmentInfoElement;

        public StatusElement[] baseStatusElements;

#pragma warning disable CS0649
        
        [Resolver("TypeImage")]
        private Image _characterTypeIcon;

        [Resolver("NameText")]
        private Text _characterNameText;

        [Resolver("StarGradeArea")]
        private StarGradeArea _starGradeArea;

        [Resolver("CharacterImage")]
        private Image _characterImage;

        [Resolver("CharacterAnimator")]
        private Animator _characterAnimator;

        [Resolver("LevelText")]
        private Text _levelText;

        [Resolver("ExpGageElement")]
        private GageElement _expGageElement;

        [Resolver("SkillInfoArea")]
        private SkillInfoArea _skillInfoArea;

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
                _characterNameText.GetTranslatedString (areaData.CharacterData.Name);
                _characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                    ResourcesType.Monster, areaData.CharacterData.SpriteResName);
                _characterAnimator.runtimeAnimatorController =
                    ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                        areaData.CharacterData.AnimatorResName);
            }

            void ChangeCharacterInfo (CharacterModel characterModel)
            {
                var levelData = characterModel.GetLevelData ();
                _levelText.text = $"Lv. {levelData.LevelString}";
                _expGageElement.SetValue (characterModel.NowExp (), levelData.ReqExp);
                _starGradeArea.SetArea (characterModel.StarGrade);

                baseStatusElements[0].SetCharacterElement (StatusType.Health, characterModel);
                baseStatusElements[1].SetCharacterElement (StatusType.Attack, characterModel);
                baseStatusElements[2].SetCharacterElement (StatusType.AbilityPoint, characterModel);
                baseStatusElements[3].SetCharacterElement (StatusType.Defense, characterModel);
                baseStatusElements[4].SetCharacterElement (StatusType.AttackSpeed, characterModel);
                
                _skillInfoArea.SetArea (characterModel);
                
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