using System.Collections.Generic;
using System.Linq;
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

        [Inject]
        private OutpostBuildingBundle _outpostBuildingBundle;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public IEnumerable<OutpostBuildingDao> ReceiveOutpostDao()
        {
            return TableDataManager.Instance.OutpostDict.Select(kvp =>
            {
                var bundleSet = _outpostBuildingBundle.Load(kvp.Key.ToString());
                var dao = new OutpostBuildingDao(bundleSet, kvp.Value);
                return dao;
            });
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}