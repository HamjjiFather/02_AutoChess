namespace AutoChess
{
    /// <summary>
    /// 주요 능력치.
    /// </summary>
    public enum PrimeAbilityType
    {
        /// <summary>
        /// 신체.
        /// </summary>
        Body,
        
        /// <summary>
        /// 기술.
        /// </summary>
        Skill,
        
        /// <summary>
        /// 정신.
        /// </summary>
        Meltality,
        
        /// <summary>
        /// 지혜.
        /// </summary>
        Wisdom,
        
        /// <summary>
        /// 속도.
        /// </summary>
        Speed
    }

    public class PrimeAbilityContainer : SubAbilityContainer, IGetAbilities
    {
        #region Fields & Property

        public readonly int[] PrimeAbilities;

        #endregion


        #region Methods

        #region Override

        public override float GetAbilityValue(SubAbilities abilityType) =>
            GetAbilityComposite(abilityType).GetAbilityValue();


        public override float GetNumberValue(SubAbilities abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public override float GetPercentValue(SubAbilities abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        public override IAbilityComposite GetAbilityComposite(SubAbilities abilityType)
        {
            return base.GetAbilityComposite(abilityType);
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}