using System.Collections.Generic;

namespace AutoChess
{
    public interface IInventory
    {
        // Dictionary<int, ItemSlotBase> StoredItems { get; set; }

        void StoreItem(int slotIndex);

        
        int TakeOutItem(int slotIndex);
    }
}