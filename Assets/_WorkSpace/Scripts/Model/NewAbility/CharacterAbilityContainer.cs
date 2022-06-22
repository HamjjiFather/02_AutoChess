using System.Collections.Generic;
using UnityEngine;

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

    public abstract class CharacterAbilityContainer
    {
        #region Fields & Property

        /// <summary>
        /// 보조 능력치.
        /// </summary>
        private readonly Dictionary<string, IAbilityComposite> _abilityContainers =
            new Dictionary<string, IAbilityComposite>();

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public virtual float GetAbilityValue(string abilityType) => GetAbilityComposite(abilityType).GetAbilityValue();


        public virtual float GetNumberValue(string abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public virtual float GetPercentValue(string abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        public virtual IAbilityComposite GetAbilityComposite(string abilityType)
        {
            if (!_abilityContainers.ContainsKey(abilityType))
                return default;

            return _abilityContainers[abilityType];
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}