using AutoChess.Helper;
using KKSFramework.GameSystem.GlobalText;
using Zenject;

namespace AutoChess
{
    public class SkillViewHelper : Singleton<SkillViewHelper>
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly CommonColorSetting _commonColorSetting;

        #endregion


        public SkillViewHelper ()
        {
            _commonColorSetting = ProjectContext.Instance.Container.Resolve<CommonColorSetting> ();
        }


        #region UnityMethods

        #endregion


        #region Methods

        public string ToSkillDescriptionString (CharacterModel characterModel)
        {
            var originDesc = GlobalTextHelper.GetTranslatedString (characterModel.SkillData.Desc);

            if (!originDesc.Contains ("#VALUE")) return originDesc;

            var valueString = ToValueString (characterModel, characterModel.SkillData);
            return originDesc.Replace ("#VALUE", valueString);
        }


        /// <summary>
        /// 스킬 값 설명란 출력.
        /// 값 + 계수 추가치 (계수 계수 능력치 이름)
        /// ex) 50 + 25(0.5 공격력)
        /// </summary>
        /// <param name="characterModel"></param>
        /// <param name="skillData"></param>
        /// <returns></returns>
        private string ToValueString (CharacterModel characterModel, Skill skillData)
        {
            var statusName = TableDataHelper.GetStatus (skillData.RefSkillStatusType).NameKey;
            var translatedName = GlobalTextHelper.GetTranslatedString (statusName);
            var selfRef = skillData.RefSkillValueTarget == RefSkillValueTarget.Self;
            var color = _commonColorSetting.GetStatusColor (skillData.RefSkillStatusType);

            var addedValueString = selfRef
                ? $"{characterModel.GetTotalStatusValue (skillData.RefSkillStatusType) * skillData.RefSkillValueAmount:F0}<color={color.ToRGBHex ()}>({skillData.RefSkillValueAmount:F2} {translatedName})</color>"
                : string.Empty;
            return $"{skillData.SkillValue} + {addedValueString}";
        }

        #endregion


        #region EventMethods

        #endregion
    }
}