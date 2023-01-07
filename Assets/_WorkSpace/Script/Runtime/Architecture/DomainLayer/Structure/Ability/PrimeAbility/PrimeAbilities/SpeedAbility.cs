namespace AutoChess
{
    /// <summary>
    /// 주요 능력치 - 속도.
    /// </summary>
    public class SpeedAbility : PrimeAbilityBase
    {
        public SpeedAbility(int value, int investedValue) : base(value, investedValue)
        {
            _subAbilityTypes = new[]
            {
                SubAbilityType.HealthPoint
            };
        }
        
        #region Fields & Property
        
        #endregion


        #region Methods

        #region Override
        
        private SubAbilityType[] _subAbilityTypes;

        public override SubAbilityType[] SubAbilityTypes
        {
            get => _subAbilityTypes;
            set => _subAbilityTypes = value;
        }
        
        /// <summary>
        /// 보조 능력치 리턴.
        /// </summary>
        public override int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            return subAbilityType switch
            {
                SubAbilityType.CriticalDamagePercent => Value * 10,
                _ => default
            };
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}