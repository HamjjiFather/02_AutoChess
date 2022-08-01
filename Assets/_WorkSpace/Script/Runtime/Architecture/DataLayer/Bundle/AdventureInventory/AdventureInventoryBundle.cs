using System;
using System.Collections.Generic;
using System.Linq;
using AutoChess.Domain;
using AutoChess.Dto;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class AdventureInventoryItemBundleSet : IBundleSet
    {
        public int uniqueIndex;

        public ItemType itemType;

        public int itemIndex;

        public int amount;
    }


    [Serializable]
    public class AdventureInventoryBundle : BundleBase<AdventureInventoryItemBundleSet>
    {
        #region Fields & Property

        public override Dictionary<int, AdventureInventoryItemBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.uniqueIndex, x => x);

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void Bind(int uid, int itemIndex, int amount)
        {
            var bundleSet = Resolve(uid);

            bundleSet.itemIndex = itemIndex;
            bundleSet.amount = amount;

            base.Bind(uid, bundleSet);
        }


        #endregion


        #region Event

        #endregion

        #endregion
    }
}