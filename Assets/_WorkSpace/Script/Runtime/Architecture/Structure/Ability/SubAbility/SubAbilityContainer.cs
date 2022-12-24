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


    public class SubAbilityContainer : IGetSubAbility
    {
        #region Fields & Property

        /// <summary>
        /// 보조 능력치.
        /// </summary>
        public Dictionary<SubAbilityType, int> AbilityContainers { get; set; } = new();

        #endregion


        #region Methods

        #region Override

        public virtual void SetAbilityValue(SubAbilityType subAbilityType, int value) =>
            AbilityContainers.SetOrAdd(subAbilityType, value);

        public virtual int GetSubAbilityValue(SubAbilityType subAbilityType) =>
        AbilityContainers.ContainsKey(subAbilityType)
            ? AbilityContainers[subAbilityType]
                : default;

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}