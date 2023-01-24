using UnityEngine;

namespace AutoChess
{
    public partial class BlackSmithBuilding
    {
        #region Fields & Property

        /// <summary>
        /// 기본 장비 내구도 감소 확률.
        /// </summary>
        public const int BaseReduceDurabilityProb = 20000;

        /// <summary>
        /// 내구도 감소시 감소량.
        /// </summary>
        public const int BaseReduceDurabilityAmount = 10000;

        /// <summary>
        /// 장비 내구도 감소 확률
        /// </summary>
        public int GetReduceDurabilityProb
        {
            get
            {
                return BaseReduceDurabilityProb + GetValuePerLevel();

                int GetValuePerLevel()
                {
                    return Level switch
                    {
                        >= 2 and < 4 => -2000,
                        >= 4 and <= BuildingDefine.MaxLevel => -3000,
                        _ => 0
                    };
                }
            }
        }

        #endregion


        #region Methods

        #region Override

        private void Initialize_Repair()
        {
        }


        private void OnLevelUp_Repair()
        {
        }


        public void SpendTime_Repair()
        {
        }

        #endregion


        #region This

        #endregion


        #region Event

        /// <summary>
        /// 내구도 수리.
        /// </summary>
        public void RepairEquipment(EquipmentBase equipmentBase)
        {
            var reduceChance = ProbabilityHelper.Chance(GetReduceDurabilityProb);
            var durability = equipmentBase.EquipmentDurability;
            if (reduceChance)
            {
                var baseDurability = equipmentBase.EquipmentGradeTableData.BaseDurability;
                var reduceAmount =
                    (int) (baseDurability * FormulaHelper.PercentLerp01Unclamped(BaseReduceDurabilityAmount));
                durability.ResetMaxDurability(Mathf.Max(durability.MaxDurability - reduceAmount, 0));
            }

            durability.RecoverDurability();
        }

        #endregion

        #endregion
    }
}