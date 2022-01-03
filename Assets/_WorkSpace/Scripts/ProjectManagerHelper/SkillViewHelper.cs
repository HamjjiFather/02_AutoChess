using System.Linq;
using KKSFramework;
using Helper;
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

        public string ToSkillDescriptionString (CharacterData characterData)
        {
            var originDesc = LocalizeHelper.FromDescription (characterData.SkillData.Desc);

            if (originDesc == null || !originDesc.Contains ("#VALUE")) return characterData.SkillData.Desc;

            var valueString = ToValueString (characterData, characterData.SkillData);
            return originDesc.Replace ("#VALUE", valueString);
        }


        /// <summary>
        /// 스킬 값 설명란 출력.
        /// 값 + 계수 추가치 (계수 계수 능력치 이름)
        /// ex) 50 + 25 (0.5 공격력)
        /// </summary>
        private string ToValueString (CharacterData characterData, Skill skillData)
        {
            return string.Empty;
            
            // var status =
            //     TableDataManager.Instance.StatusDict.Values.First (x =>
            //         x.StatusType.Equals (skillData.RefSkillStatusType));
            // var translatedName = LocalizeHelper.FromName (status.NameKey);
            // var selfRef = skillData.RefSkillValueTarget == RefSkillValueTarget.Self;
            // var color = _commonColorSetting.GetStatusColor (skillData.RefSkillStatusType);
            //
            // var addedValueString = selfRef
            //     ? $"{characterData.GetTotalStatusValue (skillData.RefSkillStatusType) * skillData.RefSkillValueAmount:F0}<color={color.ToRGBHex ()}>({skillData.RefSkillValueAmount:F2} {translatedName})</color>"
            //     : string.Empty;
            // return $"{skillData.SkillValue} + {addedValueString}";
        }

        #endregion


        #region EventMethods

        #endregion
    }
}