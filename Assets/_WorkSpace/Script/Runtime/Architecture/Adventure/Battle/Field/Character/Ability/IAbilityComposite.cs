namespace AutoChess
{
    public interface IAbilityComposite
    {
        public IAbility NumberValue { get; set; }

        public IAbility PercentValue { get; set; }

        public float GetAbilityValue();
    }
}