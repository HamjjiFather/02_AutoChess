using System.Linq;
using KKSFramework;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using MasterData;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class GraveyardCharacterInfoArea : AreaBase<CharacterData>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private GameObject _noExistObj;

        [Resolver]
        private Property<string> _characterDeathDate;

        [Resolver]
        private StatusElement[] _baseStatusElements;

        [Resolver]
        private Image _characterTypeIcon;

        [Resolver]
        private Property<string> _characterNameText;

        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private SkillInfoArea _skillInfoArea;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        public CharacterData AreaData;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterData areaData)
        {
            var existDeathCharacter = _characterViewmodel.AllDeathCharacterModels.Any ();
            _noExistObj.SetActive (!existDeathCharacter);
            if (!existDeathCharacter)
            {
                return;
            }

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
            }

            void ChangeCharacterInfo (CharacterData characterModel)
            {
                // _starGradeArea.SetArea (characterModel.StarGrade);
                _skillInfoArea.SetArea (characterModel);

                SetEquipment ();

                void SetEquipment ()
                {
                    // characterModel.EquipmentStatusModel.EquipmentModels.ForEach ((model, index) => { });
                }
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}