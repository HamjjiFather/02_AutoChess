namespace AutoChess
{
    /// <summary>
    /// 개별 능력 수치.
    /// </summary>
    public class CompositeAbility : IAbilityComposite
    {
        #region Fields & Property

        public IAbility PercentValue { get; set; }

        public IAbility NumberValue { get; set; }

        #endregion


        #region Methods

        #region Override
        
        public float GetAbilityValue()
        {
            var numberValue = NumberValue.GetValue();
            return numberValue + numberValue * PercentValue.GetValue();
        }
        
        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}