namespace AutoChess.Domain
{
    public enum ItemType
    {
        /// <summary>
        /// 잡동사니.
        /// </summary>
        Junk,
        
        /// <summary>
        /// 식별되지 않은 장비.
        /// </summary>
        UnidentifiedGear,
        
        /// <summary>
        /// 시체.
        /// </summary>
        Corps,
    }
    
    public interface IItem
    {
        public string ItemIndex { get; set; }

        public int ItemAmount { get; set; }
    }

    
    public class ItemModel : IItem, IGetSprite
    {
        public Item ItemTableData;
        
        public int UniqueIndex { get; set; }

        public string ItemIndex { get; set; }
        
        public int ItemAmount { get; set; }

        public virtual ItemType ItemType { get; }

        public float Weight;

        public ItemModel(int uniqueIndex, string itemIndex, int itemAmount)
        {
            UniqueIndex = uniqueIndex;
            ItemIndex = itemIndex;
            ItemAmount = itemAmount;

            // ItemTableData = TableDataManager.Instance.ItemDict[itemIndex];
        }
    }
}