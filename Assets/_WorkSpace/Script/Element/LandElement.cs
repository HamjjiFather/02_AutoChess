using KKSFramework.ResourcesLoad;
using UnityEngine;

namespace AutoChess
{
    public class LandElement : PrefabComponent
    {
        public RectTransform rectTransform => GetCachedComponent<RectTransform> ();
        
        public PositionModel PositionModel;
        
        public Transform characterPositionTransform;
    }
}