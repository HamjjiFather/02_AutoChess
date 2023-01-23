using AutoChess.Service;

namespace AutoChess
{
    public class CharacterUnit : BehaviourUnit, ICharacterUnit
    {
        #region Fields & Property

        public global::Character CharacterTableData;

        public EquipmentContainer equipmentContainer { get; set; }

        public SkillContainer skillContainer { get; set; }

        #endregion


        #region Methods

        #region Override

        public virtual float GetAbilityValue(SubAbilityType abilityTypeType)
        {
            // var baseValue = base.GetAbilityValue(abilityTypeType);
            // var equipmentValue = equipmentContainer.GetAbilityValue(abilityTypeType);
            // var skillValue = skillContainer.GetAbilityValue(abilityTypeType);
            // return baseValue + equipmentValue + skillValue;
            return default;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}