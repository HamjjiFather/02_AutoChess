using UnityEngine;

namespace AutoChess
{
    public interface IFieldObject
    {
        public Vector3Int MyPosition { get; set; }
    }
}