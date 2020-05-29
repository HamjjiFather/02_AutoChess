using System.Linq;
using UnityEngine;

namespace HexaPuzzle
{
    public class LineElement : MonoBehaviour
    {
        public LandElement[] landElements;

        public PuzzleLineModel PuzzleLineModel { get; private set; }


        private void Awake ()
        {
            landElements.Foreach (x => x.gameObject.SetActive (false));
        }
        
        
        public LineElement SetLineModel (PuzzleLineModel puzzleLineModel)
        {
            PuzzleLineModel = puzzleLineModel;
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