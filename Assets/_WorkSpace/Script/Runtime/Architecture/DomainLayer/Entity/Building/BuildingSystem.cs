namespace AutoChess
{
    public class BuildingDefine
    {
        public enum BlackSmithBehaviourType
        {
            Product,
            Enhance,
            Appraisal,
            Purchase,
            Sell,
            Repair,
        }
        
        
        public enum WarehouseBehaviourType
        {
            Store,
        }
        
        /// <summary>
        /// 시작 레벨.
        /// </summary>
        public const int StartLevel = 1;

        /// <summary>
        /// 최대 건물 레벨.
        /// </summary>
        public const int MaxLevel = 5;
        
        /// <summary>
        /// 건물 호감도 레벨별 요구 경험치.
        /// 1 -> 2.
        /// 2 -> 3.
        /// ... -> MaxLevel
        /// </summary>
        public static double[] RequireExp = {
            200, 1000, 2000, 5000,
        };

        /// <summary>
        /// 전초기지 건설 재화 타입.
        /// </summary>
        public const CurrencyType OutpostBuildCostType = CurrencyType.ExploreGoods;
        
        /// <summary>
        /// 전초기지 건설 가격.
        /// </summary>
        public const int OutpostBuildCost = 1000;
    }
}