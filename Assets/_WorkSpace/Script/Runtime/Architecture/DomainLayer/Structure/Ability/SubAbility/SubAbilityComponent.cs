namespace AutoChess
{
    public class SubAbilityComponent : IGetSubAbility
    {
        public SubAbilityComponent(SubAbilityType subAbilityType, int value)
        {
            SubAbilityType = subAbilityType;
            Value = value;
        }

        #region Fields & Property

        public readonly SubAbilityType SubAbilityType;

        public readonly int Value;

        #endregion


        #region Methods

        #region Override

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            return SubAbilityType.Equals(subAbilityType) ? Value : default;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}