using System;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine.UI;

namespace AutoChess
{
    public class CurrencyElement : ElementBase<CurrencyModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver("CurrencyImage")]
        private Image _icon;

        [Resolver("CurrencyText")]
        private Text _statusText;

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
            ElementData.CurrencyAmount.SubscribeToText (_statusText);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}