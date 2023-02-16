using System;
using UnityEngine.Serialization;
using Zenject;

namespace AutoChess.Presenter
{
    public class BaseEnvironmentController : GameEnvironmentBase
    {
        #region Fields & Property

        [Inject]
        private BuildingManager _buildingManager;

        public ExploreOfficeBuildingTile exploreOfficeBuilding;

        [FormerlySerializedAs("blacksmithBuildingTile")]
        public BlackSmithBuildingTile blackSmithBuildingTile;

        public EmploymentOfficeBuildingTile employmentOfficeBuildingTile;
        
        public GraveyardBuildingTile graveyardBuildingTile;

        public WarehouseBuildingTile warehouseBuildingTile;
        
        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        public override void OnEnvironmentEnabled(EnvironmentParameterBase environmentParameter)
        {
            exploreOfficeBuilding.Initialize(_buildingManager.GetBuilding(BuildingType.ExploreOffice));
            blackSmithBuildingTile.Initialize(_buildingManager.GetBuilding(BuildingType.BlackSmith));
            employmentOfficeBuildingTile.Initialize(_buildingManager.GetBuilding(BuildingType.EmploymentOffice));
            graveyardBuildingTile.Initialize(_buildingManager.GetBuilding(BuildingType.Graveyard));
            warehouseBuildingTile.Initialize(_buildingManager.GetBuilding(BuildingType.Warehouse));
        }

        #endregion

        #endregion
    }
}