
namespace AutoChess
{
    public enum PrimePointType
    {
        Health,
        Mana,
        Strength,
        Dexterity,
        Intelligence
    }
    
    
    public enum AdditionalAbilityType
    {
        Health = 100,
        Mana = 200,

        AttackDamage = 300,
        AttackResistance = 301,
        
        CriticalProb = 400,
        BlockProb = 401,
        
        SpellDamage = 500,
        SpellResistance = 501,
        
        HealthRecovery,
        ManaRecovery,
        
        /// <summary>
        /// 초당 공격 횟수
        /// </summary>
        AttackAmountPerSeconds,
        
        /// <summary>
        /// 1타일당 이동 횟수
        /// </summary>
        MovementAmountPerTile,
        
        FlameResistance,
        FreezeResistance,
        ElectricResistance,
        
        AttackDamagePercent,
        SpellDamagePercent,
    }
    

    public class AbilityModelBase
    {
        public AbilityModelBase(AdditionalAbilityType additionalAbilityType, float abilityValue)
        {
            AdditionalAbilityType = additionalAbilityType;
            AbilityValue = abilityValue;
        }

        #region Fields & Property

        public AdditionalAbilityType AdditionalAbilityType;

        public float AbilityValue;

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