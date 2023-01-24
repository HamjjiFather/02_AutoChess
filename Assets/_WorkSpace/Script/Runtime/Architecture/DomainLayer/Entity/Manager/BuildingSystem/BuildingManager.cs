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
        private Dictionary<BuildingType, BuildingBase> _buildingDict = new()
        {
            {BuildingType.BlackSmith, new BlackSmithBuilding()},
            {BuildingType.Warehouse, new WarehouseBuilding()},
            {BuildingType.ExploreOffice, new ExploreOfficeBuilding()},
            {BuildingType.Graveyard, new GraveyardBuilding()},
            {BuildingType.EmploymentOffice, new EmploymentOfficeBuilding()}
        };

        /// <summary>
        /// 전초기지.
        /// </summary>
        private Dictionary<int, OutpostBuilding> _outpostBuildings;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public T GetBuilding<T>() where T : BuildingBase => _buildingDict[ToBuildingType(typeof(T))] as T;


        private BuildingType ToBuildingType(Type type)
        {
            if (type == typeof(BlackSmithBuilding))
                return BuildingType.BlackSmith;
            if (type == typeof(WarehouseBuilding))
                return BuildingType.Warehouse;
            if (type == typeof(ExploreOfficeBuilding))
                return BuildingType.ExploreOffice;
            if (type == typeof(GraveyardBuilding))
                return BuildingType.Graveyard;
            if (type == typeof(EmploymentOfficeBuilding))
                return BuildingType.EmploymentOffice;

            return BuildingType.ExploreOffice;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}