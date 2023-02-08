using System.Linq;

namespace AutoChess
{
    public partial class BlackSmithBuildingModel
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public void Initialize_Appraisal()
        {
        }

        protected void OnLevelUp_Appraisal()
        {
        }


        public void SpendTime_Appraisal()
        {
        }

        #endregion


        #region This

        /// <summary>
        /// 감정 가능 여부.
        /// </summary>
        public bool CanAppraisalEquipment(EquipmentBase equipmentBase)
        {
            var anyExistEmptySlot = equipmentBase.RemainSlot;
            return anyExistEmptySlot;
        }

        #endregion


        #region Event

        /// <summary>
        /// 장비 감정.
        /// </summary>
        public void AppraisalEquipment(EquipmentBase equipmentBase)
        {
            if (CanAppraisalEquipment(equipmentBase))
            {
                EquipmentGenerator.OpenEquipmentSlot(equipmentBase);
            }
        }

        #endregion

        #endregion
    }
}