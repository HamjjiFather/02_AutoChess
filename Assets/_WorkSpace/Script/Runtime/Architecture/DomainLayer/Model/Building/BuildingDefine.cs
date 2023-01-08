namespace AutoChess
{
    public class BuildingDefine
    {
        #region Fields & Property

        /// <summary>
        /// 최대 건물 레벨.
        /// </summary>
        public const int MaxLevel = 5;
        
        /// <summary>
        /// 건물 호감도 레벨별 요구 경험치.
        /// </summary>
        public static double[] RequireExp = new double[MaxLevel]{
            200, 1000, 2000, 5000, 10000,
        };
        

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}