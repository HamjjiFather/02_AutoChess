using System.Collections.Generic;
using System.Linq;
using KKSFramework.GameSystem;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// 장비의 베이스 클래스.
    /// </summary>
    public class EquipmentEntity : IGetSubAbility, ILevelCore
    {
        public EquipmentEntity(int uniqueIndex, Equipment equipmentTableData, int slotAmount)
        {
            UniqueIndex = uniqueIndex;
            EquipmentTableData = equipmentTableData;
            EquipmentGradeTableData =
                TableDataManager.Instance.EquipmentGradeDict[(int) equipmentTableData.EquipmentGradeType];
            AttachedStatusSlots = new List<IEquipmentStatusSlot>(slotAmount);
            SlotLimit = slotAmount;
            SlotIndex = 0;
            EquipmentDurability = new EquipmentDurability(100, 100);
        }

        #region Fields & Property

        public int UniqueIndex;

        /// <summary>
        /// 강화 레벨.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 최대 강화 레벨.
        /// </summary>
        public int MaxLevel { get; set; } = EquipmentDefine.MaxEnhanceLevel;

        /// <summary>
        /// 장비 테이블 데이터.
        /// </summary>
        public Equipment EquipmentTableData;

        /// <summary>
        /// 장비 등급 테이블 데이터.
        /// </summary>
        public EquipmentGrade EquipmentGradeTableData;

        // 장비 등급.
        public EquipmentGradeType EquipmentGradeType => EquipmentGradeTableData.EquipmentGradeType;

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


        #region Enhance

        public void AddLevel(int levelAmount)
        {
            Level = Mathf.Clamp(Level + levelAmount, 0, MaxLevel);
        }

        #endregion

        #endregion


        #region This

        public void AttachAbilityInSlot(EquipmentAbility equipmentAbility)
        {
            var slot = new EquipmentAbilityStatusSlot(this, equipmentAbility);
            AttachedStatusSlots.Add(slot);
            SlotIndex++;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}