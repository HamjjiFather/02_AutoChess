using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    public class StorageInventoryManager : ManagerBase, IInventory
    {
        #region Fields & Property

        public Dictionary<int, ItemSlotBase> StoredItems { get; set; }

        private WarehouseBuildingModel _warehouseBuildingModel;

        #endregion


        #region Methods

        #region Override

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion


        #region This

        /// <summary>
        /// 빈 보관함 슬롯이 있는지?
        /// </summary>
        /// <returns></returns>
        public bool ExistEmptySpace() => StoredItems.Values.Any(x => x.IsEmpty);


        /// <summary>
        /// 창고 건물의 업그레이드에 따라 보관함 용량을 확보함.
        /// </summary>
        public void SyncStorageSpace(BuildingManager buildingManager)
        {
            _warehouseBuildingModel = buildingManager.GetBuilding<WarehouseBuildingModel>();
            StoredItems = new Dictionary<int, ItemSlotBase>(_warehouseBuildingModel.InventorySpace);

            _warehouseBuildingModel.OnLevelUpEvent += OnWarehouseLevelUp;

            void OnWarehouseLevelUp(int _)
            {
                StoredItems.EnsureCapacityOrAdd(_warehouseBuildingModel.InventorySpace);
            }
        }


        /// <summary>
        /// 아이템을 보관함.
        /// </summary>
        public void StoreItem(int slotIndex)
        {
            var index = StoredItems.Values.ToList().FindIndex(x => x.IsEmpty);
            StoredItems[index].Store(slotIndex);
        }


        /// <summary>
        /// 아이템을 빼냄.
        /// </summary>
        public int TakeOutItem(int slotIndex) => StoredItems[slotIndex].TakeOut();

        #endregion


        #region Event

        #endregion

        #endregion
    }
}