using System;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine.UI;

namespace AutoChess
{
    public class CurrencyElement : ElementBase<CurrencyModel>
    {
        #region Fields & Property

        public Image icon;

        public Text statusText;

#pragma warning disable CS0649

#pragma warning restore CS0649

        public override CurrencyModel ElementData { get; set; }

        private IDisposable _currencyDisposable;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetElement (CurrencyModel currencyModel)
        {
            ElementData = currencyModel;
            ElementData.CurrencyAmount.SubscribeToText (statusText);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}