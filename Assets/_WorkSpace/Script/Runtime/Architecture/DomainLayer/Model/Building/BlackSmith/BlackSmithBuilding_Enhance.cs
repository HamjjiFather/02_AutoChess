namespace AutoChess
{
    public partial class BlackSmithBuilding
    {
        #region Fields & Property
        
        /// <summary>
        /// 장비 강화에 필요한 기본 내구도.
        /// </summary>
        public const int BaseRequireDurabilityForEnhance = 20;
        
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
            var isNotMaxLevel = equipmentBase.MaxLevel.Equals(equipmentBase.Level);
            var enoughDurability = equipmentBase.EquipmentDurability.EnoughDurability(GetReqDurability);
            
            return isNotMaxLevel && enoughDurability;
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