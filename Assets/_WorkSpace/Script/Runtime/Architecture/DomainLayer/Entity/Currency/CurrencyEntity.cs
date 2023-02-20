using UniRx;

namespace AutoChess
{
    public class CurrencyEntity : IItemEntityBase
    {
        public CurrencyEntity(int uniqueIndex, int amount)
        {
            UniqueIndex = uniqueIndex;
            Amount = new IntReactiveProperty(amount);
        }

        #region Fields & Property

        public int UniqueIndex { get; set; }

        public IntReactiveProperty Amount { get; set; }

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}