using Zenject;

namespace AutoChess
{
    public class GraveyardBuildingEntity : BuildingEntityBase, IInitializable
    {
        public GraveyardBuildingEntity(Building buildingTableData) : base(buildingTableData)
        {
        }

        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
        }

        public override void SpendTime()
        {
        }

        protected override void OnLevelUp(int level)
        {
        }

        public override void Initialize()
        {
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}