using System;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class EquipmentBundle : IBundleBase
    {
        public EquipmentBundle()
        {
            uniqueIndex = UniqueIndexDefine.BaseEquipmentUniqueIndex;
        }

        #region Fields & Property

        public int uniqueIndex;

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