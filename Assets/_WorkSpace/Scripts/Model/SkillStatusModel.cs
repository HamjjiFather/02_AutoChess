using System.Collections.Generic;
using MasterData;

namespace AutoChess
{
    public class SkillStatusModel : BaseStatusModel
    {
        #region Fields & Property

        public Dictionary<StatusType, BaseStatusModel> Status { get; set; }

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion


        public SkillStatusModel ()
        {
            Status = new Dictionary<StatusType, BaseStatusModel> ();
        }


        public void SetStatus (Dictionary<StatusType, BaseStatusModel> status)
        {
            Status = status;
        }

        public void AddStatus (StatusType statusType, BaseStatusModel status)
        {
            if (Status.ContainsKey (statusType))
            {
                
            }
        }

        public float GetStatusValue (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType].StatusValue : 0f;
        }

        public void Clear ()
        {
            Status.Clear ();
        }
    }
}