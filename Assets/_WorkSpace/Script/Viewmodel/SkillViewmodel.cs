using AutoChess.Helper;
using KKSFramework.DesignPattern;
using KKSFramework.GameSystem.GlobalText;
using Zenject;

namespace AutoChess
{
    public class SkillViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private StatusViewmodel _statusViewmodel;

#pragma warning restore CS0649

        #endregion


        public override void Initialize ()
        {
        }


        #region Methods

        public string ToSkillDescriptionString (CharacterModel characterModel)
        {
            var originDesc = GlobalTextHelper.GetTranslatedString (characterModel.SkillData.Desc);

            if (!originDesc.Contains ("#VALUE")) return originDesc;

            var valueString = ToValueString (characterModel, characterModel.SkillData);
            return originDesc.Replace ("#VALUE", valueString);
        }


        private string ToValueString (CharacterModel characterModel, Skill skillData)
        {
            var statusName = TableDataHelper.GetStatus (skillData.RefSkillStatusType).NameKey;
            var translatedName = GlobalTextHelper.GetTranslatedString (statusName);
            var selfRef = skillData.RefSkillValueTarget == RefSkillValueTarget.Self;
            var addedValueString = selfRef
                ? $"{characterModel.GetTotalStatusValue (skillData.RefSkillStatusType) * skillData.RefSkillValueAmount:F0} ({skillData.RefSkillValueAmount:F2}{translatedName})"
                : string.Empty;
            return $"{skillData.SkillValue} + {addedValueString}";
        }

        #endregion


        #region EventMethods

        #endregion
    }
}