using System;

namespace AutoChess
{
    public partial class BlackSmithBuildingEntity
    {
        #region Fields & Property

        public int PricePercent
            => Level switch
            {
                > 1 and <= 2 => 10000,
                >= 3 => 25000,
                _ => 0
            };

        #endregion


        #region Methods

        #region Override

        private void Initialize_Selling()
        {
        }


        private void OnLevelUp_Selling()
        {
        }


        public void SpendTime_Selling()
        {
        }

        #endregion


        #region This

        #endregion


        #region Event

        public void SellingEquipment(EquipmentBase equipmentBase)
        {
            var sellingPrice = EquipmentHelper.SellingPrice(equipmentBase);
        }

        #endregion

        #endregion
    }
}