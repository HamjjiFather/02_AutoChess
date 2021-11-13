using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using MasterData;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class EmployCharacterInfoArea : AreaBase<CharacterModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private GameObject _noExistCharacterObj;

        [Resolver]
        private GameObject _existCharacterObj;
        
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
        private SkillInfoArea _skillInfoArea;

#pragma warning restore CS0649

        public CharacterModel AreaData;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterModel areaData)
        {
            _noExistCharacterObj.SetActive (false);
            _existCharacterObj.SetActive (true);
            
            AreaData?.DisposeSubscribe ();

            AreaData = areaData;
            AreaData.CharacterInfoSubscribe (ChangeCharacterInfo);

            SetFixedCharacterInfo ();
            ChangeCharacterInfo (areaData);

            void SetFixedCharacterInfo ()
            {
                _characterNameText.Value = LocalizeHelper.FromName (areaData.CharacterData.Name);
                _characterImage.Value = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                    ResourcesType.Monster, areaData.CharacterData.SpriteResName);
                _characterAnimator.runtimeAnimatorController =
                    ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                        areaData.CharacterData.AnimatorResName);
            }

            void ChangeCharacterInfo (CharacterModel characterModel)
            {
                _starGradeArea.SetArea (characterModel.StarGrade);

                _baseStatusElements[0].SetCharacterElement (StatusType.Health, characterModel);
                _baseStatusElements[1].SetCharacterElement (StatusType.Attack, characterModel);
                _baseStatusElements[2].SetCharacterElement (StatusType.AbilityPoint, characterModel);
                _baseStatusElements[3].SetCharacterElement (StatusType.Defense, characterModel);
                _baseStatusElements[4].SetCharacterElement (StatusType.AttackSpeed, characterModel);
                
                _skillInfoArea.SetArea (characterModel);
            }
        }


        public void EmptyArea ()
        {
            _noExistCharacterObj.SetActive (true);
            _existCharacterObj.SetActive (false);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}