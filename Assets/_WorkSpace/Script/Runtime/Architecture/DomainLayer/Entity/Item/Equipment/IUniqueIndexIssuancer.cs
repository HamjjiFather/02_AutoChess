namespace AutoChess
{
    public interface IUniqueIndexIssuancer
    {
        int UniqueIndex { get; set; }

        int GetUniqueIndex();
    }
}