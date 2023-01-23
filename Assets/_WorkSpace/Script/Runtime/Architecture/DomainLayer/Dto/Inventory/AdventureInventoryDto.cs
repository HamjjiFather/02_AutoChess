namespace AutoChess.Dto
{
    public struct AdventureInventoryDto
    {
        public int UniqueIndex;
        
        public string ItemIndex;

        public int Amount;

        
        public AdventureInventoryDto(int uniqueIndex, string itemIndex, int amount)
        {
            UniqueIndex = uniqueIndex;
            ItemIndex = itemIndex;
            Amount = amount;
        }
    }
}