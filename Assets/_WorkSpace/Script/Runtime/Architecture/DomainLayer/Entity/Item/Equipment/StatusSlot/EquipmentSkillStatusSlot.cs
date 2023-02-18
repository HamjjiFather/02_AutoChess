namespace AutoChess
{
    /// <summary>
    /// 장비 능력 슬롯 중 스킬이 추가된 슬롯.
    /// </summary>
    public class EquipmentSkillStatusSlot : IEquipmentStatusSlot
    {
        public int SlotIndex { get; }

        public EquipmentStatusSlotState EquipmentStatusSlotState => EquipmentStatusSlotState.Skill;
    }
}