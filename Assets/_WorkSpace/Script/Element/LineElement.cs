using System.Linq;
using UnityEngine;

namespace AutoChess
{
    public class LineElement : MonoBehaviour
    {
        public LandElement[] landElements;


        public LandElement GetLandElement (int row)
        {
            return landElements.Reverse ().ToList ()[row];
        }


        public bool IsContainRow (int row)
        {
            return Enumerable.Range (0, landElements.Length).Contains (row);
        }
    }
}