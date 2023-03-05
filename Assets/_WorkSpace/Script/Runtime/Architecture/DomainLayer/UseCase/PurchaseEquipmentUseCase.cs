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

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

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