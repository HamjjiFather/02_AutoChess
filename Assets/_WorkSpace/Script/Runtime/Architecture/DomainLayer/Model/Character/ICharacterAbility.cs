namespace AutoChess.Presenter
{
    public interface ICharacterAbility
    {
        public global::Character CharacterTableData { get; set; }
        
        public PrimeAbilityComponentBase PrimeAbilityComponentBase { get; set; }
    }
}