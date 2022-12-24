namespace AutoChess.Presenter
{
    public interface ICharacterAbility
    {
        public global::Character CharacterTableData { get; set; }
        
        public PrimeAbilityBase PrimeAbilityBase { get; set; }
    }
}