using System.Linq;

namespace AutoChess
{
    /// <summary>
    /// 장비의 베이스 클래스.
    /// </summary>
    public class EquipmentBase : IGetSubAbility
    {
        public EquipmentBase(Equipment equipmentTableData)
        {
            EquipmentTableData = equipmentTableData;
        }

        #region Fields & Property

        /// <summary>
        /// 장비 테이블 데이터.
        /// </summary>
        public Equipment EquipmentTableData;

        /// <summary>
        /// 장비의 슬롯.
        /// </summary>
        public IEquipmentStatusSlot[] AttachedAbilities;

        #endregion


        #region Methods

        #region Override

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            var sum = AttachedAbilities
                .OfType<EquipmentAbilityStatusSlot>()
                .Sum(aa => aa.GetSubAbilityValue(subAbilityType));
            return sum;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}