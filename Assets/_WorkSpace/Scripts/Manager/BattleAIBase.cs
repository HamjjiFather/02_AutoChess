namespace AutoChess
{
    public class BattleAIBase
    {
        public MovementAIType movementAIType;

        public TargetPriority targetPriority;
    }
    

    public enum MovementAIType
    {
        /// <summary>
        /// 통상적 움직임.
        /// </summary>
        General,
        
        /// <summary>
        /// 대상의 뒤로 움직임.
        /// </summary>
        BehindTo,
    }


    public enum TargetPriority
    {
        /// <summary>
        /// 가장 가까운 적.
        /// </summary>
        Nearest,

        /// <summary>
        /// 가장 먼 적.
        /// </summary>
        Farthest,
        
        /// <summary>
        /// 힘 계열 먼저.
        /// </summary>
        PowerCharacter,
        
        /// <summary>
        /// 민첩 계열 먼저.
        /// </summary>
        DexterityCharacter,
        
        /// <summary>
        /// 지능 계열 먼저.
        /// </summary>
        IntelligenceCharacter,
    }
}