using System.Collections.Generic;
using Helper;
using KKSFramework.DesignPattern;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using MasterData;

namespace AutoChess
{
    public class EquipmentModel : ModelBase, IStatusModel
    {
        #region Fields & Property
        
        public int UniqueEquipmentId;

        public Equipment EquipmentData;
        
        /// <summary>
        /// 장비 등급.
        /// </summary>
        public StarGrade StarGrade;
        
        /// <summary>
        /// 능력치 인덱스.
        /// </summary>
        public List<int> StatusIndexes = new List<int> ();

        /// <summary>
        /// 능력치.
        /// </summary>
        public List<float> StatusGrades = new List<float> ();

        /// <summary>
        /// 아이콘 이미지 리소스.
        /// </summary>
        public Sprite IconImageResources => ResourcesLoadHelper.LoadResource<Sprite> (ResourceRoleType._Image,
            ResourcesType.Equipment, EquipmentData.SpriteResName);
            
       

#pragma warning disable CS0649

#pragma warning restore CS0649

        public Dictionary<StatusType, BaseStatusModel> Status { get; set; } = new Dictionary<StatusType, BaseStatusModel> ();

        #endregion


        public EquipmentModel ()
        {
        }
        
        
        public void SetUniqueData (int uid)
        {
            UniqueEquipmentId = uid;
        }

        
        public void SetEquipmentData (Equipment equipment)
        {
            EquipmentData = equipment;
        }


        public void SetStarGrade (StarGrade starGrade)
        {
            StarGrade = starGrade;
        }
        
        
        public void SetStatus (Dictionary<StatusType, BaseStatusModel> status)
        {
            Status = status;
        }


        public void SetStatusGrade (List<int> indexes, List<float> grades)
        {
            StatusIndexes = indexes;
            StatusGrades = grades;
        }
        

        public void SetStatusGrade (int index, float grade)
        {
            StatusIndexes.Add (index);
            StatusGrades.Add (grade);
        }


        public BaseStatusModel GetBaseStatusModel (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType] : new BaseStatusModel ();
        }
        
        
        public float GetStatusValue (StatusType statusType)
        {
            return Status.ContainsKey (statusType) ? Status[statusType].StatusValue : 0f;
        }


        #region Methods

        #endregion
    }
}