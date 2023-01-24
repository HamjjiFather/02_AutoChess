using Zenject;

namespace AutoChess
{
    public enum BlackSmithBehaviourType
    {
        Product,
        Enhance,
        Appraisal,
        Purchase,
        Sell,
        Repair,
    }


    public partial class BlackSmithBuilding : BuildingBase, IInitializable
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
            Initialize_Product();
            Initialize_Purchase();
        }

        protected override void OnLevelUp(int level)
        {
            OnLevelUp_Product();
            OnLevelUp_Product();
        }

        public override void Build()
        {
        }

        public override void SpendTime()
        {
            SpendTime_Product();
            SpendTime_Purchase();
        }

        #endregion


        #region This

        #endregion


        #region Event

        // /// <summary>
        // /// 제작 의뢰.
        // /// </summary>
        // public bool ProductReserveEquipment()
        // {
        //     var hasEmptySlot = ProductEquipments.Values.Count(x => x.ProductState.Equals(ProductState.Empty)) >= 1;
        //     if (!hasEmptySlot)
        //     {
        //         return false;
        //     }
        //     
        //     var emptyIndex = ProductEquipments.Values.ToList().FindIndex(x => x.ProductState == ProductState.Empty);
        //     ProductEquipments[emptyIndex].ProductState = ProductState.Reserved;
        //     return true;
        // }
        //
        //
        // public void EnhanceEquipment(EquipmentBase equipment)
        // {
        // }
        //
        //
        // public void AppraisalEquipment(EquipmentBase equipment)
        // {
        // }
        //
        //
        // public void PurchaseEquipment(EquipmentBase equipment)
        // {
        // }
        //
        //
        // public void SellEquipment(EquipmentBase equipment)
        // {
        // }
        //
        // public void RepairEquipment(EquipmentBase equipment)
        // {
        // }

        #endregion

        #endregion
    }
}