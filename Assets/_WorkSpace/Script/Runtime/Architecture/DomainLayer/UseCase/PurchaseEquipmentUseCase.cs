using JetBrains.Annotations;
using KKSFramework.Domain;

namespace AutoChess
{
    [UsedImplicitly]
    public class PurchaseEquipmentUseCase : IUseCaseBase
    {
        public PurchaseEquipmentUseCase(CurrencyManager currencyManager)
        {
            _currencyManager = currencyManager;
        }

        #region Fields & Property

        private readonly CurrencyManager _currencyManager;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void Execute(EquipmentEntity equipmentEntity)
        {
            var price = equipmentEntity.EquipmentTableData.SellingPrice;
            _currencyManager.VariationCurrency(CurrencyType.Gold, -price);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}