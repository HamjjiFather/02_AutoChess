using System;
using System.Collections.Generic;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class EquipmentBundleSet : IBundleSet
    {
        public string Index { get; set; }

        public int uniqueIndex;

        public int equipmentTableData;

        public int[] slotIndexes;
        
    }
    
    [Serializable]
    public class EquipmentBundle : BundleBase<EquipmentBundleSet>
    {
        public EquipmentBundle()
        {
            uniqueIndex = UniqueIndexDefine.BaseEquipmentUniqueIndex;
        }

        #region Fields & Property

        public int uniqueIndex;
        
        public override Dictionary<string, EquipmentBundleSet> ToDictionaryLinq { get; }

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