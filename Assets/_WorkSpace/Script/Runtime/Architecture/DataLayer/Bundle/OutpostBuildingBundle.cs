using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class OutpostBuildingBundleSet : IBundleSet
    {
        public string index;

        public string Index
        {
            get => index;
            set => index = value;
        }

        public OutpostAddOnTypes addOnTypes;
    }

    [Serializable]
    public class OutpostBuildingBundle : BundleBase<OutpostBuildingBundleSet>
    {
        #region Fields & Property

        public override Dictionary<string, OutpostBuildingBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.index);

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