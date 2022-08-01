namespace AutoChess.Dto
{
    public struct AdventureInventoryDto
    {
        public int UniqueIndex;
        
        public int ItemIndex;

        public int Amount;

        
        public AdventureInventoryDto(int uniqueIndex, int itemIndex, int amount)
        {
            UniqueIndex = uniqueIndex;
            ItemIndex = itemIndex;
            Amount = amount;
        }
    }
}