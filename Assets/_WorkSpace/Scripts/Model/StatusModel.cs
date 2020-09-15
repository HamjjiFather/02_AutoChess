using System.Collections.Generic;
using KKSFramework.DesignPattern;
using MasterData;

namespace AutoChess
{
    public interface IStatusModel
    {
        Dictionary<StatusType, BaseStatusModel> Status { set; get; }
        
        void SetStatus (Dictionary<StatusType, BaseStatusModel> status);

        float GetStatusValue (StatusType statusType);
    }
    
    
    public class StatusModel : ModelBase, IStatusModel
    {
        #region Fields & Property
        
        public Dictionary<StatusType, BaseStatusModel> Status { get; set; }
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        private float _maxHealth;

        public float MaxHealth => _maxHealth;

        #endregion


        #region Methods

        #endregion



        public void SetStatus (Dictionary<StatusType, BaseStatusModel> status)
        {
            Status = status;
            _maxHealth = Status[StatusType.Health].StatusValue;
        }

        
        public void SetNewStatusGradeValue (StatusType statusType, float gradeValue)
        {
            GetBaseStatusModel(statusType).SetGradeValue (gradeValue);
        }

        
        public BaseStatusModel GetBaseStatusModel (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType] : new BaseStatusModel ();
        }


        public float GetStatusGradeValue (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType].GradeValue : 0f;
        } 
        
        public float GetStatusValue (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType].StatusValue : 0f;
        }
    }
}