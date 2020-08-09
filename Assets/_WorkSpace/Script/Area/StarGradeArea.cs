using KKSFramework;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    public class StarGradeArea : AreaBase<StarGrade>
    {
        #region Fields & Property

        public GameObject[] onStarObj;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (StarGrade grade)
        {
            onStarObj.Foreach ((obj, index) =>
            {
                obj.SetActive (index <= (int)grade);
            });
        }
        
        #endregion


        #region EventMethods

        #endregion
    }
}