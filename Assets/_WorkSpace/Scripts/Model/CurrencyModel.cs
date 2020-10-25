using KKSFramework.DesignPattern;
using MasterData;
using UniRx;

namespace AutoChess
{
    public class CurrencyModel : ModelBase
    {
        #region Fields & Property
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        public CurrencyType CurrencyType;

        public readonly IntReactiveProperty CurrencyAmount = new IntReactiveProperty ();

        #endregion


        #region Methods

        #endregion


        public CurrencyModel (CurrencyType currencyType, int amount)
        {
            CurrencyType = currencyType;
            CurrencyAmount.Value = amount;
        }
    }
}