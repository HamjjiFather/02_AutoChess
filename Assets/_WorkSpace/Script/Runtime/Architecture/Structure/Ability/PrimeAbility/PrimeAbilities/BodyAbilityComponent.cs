namespace AutoChess
{
    /// <summary>
    /// 주요 능력치 - 신체.
    /// </summary>
    public class BodyAbilityComponent : PrimeAbilityComponentBase
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
                SubAbilityType.Health => Value * 10,
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