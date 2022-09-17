namespace AutoChess.Service
{
    public interface ICharacterUnit
    {
        EquipmentContainer equipmentContainer { get; set; }

        SkillContainer skillContainer { get; set; }
    }
}