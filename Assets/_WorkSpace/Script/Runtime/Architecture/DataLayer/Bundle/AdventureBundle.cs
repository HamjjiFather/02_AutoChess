using System;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class AdventureBundle : BundleBase
    {
        #region Fields & Property

        public int issuancedUniqueIndex;

        #endregion


        #region Methods

        #region Override
        
        public override void Initialize()
        {
            
        }
        
        #endregion


        #region This

        public void Bind(int uid)
        {
            issuancedUniqueIndex = uid; 
            this.ToJsonData();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}