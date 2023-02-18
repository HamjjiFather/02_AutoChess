using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace AutoChess
{
    public class WarehouseBuildingEntity : BuildingEntityBase, IInitializable
    {
        public class WarehouseStoreSlot : ItemSlotBase
        {
        }

        public WarehouseBuildingEntity(Building buildingTableData) : base(buildingTableData)
        {
        }

        #region Fields & Property

        public List<WarehouseStoreSlot> StoreSlots;

        /// <summary>
        /// 인벤토리 공간.
        /// </summary>
        public int InventorySpace
        {
            get
            {
                return ItemDefine.BaseInventorySpace + AdditionalValue();

                int AdditionalValue()
                {
                    return Level switch
                    {
                        1 => 5,
                        2 => 10,
                        3 => 15,
                        4 => 20,
                        _ => 0,
                    };
                }
            }
        }

        #endregion


        #region Methods

        #region Override
        
        
        public override void Initialize()
        {
            StoreSlots = new List<WarehouseStoreSlot>(InventorySpace);
            StoreSlots = Enumerable.Range(0, InventorySpace)
                .Select(x => new WarehouseStoreSlot())
                .ToList();
        }
        
        
        protected override void OnLevelUp(int level)
        {
            StoreSlots.Capacity = InventorySpace;
            for (var i = 0; i < InventorySpace - StoreSlots.Count; i++)
            {
                StoreSlots.Add(new WarehouseStoreSlot());
            }
        }

        
        public override void Build()
        {
            // throw new System.NotImplementedException();
        }

        
        public override void SpendTime()
        {
            // throw new System.NotImplementedException();
        }


        #endregion


        #region This

        public void StoreItem()
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}