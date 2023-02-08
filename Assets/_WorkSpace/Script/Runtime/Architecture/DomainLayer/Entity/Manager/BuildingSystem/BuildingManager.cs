using System;
using System.Collections.Generic;
using AutoChess;
using JetBrains.Annotations;

namespace AutoChess
{
    public enum BuildingType
    {
        /// <summary>
        /// 탐험대.
        /// </summary>
        ExploreOffice,

        /// <summary>
        /// 대장간.
        /// </summary>
        BlackSmith,

        /// <summary>
        /// 고용소.
        /// </summary>
        EmploymentOffice,

        /// <summary>
        /// 창고.
        /// </summary>
        Warehouse,

        /// <summary>
        /// 묘지.
        /// </summary>
        Graveyard,
    }


    [UsedImplicitly]
    public class BuildingManager : ManagerBase
    {
        public BuildingManager()
        {
        }

        #region Fields & Property

        /// <summary>
        /// 건축물.
        /// </summary>
        private Dictionary<BuildingType, BuildingModelBase> _buildingDict = new()
        {
            {BuildingType.BlackSmith, new BlackSmithBuildingModel()},
            {BuildingType.Warehouse, new WarehouseBuildingModel()},
            {BuildingType.ExploreOffice, new ExploreOfficeBuildingModel()},
            {BuildingType.Graveyard, new GraveyardBuildingModel()},
            {BuildingType.EmploymentOffice, new EmploymentOfficeBuildingModel()}
        };

        /// <summary>
        /// 전초기지.
        /// </summary>
        private Dictionary<int, OutpostBuildingModel> _outpostBuildings;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public T GetBuilding<T>() where T : BuildingModelBase => _buildingDict[ToBuildingType(typeof(T))] as T;


        private BuildingType ToBuildingType(Type type)
        {
            if (type == typeof(BlackSmithBuildingModel))
                return BuildingType.BlackSmith;
            if (type == typeof(WarehouseBuildingModel))
                return BuildingType.Warehouse;
            if (type == typeof(ExploreOfficeBuildingModel))
                return BuildingType.ExploreOffice;
            if (type == typeof(GraveyardBuildingModel))
                return BuildingType.Graveyard;
            if (type == typeof(EmploymentOfficeBuildingModel))
                return BuildingType.EmploymentOffice;

            return BuildingType.ExploreOffice;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}