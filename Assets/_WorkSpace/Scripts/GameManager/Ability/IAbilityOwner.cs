namespace AutoChess
{
    public interface IAbilityOwner
    {
        public string OwnerString { get; }

        public AbilityTypeContainer AbilityTypeContainer { get; set; }
    }


    public interface IAbilityAllocator : IAbilityOwner
    {
        public string Allocator { get; }
    }
}