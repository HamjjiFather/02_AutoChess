namespace AutoChess.Presenter
{
    public class EmploymentOfficeBuildingTile : BuildingTileBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override BuildingEntityBase BuildingEntity { get; set; }

        public EmploymentOfficeBuildingEntity ToEmploymentOfficeBuildingEntity { get; private set; }


        public override void Initialize(BuildingEntityBase buildingEntity)
        {
            base.Initialize(buildingEntity);
            ToEmploymentOfficeBuildingEntity = buildingEntity as EmploymentOfficeBuildingEntity;
        }

        #endregion


        #region This
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}