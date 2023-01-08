using KKSFramework.GameSystem;

namespace AutoChess
{
    /// <summary>
    /// 장비 능력 슬롯 중 능력치를 추가하는 슬롯.
    /// </summary>
    public class EquipmentAbilityStatusSlot : IEquipmentStatusSlot, IGetSubAbility
    {
        public EquipmentAbilityStatusSlot(ILevelCore level, EquipmentAbility equipmentAbility)
        {
            _levelBase = level;
            _equipmentAbilityTableData = equipmentAbility;
        }

        private ILevelCore _levelBase;

        private EquipmentAbility _equipmentAbilityTableData;

        public EquipmentStatusSlotState EquipmentStatusSlotState => EquipmentStatusSlotState.Ability;

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            if (_equipmentAbilityTableData.AbilityType.Equals(subAbilityType))
                return _equipmentAbilityTableData.BaseValue +
                       _levelBase.Level * _equipmentAbilityTableData.AddValuePerLevel;
            return default;
        }

        public override string ToString() =>
            $"장비 슬롯 레벨: {_levelBase.Level}, 능력치 타입: {_equipmentAbilityTableData.AbilityType}, 능력치: {GetSubAbilityValue(_equipmentAbilityTableData.AbilityType)}";
    }
}