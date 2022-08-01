namespace AutoChess.Domain
{
    public enum ItemType
    {
        Junk,
        UnidentifiedGear,
        Corps,
    }
    
    public interface IItem
    {
        public int ItemIndex { get; set; }

        public int ItemAmount { get; set; }
    }
    
    public class InventoryItemModel : IItem, IGetSprite
    {
        public Item ItemTableData;
        
        public int UniqueIndex { get; set; }

        public int ItemIndex { get; set; }
        
        public int ItemAmount { get; set; }

        public virtual ItemType ItemType { get; }

        public float Weight;

        public InventoryItemModel(int uniqueIndex, int itemIndex, int itemAmount)
        {
            UniqueIndex = uniqueIndex;
            ItemIndex = itemIndex;
            ItemAmount = itemAmount;

            ItemTableData = TableDataManager.Instance.ItemDict[itemIndex];
        }
    }
}