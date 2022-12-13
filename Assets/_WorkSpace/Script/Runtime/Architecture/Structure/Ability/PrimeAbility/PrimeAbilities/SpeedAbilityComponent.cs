namespace AutoChess
{
    /// <summary>
    /// 주요 능력치 - 속도.
    /// </summary>
    public class SpeedAbilityComponent : PrimeAbilityComponentBase
    {
        #region Fields & Property
        
        #endregion


        #region Methods

        #region Override
        
        /// <summary>
        /// 보조 능력치 리턴.
        /// </summary>
        public override int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            return subAbilityType switch
            {
                SubAbilityType.CriticalDamage => Value * 10,
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