using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class StatusView : MonoBehaviour
    {
        #region Fields & Property

        [Inject]
        private CurrencyManager _currencyManager;

        public StatusViewCurrencyInfoArea[] currencyInfoAreas;

        #endregion


        #region Methods

        #region Override

        private void Start()
        {
            currencyInfoAreas[0].currencyIcon.sprite = CurrencyHelper.GetCurrencyIcon(CurrencyType.Gold);
            currencyInfoAreas[1].currencyIcon.sprite = CurrencyHelper.GetCurrencyIcon(CurrencyType.Lore);
            
            _currencyManager.GetCurrency(CurrencyType.Gold).Subscribe(ir =>
            {
                currencyInfoAreas[0].amountText.text = ir.ToString();
            });
            
            _currencyManager.GetCurrency(CurrencyType.Lore).Subscribe(ir =>
            {
                currencyInfoAreas[1].amountText.text = ir.ToString();
            });
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}