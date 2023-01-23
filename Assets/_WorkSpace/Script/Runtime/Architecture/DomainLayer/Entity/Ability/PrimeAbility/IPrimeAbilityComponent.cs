namespace AutoChess
{
    public interface IPrimeAbility : IGetSubAbility
    {
        /// <summary>
        /// 주요 능력치 타입.
        /// </summary>
        PrimeAbilityType PrimeAbilityType { get; }

        /// <summary>
        /// 이 주요 능력치로 변동되는 보조 능력치 타입들.
        /// </summary>
        SubAbilityType[] SubAbilityTypes { get; set; }

        /// <summary>
        /// 기본 능력치.
        /// </summary>
        int BaseValue { get; set; }

        /// <summary>
        /// 투자된 능력치.
        /// </summary>
        int InvestedValue { get; set; }
    }
}