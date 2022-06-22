namespace AutoChess
{
    public static class AbilityFormula
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This
        
        public static float BehaviourSecondsFromSpeedAbilityValue(float value)
        {
            return (value / AbilityDefine.SpeedValueBase) / 1;
        }
        

        #endregion


        #region Event

        #endregion

        #endregion
    }
}