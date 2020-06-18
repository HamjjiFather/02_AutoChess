using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class StageModel : ModelBase
    {
        #region Fields & Property

        public Stage StageData;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion

        public void SetStageData (Stage stage)
        {
            StageData = stage;
        }


        #region Methods

        #endregion
    }
}