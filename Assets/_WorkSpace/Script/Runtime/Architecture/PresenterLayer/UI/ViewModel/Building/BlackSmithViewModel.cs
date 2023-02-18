using System.Linq;
using JetBrains.Annotations;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess
{
    public enum BlackSmithPurchaseSlotState
    {
        /// <summary>
        /// 장비를 가져오는 중. 
        /// </summary>
        WaitForBring,

        /// <summary>
        /// 판매 중.
        /// </summary>
        Selling,

        /// <summary>
        /// 잠겨있음.
        /// </summary>
        Locked,
    }


    /// <summary>
    /// 구매 장비 아이템 슬롯 셀 모델.
    /// 데이터가 있고 Entity에 장비 데이터가 없으면 가져오는 중 상태, 있으면 판매 중 상태.
    /// </summary>
    public class BlackSmithPurcaseSlotCellModel
    {
        public BlackSmithPurcaseSlotCellModel(BlackSmithBuildingEntity.PurchaseSlotEntity purchaseSlotEntity)
        {
            PurchaseSlotEntity = purchaseSlotEntity;
            SlotState = PurchaseSlotEntity.PurchaseEquipment == default
                ? BlackSmithPurchaseSlotState.WaitForBring
                : BlackSmithPurchaseSlotState.Selling;
        }

        public BlackSmithBuildingEntity.PurchaseSlotEntity PurchaseSlotEntity;

        public BlackSmithPurchaseSlotState SlotState;
    }


    public class BlackSmithViewModel : IViewModel
    {
        public BlackSmithViewModel()
        {
        }

        #region Fields & Property

        [Inject]
        private BuildingManager _buildingManager;

        private BlackSmithBuildingEntity _blackSmithBuildingEntity;

        private BlackSmithBuildingEntity BlackSmithBuildingEntity
        {
            get => _blackSmithBuildingEntity ??=
                _buildingManager.GetBuilding(BuildingType.BlackSmith) as BlackSmithBuildingEntity;
            set => _blackSmithBuildingEntity = value;
        }

        private BlackSmithBuildingEntity _blackSmithBuilding;

        public BlackSmithPurcaseSlotCellModel[] GetPurchaseSlotCellModels
        {
            get
            {
                return BlackSmithBuildingEntity.PurchaseSlotEntityMap.Values
                    .Select(x => new BlackSmithPurcaseSlotCellModel(x)).ToArray();
            }
        }

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}