using System.Collections.Generic;
using KKSFramework;

namespace AutoChess
{
    /// <summary>
    /// 보조 능력치.
    /// </summary>
    public enum SubAbilityType
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


    public class SubAbilityContainer
    {
        #region Fields & Property

        /// <summary>
        /// 보조 능력치.
        /// </summary>
        public Dictionary<SubAbilityType, IAbilityComponent> AbilityContainers { get; set; }

        #endregion


        #region Methods

        #region Override

        public virtual void SetAbilityValue(SubAbilityType abilityType, int value) =>
            AbilityContainers.SetOrAdd(abilityType, new SubAbilityComponent(value));

        public virtual int GetAbilityValue(SubAbilityType abilityType) => AbilityContainers.ContainsKey(abilityType)
            ? AbilityContainers[abilityType].Value
            : default;

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}