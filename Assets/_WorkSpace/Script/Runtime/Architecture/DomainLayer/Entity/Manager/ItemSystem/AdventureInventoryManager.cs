using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    public class AdventureInventoryManager : ManagerBase, IInventory
    {
        #region Fields & Property

        public Dictionary<int, ItemSlotBase> StoredItems { get; set; }

        #endregion


        #region Methods

        #region Override

        public override void Initialize()
        {
            base.Initialize();
        }

        public void StoreItem(int slotIndex)
        {
            var index = StoredItems.Values.ToList().FindIndex(x => x.IsEmpty);
            StoredItems[index].Store(slotIndex);
        }

        public int TakeOutItem(int slotIndex) => StoredItems[slotIndex].TakeOut();

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}