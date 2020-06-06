using System.Collections.Generic;
using UnityEngine;
using KKSFramework.DesignPattern;

namespace HexaPuzzle
{
    public class GameViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly SummonResultModel _summonResultModel = new SummonResultModel ();
        public SummonResultModel SummonResultModel => _summonResultModel;
            

        private readonly List<CurrencyModel> _currencyModels = new List<CurrencyModel> ();
        public List<CurrencyModel> CurrencyModels => _currencyModels;

        #endregion


        public override void Initialize ()
        {
            _currencyModels.Add (new CurrencyModel (CurrencyType.Gold, 1000));
            _currencyModels.Add (new CurrencyModel (CurrencyType.Soulstone, 100));
        }


        #region Subscribe

        #endregion
        

        #region Methods

        public void SetResult (float value)
        {
            _summonResultModel.SummonGageValue.Value = value;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}