using System.Collections.Generic;

namespace AutoChess
{
    public class AbilityContainer
    {
        /// <summary>
        /// 주요 능력치.
        /// </summary>
        protected Dictionary<string, IAbilityComposite> AbilityContainers { get; set; }
        
        public virtual float GetAbilityValue(string abilityType) => GetAbilityComposite(abilityType).GetAbilityValue();


        public virtual float GetNumberValue(string abilityType) =>
            GetAbilityComposite(abilityType).NumberValue.GetValue();


        public virtual float GetPercentValue(string abilityType) =>
            GetAbilityComposite(abilityType).PercentValue.GetValue();

        public virtual IAbilityComposite GetAbilityComposite(string abilityType)
        {
            return !AbilityContainers.ContainsKey(abilityType) ? default : AbilityContainers[abilityType];
        }
    }
}