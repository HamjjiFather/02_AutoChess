using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess.Repository
{
    public class OutpostRepository : IRepository
    {
        #region Fields & Property

        [Inject]
        private OutpostBuildingBundle _outpostBuildingBundle;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        public IEnumerable<OutpostBuildingDao> ReceiveDaos()
        {
            return TableDataManager.Instance.OutpostDict.Select(kvp =>
            {
                var bundleSet = _outpostBuildingBundle.Load(kvp.Key.ToString());
                var dao = new OutpostBuildingDao(bundleSet, kvp.Value);
                return dao;
            });
        }


        public void Update(OutpostBuildingDto dto)
        {
            var bundleSet = _outpostBuildingBundle.Load(dto.OutpostIndex);
            bundleSet.extendBuildings = dto.OutpostExtendIndexes.ToArray();
            bundleSet.hasBuilt = true;
            _outpostBuildingBundle.Save(bundleSet);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}