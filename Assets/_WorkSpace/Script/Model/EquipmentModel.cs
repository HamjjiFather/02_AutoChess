using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class EquipmentModel : ModelBase, IStatusModel
    {
        #region Fields & Property
        
        public int UniqueEquipmentId;

#pragma warning disable CS0649

#pragma warning restore CS0649

        public Dictionary<StatusType, BaseStatusModel> Status { get; set; } = new Dictionary<StatusType, BaseStatusModel> ();

        #endregion


        public EquipmentModel ()
        {
        }


        public void SetStatus (Dictionary<StatusType, BaseStatusModel> status)
        {
            Status = status;
        }


        public float GetStatusValue (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType].StatusValue : 0f;
        }


        #region Methods

        #endregion
    }
}