using System.Collections.Generic;

namespace AutoChess
{
    /// <summary>
    /// 보조 능력치.
    /// </summary>
    public enum SubAbilities
    {
        Health,
        Mana,
        PhysicalDamage,
        MagicalDamage,
        PhysicalDefense,
        MagicalRegistance,
        CriticalProbability,
        CriticalDamage,
        Speed,
        Tough,
        FlameRegistance,
        LightningRegistance,
        FrostRegistance
    }
    

    public abstract class SubAbilityContainer : AbilityContainer
    {
        #region Fields & Property

        /// <summary>
        /// 보조 능력치.
        /// </summary>
        public Dictionary<string, IAbilityComposite> AbilityContainers { get; set; }

        #endregion


        #region Methods

        #region Override
        
        public virtual float GetAbilityValue(string abilityType) => GetAbilityComposite(abilityType).GetAbilityValue();


        public virtual float GetNumberValue(string abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public virtual float GetPercentValue(string abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        public virtual IAbilityComposite GetAbilityComposite(string abilityType)
        {
            if (!AbilityContainers.ContainsKey(abilityType))
                return default;

            return AbilityContainers[abilityType];
        }

        #endregion


        #region This
        
        
        public virtual float GetAbilityValue(SubAbilities abilityType) => GetAbilityComposite(abilityType).GetAbilityValue();


        public virtual float GetNumberValue(SubAbilities abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public virtual float GetPercentValue(SubAbilities abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        public virtual IAbilityComposite GetAbilityComposite(SubAbilities abilityType)
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