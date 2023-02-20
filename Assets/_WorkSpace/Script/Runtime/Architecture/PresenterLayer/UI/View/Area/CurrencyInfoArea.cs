using KKSFramework.Navigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class CurrencyInfoArea : MonoBehaviour
    {
        #region Fields & Property

        public Image currencyIcon;

        public TextMeshProUGUI amountText;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void SetArea(CurrencyPresentVo vo)
        {
            currencyIcon.sprite = vo.IconImage;
            amountText.text = vo.AmountString;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}