using System;
using System.Linq;
using AutoChess.Service;

namespace AutoChess
{
    public enum EquipmentSlotType
    {
        Main,
        Sub,
    }
    
    public class EquipmentContainer : IGetAbilities
    {
        #region Fields & Property

        public EquipmentUnit[] EquipmentUnits = new EquipmentUnit[Enum.GetValues(typeof(EquipmentSlotType)).Length];

        #endregion


        #region Methods

        #region Override

        public float GetAbilityValue(SubAbilities abilityType) =>
            EquipmentUnits.Sum(eu => eu.GetAbilityValue(abilityType));

        #endregion


        #region This

        public void ChangeEquipment(EquipmentSlotType equipmentSlotType, EquipmentUnit equipmentUnit)
        {
            EquipmentUnits[(int) equipmentSlotType] = equipmentUnit;
        }

        #endregion


        #region Event

        #endregion

        #endregion


    }
}