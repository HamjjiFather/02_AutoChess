namespace AutoChess
{
    public struct AbilitySet
    {
        public AbilitySet (AbilityType abilityType, double abilityValue)
        {
            AbilityType = abilityType;
            AbilityValue = abilityValue;
        }


        #region Fields & Property

        public AbilityType AbilityType;

        public double AbilityValue;

        #endregion
    }
}