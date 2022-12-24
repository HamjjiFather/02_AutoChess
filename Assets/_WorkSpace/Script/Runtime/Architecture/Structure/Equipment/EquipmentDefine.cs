namespace AutoChess
{
    public enum MonsterGrade
    {
        /// <summary>
        /// 신참.
        /// </summary>
        Rookie,

        /// <summary>
        /// 정예.
        /// </summary>
        Elite,

        /// <summary>
        /// 유명한.
        /// </summary>
        Named,

        /// <summary>
        /// 악명높은.
        /// </summary>
        Notorious,

        /// <summary>
        /// 전설적인
        /// </summary>
        Legendary,
    }

    public enum EquipmentDropType
    {
        /// <summary>
        /// 게임에서 여러개 등장하는 장비.
        /// </summary>
        Common = 1,

        /// <summary>
        /// 게임에서 하나만 등장하는 고유 장비.
        /// </summary>
        Unique = 10,
    }

    /// <summary>
    /// 장비 등급.
    /// </summary>
    public enum EquipmentGrade
    {
        /// <summary>
        /// 흔한.
        /// </summary>
        Common,

        /// <summary>
        /// 잘 만들어진.
        /// </summary>
        WellMade,

        /// <summary>
        /// 걸작의. 
        /// </summary>
        MasterPiece,

        /// <summary>
        /// 고유한.
        /// </summary>
        Unique = 10,

        /// <summary>
        /// 전설의.
        /// </summary>
        Legendary,

        /// <summary>
        /// 유물.
        /// </summary>
        Relic
    }


    public static class EquipmentDefine
    {
        // /// <summary>
        // /// 각 등급의 몬스터가 장비를 가질 확률. 십만분율.
        // /// </summary>
        // public static Dictionary<MonsterGrade, int> EquipmentProbCommonMonster = new()
        // {
        //     {MonsterGrade.Rookie, 8000},
        //     {MonsterGrade.Elite, 24000},
        //     {MonsterGrade.Named, 48000},
        //     {MonsterGrade.Notorious, 80000},
        //     {MonsterGrade.Legendary, 100000}
        // };
        //
        // /// <summary>
        // /// 각 등급의 몬스터가 장비를 가졌다면 장비의 등급 확률.
        // /// </summary>
        // public static Dictionary<MonsterGrade, int[]> EquipmentGradeProbOnCommonDropType = new()
        // {
        //     {
        //         MonsterGrade.Rookie, new[] {92000, 8000, 0, 0, 0, 0}
        //     },
        //     {
        //         MonsterGrade.Elite, new[] {50000, 38000, 12000, 0, 0, 0}
        //     },
        //     {
        //         MonsterGrade.Named, new[] {10000, 30000, 45000, 15000, 0, 0}
        //     },
        //     {
        //         MonsterGrade.Notorious, new[] {0, 20000, 40000, 39000, 700, 300}
        //     },
        //     {
        //         MonsterGrade.Legendary, new[] {0, 10000, 50000, 37000, 1500, 500}
        //     }
        // };
        
        // public static Dictionary<EquipmentGrade, >
    }


    public class ProbabilityTable
    {
        
    }
}