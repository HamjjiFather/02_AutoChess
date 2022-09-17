using AutoChess.Service;

namespace AutoChess
{
    public class CharacterUnit : BehaviourUnit, ICharacterUnit
    {
        #region Fields & Property

        public EquipmentContainer equipmentContainer { get; set; }
        
        public SkillContainer skillContainer { get; set; }

        #endregion


        #region Methods

        #region Override

        
        public virtual float GetAbilityValue(SubAbilities abilityType)
        {
            var baseValue = base.GetAbilityValue(abilityType);
            var equipmentValue = equipmentContainer.GetAbilityValue(abilityType);
            var skillValue = skillContainer.GetAbilityValue(abilityType);
            return baseValue + equipmentValue + skillValue;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion


    }
}