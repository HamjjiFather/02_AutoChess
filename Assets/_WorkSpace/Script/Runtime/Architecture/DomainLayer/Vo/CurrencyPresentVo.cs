using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public readonly struct CurrencyPresentVo
    {
        public CurrencyPresentVo(Sprite iconImage, string amountString, bool enough)
        {
            IconImage = iconImage;
            AmountString = amountString;
            Enough = enough;
        }

        public readonly Sprite IconImage;

        public readonly string AmountString;

        public readonly bool Enough;
    }


    public readonly struct CurrencyDataVo
    {
        public CurrencyDataVo(CurrencyType currencyType, int currencyAmount)
        {
            CurrencyType = currencyType;
            CurrencyAmount = currencyAmount;
        }

        public readonly CurrencyType CurrencyType;

        public readonly int CurrencyAmount;
    }
}