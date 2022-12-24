namespace AutoChess
{
    /// <summary>
    /// 장비 능력 슬롯 중 능력치를 추가하는 슬롯.
    /// </summary>
    public class EquipmentAbilityStatusSlot : IEquipmentStatusSlot, IGetSubAbility
    {
        public IGetSubAbility GetSubAbility;

        public int GetSubAbilityValue(SubAbilityType subAbilityType) =>
            GetSubAbility.GetSubAbilityValue(subAbilityType);
    }
}