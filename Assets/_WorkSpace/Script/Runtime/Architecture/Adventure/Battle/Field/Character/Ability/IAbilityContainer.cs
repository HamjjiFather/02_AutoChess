using System.Collections.Generic;

namespace AutoChess
{
    public interface IAbilityContainer
    {
        /// <summary>
        /// 주요 능력치.
        /// </summary>
        Dictionary<string, IAbilityComposite> AbilityContainers { get; set; }
        
        float GetAbilityValue(string abilityType) => GetAbilityComposite(abilityType).GetAbilityValue();


        float GetNumberValue(string abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        float GetPercentValue(string abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();


        IAbilityComposite GetAbilityComposite(string abilityType)
        {
            return !AbilityContainers.ContainsKey(abilityType) ? default : AbilityContainers[abilityType];
        }
    }
}