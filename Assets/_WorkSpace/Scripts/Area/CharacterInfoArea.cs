using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using MasterData;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoArea : AreaBase<CharacterData>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private EquipmentInfoElement[] _equipmentInfoElement;

        [Resolver]
        private StatusElement[] _baseStatusElements;

        [Resolver]
        private Image _characterTypeIcon;

        [Resolver]
        private Property<string> _characterNameText;

        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private Animator _characterAnimator;

        [Resolver]
        private Property<string> _levelText;

        [Resolver]
        private GageElement _expGageElement;

        [Resolver]
        private SkillInfoArea _skillInfoArea;

#pragma warning restore CS0649

        public CharacterData AreaData;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterData areaData)
        {
            AreaData?.DisposeSubscribe ();

            AreaData = areaData;
            AreaData.CharacterInfoSubscribe (ChangeCharacterInfo);

            SetFixedCharacterInfo ();
            ChangeCharacterInfo (areaData);

            void SetFixedCharacterInfo ()
            {
                _characterNameText.Value = LocalizeHelper.FromName (areaData.CharacterTable.Name);
                _characterImage.Value = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                    ResourcesType.Monster, areaData.CharacterTable.SpriteResName);
                _characterAnimator.runtimeAnimatorController =
                    ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                        areaData.CharacterTable.AnimatorResName);
            }

            void ChangeCharacterInfo (CharacterData characterModel)
            {
                var levelData = characterModel.GetLevelData ();
                _levelText.Value = $"Lv. {levelData.LevelString}";
                _expGageElement.SetValue (characterModel.NowExp (), levelData.ReqExp);
                // _starGradeArea.SetArea (characterModel.StarGrade);

                // _baseStatusElements[0].SetCharacterElement (StatusType.Health, characterModel);
                // _baseStatusElements[1].SetCharacterElement (StatusType.Attack, characterModel);
                // _baseStatusElements[2].SetCharacterElement (StatusType.AbilityPoint, characterModel);
                // _baseStatusElements[3].SetCharacterElement (StatusType.Defense, characterModel);
                // _baseStatusElements[4].SetCharacterElement (StatusType.AttackSpeed, characterModel);
                //
                // _skillInfoArea.SetArea (characterModel);
                //
                // SetEquipment ();

                // void SetEquipment ()
                // {
                //     characterModel.EquipmentStatusModel.EquipmentModels.ZipForEach (_equipmentInfoElement,
                //         (model, element) => { element.SetElement (model); });
                // }
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}