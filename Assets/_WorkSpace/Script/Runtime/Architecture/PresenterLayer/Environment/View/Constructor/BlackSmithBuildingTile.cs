namespace AutoChess.Presenter
{
    public class BlackSmithBuildingTile : BuildingTileBase
    {
        #region Fields & Property
        
        public override BuildingEntityBase BuildingEntity { get; set; }
        
        public BlackSmithBuildingEntity ToBlackSmithBuildingEntity { get; private set; }

        #endregion


        #region Methods

        #region Override
        
        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToBlackSmithBuildingEntity = buildingEntity as BlackSmithBuildingEntity;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion

    }
}