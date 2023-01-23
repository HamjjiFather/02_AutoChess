using System.Collections.Generic;
using AutoChess.Service;
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

        #endregion


        #region Event

        #endregion

        #endregion
    }
}