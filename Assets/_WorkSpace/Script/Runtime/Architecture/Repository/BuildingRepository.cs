using AutoChess.Bundle;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess.Repository
{
    public class BuildingRepository : IRepository
    {
        #region Fields & Property

        [Inject]
        private BaseBuildingBundle _baseBuildingBundle;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}