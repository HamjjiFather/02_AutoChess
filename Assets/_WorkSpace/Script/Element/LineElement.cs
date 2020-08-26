using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public class LineElement : MonoBehaviour
    {
        private readonly List<LandElement> _landElements = new List<LandElement> ();
        public IEnumerable<LandElement> LandElements => _landElements;

        public LandElement GetLandElement (int index)
        {
            return _landElements[index];
        }


        public void AddLandElement (LandElement landElement)
        {
            _landElements.Add (landElement);
        }
    }
}