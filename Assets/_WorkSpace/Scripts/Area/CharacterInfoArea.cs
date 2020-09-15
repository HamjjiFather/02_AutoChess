using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using MasterData;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CharacterInfoArea : AreaBase<CharacterModel>, IResolveTarget
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
        private StarGradeArea _starGradeArea;

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
                _characterNameText.Value = LocalizeHelper.FromName (areaData.CharacterData.Name);
                _characterImage.Value = ResourcesLoadHelper.LoadResource<Sprite> (ResourceRoleType._Image,
                    ResourcesType.Monster, areaData.CharacterData.SpriteResName);
                _characterAnimator.runtimeAnimatorController =
                    ResourcesLoadHelper.LoadResource<RuntimeAnimatorController> (ResourceRoleType._Animation,
                        areaData.CharacterData.AnimatorResName);
            }

            void ChangeCharacterInfo (CharacterModel characterModel)
            {
                var levelData = characterModel.GetLevelData ();
                _levelText.Value = $"Lv. {levelData.LevelString}";
                _expGageElement.SetValue (characterModel.NowExp (), levelData.ReqExp);
                _starGradeArea.SetArea (characterModel.StarGrade);

                _baseStatusElements[0].SetCharacterElement (StatusType.Health, characterModel);
                _baseStatusElements[1].SetCharacterElement (StatusType.Attack, characterModel);
                _baseStatusElements[2].SetCharacterElement (StatusType.AbilityPoint, characterModel);
                _baseStatusElements[3].SetCharacterElement (StatusType.Defense, characterModel);
                _baseStatusElements[4].SetCharacterElement (StatusType.AttackSpeed, characterModel);
                
                _skillInfoArea.SetArea (characterModel);
                
                SetEquipment ();
                
                void SetEquipment ()
                {
                    characterModel.EquipmentStatusModel.EquipmentModels.ForEach ((model, index) =>
                    {
                        _equipmentInfoElement[index].SetElement (model);
                    });
                }
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}