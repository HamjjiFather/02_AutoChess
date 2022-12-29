using System.Linq;

namespace AutoChess
{
    /// <summary>
    /// 장비의 베이스 클래스.
    /// </summary>
    public class EquipmentBase : IGetSubAbility
    {
        public EquipmentBase(Equipment equipmentTableData, int slotAmount)
        {
            EquipmentTableData = equipmentTableData;
            AttachedStatusSlots = new UnIdentifiedEquipmentStatusSlot[slotAmount];
            SlotIndex = 0;
        }

        #region Fields & Property

        /// <summary>
        /// 장비 테이블 데이터.
        /// </summary>
        public Equipment EquipmentTableData;

        /// <summary>
        /// 장비의 슬롯.
        /// </summary>
        public IEquipmentStatusSlot[] AttachedStatusSlots;

        /// <summary>
        /// 현재 슬롯.
        /// </summary>
        public int SlotIndex;

        /// <summary>
        /// 비어있는 슬롯이 있는지?
        /// </summary>
        public bool RemainSlot => SlotIndex < AttachedStatusSlots.Length;

        #endregion


        #region Methods

        #region Override

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            var sum = AttachedStatusSlots
                .OfType<EquipmentAbilityStatusSlot>()
                .Sum(aa => aa.GetSubAbilityValue(subAbilityType));
            return sum;
        }

        #endregion


        #region This

        public void AttachAbilityInSlot(SubAbilityType subAbilityType, int value)
        {
            var slot = new EquipmentAbilityStatusSlot
            {
                GetSubAbility = new SubAbilityComponent(subAbilityType, value)
            };
            AttachedStatusSlots[SlotIndex++] = slot;
        }
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}