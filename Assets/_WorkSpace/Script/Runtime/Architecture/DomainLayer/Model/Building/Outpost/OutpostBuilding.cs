using Zenject;

namespace AutoChess
{
    /// <summary>
    /// 세계맵에 배치된 전초기지 건물.
    /// </summary>
    public class OutpostBuilding : BuildingBase, IInitializable
    {
        #region Fields & Property

        public int OutpostIndex;

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
            throw new System.NotImplementedException();
        }

        public override void SpendTime()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnLevelUp(int level)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}