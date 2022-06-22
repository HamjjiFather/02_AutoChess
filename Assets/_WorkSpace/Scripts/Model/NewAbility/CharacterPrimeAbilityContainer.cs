using System.Collections.Generic;

namespace AutoChess
{
    /// <summary>
    /// 주요 능력치.
    /// </summary>
    public enum PrimAbilities
    {
        Body,
        Skill,
        Meltality,
        Wisdom,
        Speed
    }
    
    public class CharacterPrimeAbilityContainer : CharacterAbilityContainer
    {
        #region Fields & Property
        
        /// <summary>
        /// 주요 능력치.
        /// </summary>
        private readonly Dictionary<string, IAbilityComposite> _abilityContainers = new Dictionary<string, IAbilityComposite>();

        #endregion


        #region Methods

        #region Override

        public override float GetAbilityValue(string abilityType) => base.GetAbilityComposite(abilityType).GetAbilityValue();


        public override float GetNumberValue(string abilityType) => base.GetAbilityComposite(abilityType).NumberValue.GetValue();
        
        
        public override float GetPercentValue(string abilityType) => base.GetAbilityComposite(abilityType).PercentValue.GetValue();
        
        public override IAbilityComposite GetAbilityComposite(string abilityType)
        {
            return base.GetAbilityComposite(abilityType);
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}