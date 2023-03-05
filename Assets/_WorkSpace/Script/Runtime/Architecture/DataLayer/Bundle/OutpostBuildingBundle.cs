using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;
using UnityEngine.Serialization;

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

        public bool hasBuilt;

        public int[] extendBuildings = Enumerable.Empty<int>().ToArray();
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