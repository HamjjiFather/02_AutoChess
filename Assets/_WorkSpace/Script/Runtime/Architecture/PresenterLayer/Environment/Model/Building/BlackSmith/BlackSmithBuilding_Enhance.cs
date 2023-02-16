using System.Linq;

namespace AutoChess
{
    public partial class BlackSmithBuildingEntity
    {
        #region Fields & Property

        /// <summary>
        /// 장비 강화에 필요한 기본 내구도.
        /// </summary>
        public const int BaseRequireDurabilityForEnhance = 10;

        /// <summary>
        /// 제작 가능한 장비의 수량.
        /// </summary>
        public int GetReqDurability
        {
            get
            {
                return BaseRequireDurabilityForEnhance + GetAddReqDurability();

                int GetAddReqDurability()
                {
                    return Level switch
                    {
                        >= 2 and < 4 => -4,
                        >= 4 and <= BuildingDefine.MaxLevel => -6,
                        _ => 0
                    };
                }
            }
        }

        #endregion


        #region Methods

        #region Override

        public void Initialize_Enhance()
        {
        }

        protected void OnLevelUp_Enhance()
        {
        }


        public void SpendTime_Enhance()
        {
        }

        #endregion


        #region This

        /// <summary>
        /// 강화 가능 여부.
        /// </summary>
        public bool CanEnhanceEquipment(EquipmentBase equipmentBase)
        {
            // 최대 레벨이 아님.
            var isNotMaxLevel = equipmentBase.MaxLevel.Equals(equipmentBase.Level);
            
            // 내구도가 충분함.
            var enoughDurability = equipmentBase.EquipmentDurability.EnoughDurability(GetReqDurability);
            
            // 모든 슬롯에 감정을 완료함.
            var allSlotAppraisaled = equipmentBase.AttachedStatusSlots.All(ss =>
                ss.EquipmentStatusSlotState != EquipmentStatusSlotState.UnIdentified);

            return isNotMaxLevel && enoughDurability && allSlotAppraisaled;
        }

        #endregion


        #region Event

        /// <summary>
        /// 장비 강화.
        /// </summary>
        public void EnhanceEquipment(EquipmentBase equipmentBase)
        {
            var gt = equipmentBase.EquipmentGradeTableData;
            var amount = gt.BaseReqCurrencyAmountForEnhance + equipmentBase.Level * gt.AddReqCurrencyAmountForEnhance;

            // 레벨업.
            equipmentBase.AddLevel(1);

            // 내구도 감소.
            equipmentBase.EquipmentDurability.Damaged(GetReqDurability);
        }

        #endregion

        #endregion
    }
}