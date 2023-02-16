namespace AutoChess.Presenter
{
    public class WarehouseBuildingTile : BuildingTileBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override BuildingEntityBase BuildingEntity { get; set; }

        public WarehouseBuildingEntity ToWarehouseBuildingEntity { get; private set; }


        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToWarehouseBuildingEntity = buildingEntity as WarehouseBuildingEntity;
        }

        #endregion


        #region This
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}