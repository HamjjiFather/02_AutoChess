using UnityEngine;

namespace AutoChess
{
    public class BehaviourUnit : AbilityUnit, IFieldObject
    {
        public Vector3Int MyPosition { get; set; }

        public Vector3Int TargetPosition { get; set; }

        public virtual void DoBehaviour()
        {
            
        }


        public void MoveTo(Vector3Int toPosition)
        {
            
        }
        

        public void DoMovement()
        {
            
        }
    }
}