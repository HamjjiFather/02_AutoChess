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
    public class EquipmentModel : ModelBase, ICombineMaterial
    {
        #region Fields & Property

        public int UniqueEquipmentId;

        public Equipment EquipmentData;

        /// <summary>
        /// 장비 등급.
        /// </summary>
        public EquipmentGrade EquipmentGrade;

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
        public Sprite IconImageResources => ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
            ResourcesType.Equipment, EquipmentData.SpriteResName);

        
        #region Interface Implements

        public int Index => EquipmentData.Id;

        public string NameString => EquipmentData.Name;
        
        public int Grade => (int) EquipmentGrade;

        public Sprite ImageSprite => IconImageResources;
        
        #endregion


#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        public EquipmentModel ()
        {
        }
        
        
        public void Combine ()
        {
            EquipmentGrade += 1;
        }


        public void SetUniqueData (int uid)
        {
            UniqueEquipmentId = uid;
        }


        public void SetEquipmentData (Equipment equipment)
        {
            EquipmentData = equipment;
        }


        public void SetEquipmentGrade (EquipmentGrade starGrade)
        {
            EquipmentGrade = starGrade;
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


        #region Methods

        #endregion
    }
}