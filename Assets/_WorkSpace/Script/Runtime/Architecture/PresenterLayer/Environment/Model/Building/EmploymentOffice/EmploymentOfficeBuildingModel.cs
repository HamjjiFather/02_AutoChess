using Zenject;

namespace AutoChess
{
    public partial class EmploymentOfficeBuildingModel : BuildingModelBase, IInitializable
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
            Build_Employ();
            Build_Evaluation();
            Build_Dismissal();
        }

        public override void SpendTime()
        {
            SpendTime_Employ();
            SpendTime_Evaluation();
            SpendTime_Dismissal();
        }

        protected override void OnLevelUp(int level)
        {
            OnLevelUp_Employ(level);
            OnLevelUp_Evaluation(level);
            OnLevelUp_Dismissal(level);
        }

        public void Initialize()
        {
            Initialize_Employ();
            Initialize_Evaluation();
            Initialize_Dismissal();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}