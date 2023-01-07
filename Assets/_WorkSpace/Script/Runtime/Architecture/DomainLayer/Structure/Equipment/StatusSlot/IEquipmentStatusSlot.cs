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
        EquipmentStatusSlotState EquipmentStatusSlotState { get; }
    }
}