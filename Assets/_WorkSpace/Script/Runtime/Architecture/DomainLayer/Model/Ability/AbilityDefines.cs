using System.Collections.Generic;

namespace AutoChess
{
    public static class AbilityDefines
    {
        public enum GrantedAbilityType
        {
            PrimePoint,
            AbilityValue,
            SkillIndex
        }
    
    
        public class GrantedAbility
        {
            public GrantedAbilityType GrantedAbilityType;

            public float AbilityValue;
        }
        
        
        #region Fields & Property

        public static readonly Dictionary<PrimePointType, AdditionalAbilityType[]> AbilityPerPrimePoint =
            new Dictionary<PrimePointType, AdditionalAbilityType[]>
            {
                {
                    PrimePointType.Health,
                    new[]
                    {
                        AdditionalAbilityType.Health
                    }
                },
                {
                    PrimePointType.Mana,
                    new[]
                    {
                        AdditionalAbilityType.Mana
                    }
                },
                {
                    PrimePointType.Strength,
                    new[]
                    {
                        AdditionalAbilityType.AttackDamage, AdditionalAbilityType.AttackResistance
                    }
                },
                {
                    PrimePointType.Dexterity,
                    new[]
                    {
                        AdditionalAbilityType.CriticalProb, AdditionalAbilityType.BlockProb
                    }
                },
                {
                    PrimePointType.Intelligence,
                    new[]
                    {
                        AdditionalAbilityType.SpellDamage, AdditionalAbilityType.SpellResistance
                    }
                }
            };


        public static readonly Dictionary<AdditionalAbilityType, float> valuePerPrimePoint =
            new Dictionary<AdditionalAbilityType, float>
            {
                {AdditionalAbilityType.Health, 1.2f},
                {AdditionalAbilityType.Mana, 1f},
                {AdditionalAbilityType.AttackDamage, 2.4f},
                {AdditionalAbilityType.AttackResistance, 1.2f},
                {AdditionalAbilityType.CriticalProb, 1.6f},
                {AdditionalAbilityType.BlockProb, 1.46f},
                {AdditionalAbilityType.SpellDamage, 3.4f},
                {AdditionalAbilityType.SpellResistance, 1.4f}
            };
        
        

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}