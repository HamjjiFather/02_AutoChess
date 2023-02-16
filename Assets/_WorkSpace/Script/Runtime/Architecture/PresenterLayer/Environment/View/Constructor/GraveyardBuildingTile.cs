namespace AutoChess.Presenter
{
    public class GraveyardBuildingTile : BuildingTileBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override BuildingEntityBase BuildingEntity { get; set; }

        public GraveyardBuildingEntity ToGraveyardBuildingEntity { get; private set; }


        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToGraveyardBuildingEntity = buildingEntity as GraveyardBuildingEntity;
        }

        #endregion


        #region This
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}