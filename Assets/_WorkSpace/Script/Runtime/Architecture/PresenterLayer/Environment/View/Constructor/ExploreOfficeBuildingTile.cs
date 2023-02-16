namespace AutoChess.Presenter
{
    public class ExploreOfficeBuildingTile : BuildingTileBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override BuildingEntityBase BuildingEntity { get; set; }

        public ExploreOfficeBuildingEntity ToOfficeBuildingEntity { get; private set; }


        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToOfficeBuildingEntity = buildingEntity as ExploreOfficeBuildingEntity;
        }

        #endregion


        #region This
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}