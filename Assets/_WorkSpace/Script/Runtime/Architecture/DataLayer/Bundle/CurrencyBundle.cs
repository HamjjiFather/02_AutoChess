using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class CurrencyBundleSet : IBundleSet
    {
        public string index = string.Empty;

        public string Index
        {
            get => index;
            set => index = value;
        }

        public int amount;
    }

    [Serializable]
    public class CurrencyBundle : BundleBase<CurrencyBundleSet>
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override Dictionary<string, CurrencyBundleSet> ToDictionaryLinq => bundleSets.ToDictionary(x => x.index);

        public override void Initialize()
        {
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}