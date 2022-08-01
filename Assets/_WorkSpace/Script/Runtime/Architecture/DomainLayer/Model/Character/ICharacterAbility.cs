namespace AutoChess.Presenter
{
    public interface ICharacterAbility
    {
        public Character CharacterTableData { get; set; }
        
        public PrimeAbilityContainer PrimeAbilityContainer { get; set; }
    }
}