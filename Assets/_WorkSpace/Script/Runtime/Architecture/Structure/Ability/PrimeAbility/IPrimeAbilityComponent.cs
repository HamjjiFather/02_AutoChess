namespace AutoChess
{
    public interface IPrimeAbilityComponent : IAbilityComponent
    {
        /// <summary>
        /// 주요 능력치 타입.
        /// </summary>
        PrimeAbilityType PrimeAbilityType { get; }
        
        /// <summary>
        /// 투자된 능력치.
        /// </summary>
        int InvestedValue { get; set; }

        /// <summary>
        /// 주요 능력치로 인해 증감된 보조 능력치 리턴.
        /// </summary>
        int GetSubAbilityValue(SubAbilityType subAbilityType);
    }
}