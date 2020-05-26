using System.Linq;
using UnityEngine;

namespace HexaPuzzle
{
    public class LineElement : MonoBehaviour
    {
        public LandElement[] landElements;

        public LineModel LineModel { get; private set; }

        public LineElement SetLineModel (LineModel lineModel)
        {
            LineModel = lineModel;
            return this;
        }


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