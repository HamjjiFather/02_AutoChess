using UnityEngine;

namespace AutoChess
{
    public class EquipmentModel : ICombineMaterial
    {
        #region Fields & Property

        // public int UniqueEquipmentId;
        //
        // /// <summary>
        // /// 장비 등급.
        // /// </summary>
        // public EquipmentGrade EquipmentGrade;
        //
        // /// <summary>
        // /// 능력치 인덱스.
        // /// </summary>
        // public List<int> StatusIndexes = new List<int> ();
        //
        // /// <summary>
        // /// 능력치.
        // /// </summary>
        // public List<float> StatusGrades = new List<float> ();
        //
        // /// <summary>
        // /// 아이콘 이미지 리소스.
        // /// </summary>
        // public Sprite IconImageResources => ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
        //     ResourcesType.Equipment, EquipmentData.SpriteResName);
        //
        //
        // #region Interface Implements
        //
        // public int Index => EquipmentData.Id;
        //
        // public string NameString => EquipmentData.Name;
        //
        // public int Grade => (int) EquipmentGrade;
        //
        // public Sprite ImageSprite => IconImageResources;
        
        public int Index { get; }
        
        public string NameString { get; }
        
        public int Grade { get; }
        
        public Sprite ImageSprite { get; }
        
        #endregion


#pragma warning disable CS0649

#pragma warning restore CS0649


        public EquipmentModel ()
        {
        }
        
        
        public void Combine ()
        {
            // EquipmentGrade += 1;
        }

        
        #region Methods

        #endregion
    }
}