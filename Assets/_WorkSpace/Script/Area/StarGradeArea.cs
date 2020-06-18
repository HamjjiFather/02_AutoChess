using BaseFrame;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class StarGradeArea : MonoBehaviour
    {
        #region Fields & Property

        public GameObject[] onStarObj;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetGrade (CharacterGrade grade)
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