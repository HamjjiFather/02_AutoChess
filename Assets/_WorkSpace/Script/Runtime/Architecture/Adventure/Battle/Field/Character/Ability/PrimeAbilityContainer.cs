namespace AutoChess
{
    /// <summary>
    /// 주요 능력치.
    /// </summary>
    public enum PrimeAbilities
    {
        Body,
        Skill,
        Meltality,
        Wisdom,
        Speed
    }

    public class PrimeAbilityContainer : SubAbilityContainer
    {
        #region Fields & Property

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

        public virtual float GetAbilityValue(PrimeAbilities abilityType) =>
            GetAbilityComposite(abilityType).GetAbilityValue();


        public virtual float GetNumberValue(PrimeAbilities abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public virtual float GetPercentValue(PrimeAbilities abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        public virtual IAbilityComposite GetAbilityComposite(PrimeAbilities abilityType)
        {
            if (!AbilityContainers.ContainsKey(abilityType.ToString()))
                return default;

            return AbilityContainers[abilityType.ToString()];
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}