using System.Collections.Generic;
using System.Linq;
using Helper;
using KKSFramework.DesignPattern;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using MasterData;
using ResourcesLoad;

namespace AutoChess
{
    public class EquipmentModel : ModelBase
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

        /// <summary>
        /// 능력치.
        /// </summary>
        public List<BaseStatusModel> StatusList { get; set; } = new List<BaseStatusModel> ();

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


        public void SetStatus (IEnumerable<BaseStatusModel> status)
        {
            StatusList.AddRange (status);
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
            return StatusList.Any (x => x.StatusData.StatusType.Equals (statusType))
                ? StatusList.First (x => x.StatusData.StatusType.Equals (statusType))
                : new BaseStatusModel ();
        }


        public float GetStatusValue (StatusType statusType)
        {
            return StatusList.Any (x => x.StatusData.StatusType.Equals (statusType))
                ? StatusList.Where (x => x.StatusData.StatusType.Equals (statusType)).Sum (x => x.StatusValue)
                : 0f;
        }


        #region Methods

        #endregion
    }
}