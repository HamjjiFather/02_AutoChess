using System.Collections.Generic;
using KKSFramework.DesignPattern;
using MasterData;

namespace AutoChess
{
    public class SkillValueModel
    {
        /// <summary>
        /// 전 처리 스킬 값.
        /// </summary>
        public float PreApplyValue;

        /// <summary>
        /// 후 처리 스킬 값.
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

        public CharacterData UseCharacterData;
        
        public PositionModel TargetPosition;
        
        public CharacterSkill SkillData;

        public bool ApplyBullet;

        public DamageType DamageType = DamageType.Damage;

        public readonly List<CharacterData> TargetCharacters = new List<CharacterData> ();

        /// <summary>
        /// 스킬을 적용하기 전 계산된 스킬 적용값.
        /// </summary>
        public readonly List<SkillValueModel> SkillValueModels = new List<SkillValueModel> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
    }
}