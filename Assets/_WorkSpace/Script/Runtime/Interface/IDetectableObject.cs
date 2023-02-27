using UnityEngine;

namespace AutoChess
{
    public interface IDetectableObject
    {
        Vector3 Position { get; set; }
        
        bool Detect { get; set; }

        void OnDetected();

        void OnFarAway();
    }
}