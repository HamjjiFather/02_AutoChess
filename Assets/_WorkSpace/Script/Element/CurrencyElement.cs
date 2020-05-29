using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HexaPuzzle
{
    public class CurrencyElement : MonoBehaviour
    {
        #region Fields & Property

        public Image icon;

        public Text statusText;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private CurrencyModel _currencyModel;

        private IDisposable _currencyDisposable;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetCurrencyElemency (CurrencyModel currencyModel)
        {
            _currencyModel = currencyModel;
            _currencyModel.CurrencyAmount.SubscribeToText (statusText);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}