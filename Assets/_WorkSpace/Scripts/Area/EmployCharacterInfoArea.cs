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
    public class EmployCharacterInfoArea : AreaBase<CharacterData>, IResolveTarget
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
        private Property<Sprite> _characterImage;

        [Resolver]
        private Animator _characterAnimator;

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
            _noExistCharacterObj.SetActive (false);
            _existCharacterObj.SetActive (true);
            
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
                // _starGradeArea.SetArea (characterModel.StarGrade);
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