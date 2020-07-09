using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class SkillValueModel
    {
        /// <summary>
        /// 적용 전 계산된 스킬 값.
        /// </summary>
        public float PreApplyValue;

        /// <summary>
        /// 적용 후 계산된 스킬 값.
        /// </summary>
        public float AppliedValue;

        public SkillValueModel (float preApplyValue)
        {
            PreApplyValue = preApplyValue;
            AppliedValue = preApplyValue;
        }

        public void SetPositiveNegativeValue (bool isPositive)
        {
            PreApplyValue = isPositive ? PreApplyValue : -PreApplyValue;
        }
        
        public void SetAppliedValue (float appliedValue)
        {
            AppliedValue = appliedValue;
        }
    }
    
    public class SkillModel : ModelBase
    {
        #region Fields & Property

        public CharacterModel UseCharacterModel;
        
        public PositionModel TargetPosition;
        
        public Skill SkillData;

        public DamageType DamageType = DamageType.Damage;

        public readonly List<CharacterModel> TargetCharacters = new List<CharacterModel> ();

        /// <summary>
        /// 스킬을 적용하기 전 계산된 스킬 적용값.
        /// </summary>
        public readonly List<SkillValueModel> SkillValueModels = new List<SkillValueModel> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
    }
}