using System.Collections.Generic;
using System.Linq;

namespace AutoChess
{
    /// <summary>
    /// 장비의 베이스 클래스.
    /// </summary>
    public class EquipmentBase : IGetSubAbility
    {
        public EquipmentBase(int uniqueIndex, Equipment equipmentTableData, int slotAmount)
        {
            UniqueIndex = uniqueIndex;
            EquipmentTableData = equipmentTableData;
            AttachedStatusSlots = new List<IEquipmentStatusSlot>(slotAmount);
            SlotLimit = slotAmount;
            SlotIndex = 0;
        }

        #region Fields & Property

        public int UniqueIndex;

        /// <summary>
        /// 장비 테이블 데이터.
        /// </summary>
        public Equipment EquipmentTableData;

        #region Slot

        /// <summary>
        /// 장비의 슬롯.
        /// </summary>
        public List<IEquipmentStatusSlot> AttachedStatusSlots;

        /// <summary>
        /// 현재 슬롯.
        /// </summary>
        public int SlotIndex;

        /// <summary>
        /// 슬롯 제한.
        /// </summary>
        public int SlotLimit;

        /// <summary>
        /// 비어있는 슬롯이 있는지?
        /// </summary>
        public bool RemainSlot => SlotIndex < SlotLimit;
        
        #endregion

        #region

        /// <summary>
        /// 내구도.
        /// </summary>
        public EquipmentDurability EquipmentDurability;

        #endregion

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

        public override string ToString()
        {
            return $"장비(UID: {UniqueIndex}), {EquipmentTableData.Name}";
        }

        #endregion


        #region This

        public void AttachAbilityInSlot(SubAbilityType subAbilityType, int value)
        {
            var slot = new EquipmentAbilityStatusSlot
            {
                GetSubAbility = new SubAbilityComponent(subAbilityType, value)
            };
            AttachedStatusSlots.Add(slot);
            SlotIndex++;
        }
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}