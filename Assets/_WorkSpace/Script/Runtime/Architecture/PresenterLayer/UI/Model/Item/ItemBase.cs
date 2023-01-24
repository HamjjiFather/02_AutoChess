using UnityEngine;

namespace AutoChess.Domain
{
    public interface IItem
    {
        Item ItemTableData { get; set; }

        int UniqueIndex { get; set; }

        int ItemAmount { get; set; }
    }


    /// <summary>
    /// 아이템의 가장 기본 클래스.
    /// </summary>
    public class ItemBase : IItem
    {
        public Item ItemTableData { get; set; }
        
        public int UniqueIndex { get; set; }
        
        public int ItemAmount { get; set; }


        public ItemBase(int uniqueIndex, string itemIndex, int itemAmount)
        {
            // UniqueIndex = uniqueIndex;
            // ItemIndex = itemIndex;
            // ItemAmount = itemAmount;

            // ItemTableData = TableDataManager.Instance.ItemDict[itemIndex];
        }
    }

    /// <summary>
    /// 보관된 아이템 데이터.
    /// </summary>
    public class StoredItemBase : ItemBase
    {
        public StoredItemBase(int uniqueIndex, string itemIndex, int itemAmount) : 
            base(uniqueIndex, itemIndex, itemAmount)
        {
        }
    }
}