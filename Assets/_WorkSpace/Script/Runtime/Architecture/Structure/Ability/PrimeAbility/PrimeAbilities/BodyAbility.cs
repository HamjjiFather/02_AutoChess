using System.Linq;

namespace AutoChess
{
    /// <summary>
    /// 주요 능력치 - 신체.
    /// </summary>
    public class BodyAbility : PrimeAbilityBase
    {
        public BodyAbility(int value, int investedValue) : base(value, investedValue)
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
            if (!SubAbilityTypes.Contains(subAbilityType))
                return default;
            
            return subAbilityType switch
            {
                SubAbilityType.HealthPoint => Value * 10,
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