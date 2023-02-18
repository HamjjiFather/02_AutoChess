namespace AutoChess
{
    public enum EquipmentStatusSlotState
    {
        UnIdentified,
        Ability,
        Skill
    }
    
    public interface IEquipmentStatusSlot
    {
        int SlotIndex { get; }

        EquipmentStatusSlotState EquipmentStatusSlotState { get; }
    }
}