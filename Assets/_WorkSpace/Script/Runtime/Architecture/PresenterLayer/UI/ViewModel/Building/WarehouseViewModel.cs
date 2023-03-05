using System.Collections.Generic;
using System.Linq;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess
{
    public enum WarehouseSlotState
    {
        Empty,
        Stored,
    }

    public class WarehouseSlotCellModel
    {
        public WarehouseSlotCellModel(WarehouseBuildingEntity.WarehouseStoreSlot warehouseStoreSlot)
        {
            WarehouseStoreSlot = warehouseStoreSlot;
            SlotState = WarehouseStoreSlot.IsEmpty ? WarehouseSlotState.Empty : WarehouseSlotState.Stored;
        }

        public WarehouseBuildingEntity.WarehouseStoreSlot WarehouseStoreSlot;

        public WarehouseSlotState SlotState;
    }

    public class WarehouseViewModel : IViewModel
    {
        #region Fields & Property

        private WarehouseBuildingEntity _warehouseBuilding;

        #endregion


        #region Methods

        #region Override

        [Inject]
        public void Construct(BuildingManager buildingMgr)
        {
            _warehouseBuilding = buildingMgr.GetBuilding(BuildingType.Warehouse) as WarehouseBuildingEntity;
        }
        
        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        public List<WarehouseSlotCellModel> WarehouseSlotCellModels =>
            _warehouseBuilding.StoreSlots.Select(ss => new WarehouseSlotCellModel(ss)).ToList();

        #endregion


        #region Event

        #endregion

        #endregion


    }
}