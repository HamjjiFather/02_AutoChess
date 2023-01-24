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
        public string index = string.Empty;
        
        public string Index { get => index; set => index = value; }

        public string itemIndex;

        public int amount;
    }


    [Serializable]
    public class AdventureInventoryBundle : BundleBase<AdventureInventoryItemBundleSet>
    {
        #region Fields & Property

        public override Dictionary<string, AdventureInventoryItemBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.Index, x => x);

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        // public void Bind(int uid, string itemIndex, int amount)
        // {
        //     var id = uid.ToString();
        //     var load = Load(id);
        //
        //     // bundleSet.itemIndex = itemIndex;
        //     // bundleSet.amount = amount;
        //
        //     base.Save(load);
        // }


        #endregion


        #region Event

        #endregion

        #endregion
    }
}