using System;
using System.Collections.Generic;
using System.Linq;
using AutoChess;
using AutoChess.Repository;
using JetBrains.Annotations;
using KKSFramework;
using Zenject;

namespace AutoChess
{
    [UsedImplicitly]
    public class BuildingManager : IManagerBase
    {
        public BuildingManager()
        {
        }

        #region Fields & Property


        [Inject]
        private BuildingRepository _buildingRepository;

        /// <summary>
        /// 건축물.
        /// </summary>
        private readonly Dictionary<BuildingType, BuildingEntityBase> _buildingEntityMap = new()
        {
            {
                BuildingType.BlackSmith,
                new BlackSmithBuildingEntity(TableDataManager.Instance.BaseDict[(int) BuildingType.BlackSmith])
            },
            {
                BuildingType.Warehouse,
                new WarehouseBuildingEntity(TableDataManager.Instance.BaseDict[(int) BuildingType.Warehouse])
            },
            {
                BuildingType.ExploreOffice,
                new ExploreOfficeBuildingEntity(TableDataManager.Instance.BaseDict[(int) BuildingType.ExploreOffice])
            },
            {
                BuildingType.Graveyard,
                new GraveyardBuildingEntity(TableDataManager.Instance.BaseDict[(int) BuildingType.Graveyard])
            },
            {
                BuildingType.EmploymentOffice,
                new EmploymentOfficeBuildingEntity(
                    TableDataManager.Instance.BaseDict[(int) BuildingType.EmploymentOffice])
            }
        };

        /// <summary>
        /// 전초기지.
        /// </summary>
        private Dictionary<int, OutpostBuildingEntity> _outpostBuildings;

        public Dictionary<int, OutpostBuildingEntity> OutpostBuildingEntities => _outpostBuildings;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
            _buildingEntityMap.Values.Foreach(entity => entity.Initialize());
            var outpostDao = _buildingRepository.ReceiveOutpostDao();
            _outpostBuildings =
                outpostDao.ToDictionary(x => x.Index, x => new OutpostBuildingEntity(x.OutpostTableData));
        }

        #endregion


        #region This

        public BuildingEntityBase GetBuilding(BuildingType buildingType) => _buildingEntityMap[buildingType];


        private BuildingType ToBuildingType(Type type)
        {
            if (type == typeof(BlackSmithBuildingEntity))
                return BuildingType.BlackSmith;
            if (type == typeof(WarehouseBuildingEntity))
                return BuildingType.Warehouse;
            if (type == typeof(ExploreOfficeBuildingEntity))
                return BuildingType.ExploreOffice;
            if (type == typeof(GraveyardBuildingEntity))
                return BuildingType.Graveyard;
            if (type == typeof(EmploymentOfficeBuildingEntity))
                return BuildingType.EmploymentOffice;

            return BuildingType.ExploreOffice;
        }
        
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}