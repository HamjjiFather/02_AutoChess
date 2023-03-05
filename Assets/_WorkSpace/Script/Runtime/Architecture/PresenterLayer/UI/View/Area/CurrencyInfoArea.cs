using System;
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

        private Color _originTextColor;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            _originTextColor = amountText.color;
        }

        #endregion


        #region This

        public void SetArea(CurrencyPresentVo presentVo)
        {
            currencyIcon.sprite = presentVo.IconImage;
            amountText.text = presentVo.AmountString;
            amountText.color = presentVo.Enough ? _originTextColor : ColorDefine.NegativeColor;
        }


        public void SetArea(CurrencyDataVo dataVo)
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}