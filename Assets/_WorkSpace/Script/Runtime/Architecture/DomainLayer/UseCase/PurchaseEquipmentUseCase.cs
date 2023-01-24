using JetBrains.Annotations;
using KKSFramework.Domain;

namespace AutoChess
{
    [UsedImplicitly]
    public class PurchaseEquipmentUseCase : IUseCaseBase
    {
        public PurchaseEquipmentUseCase(CurrencySystemManager currencySystemManager)
        {
            _currencySystemManager = currencySystemManager;
        }

        #region Fields & Property

        private readonly CurrencySystemManager _currencySystemManager;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void Execute(EquipmentBase equipmentBase)
        {
            var price = equipmentBase.EquipmentTableData.SellingPrice;
            _currencySystemManager.VariationCurrency(CurrencyType.Gold, -price);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}