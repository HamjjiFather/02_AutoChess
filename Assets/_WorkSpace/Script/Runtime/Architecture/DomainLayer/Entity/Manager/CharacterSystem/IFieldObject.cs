using UnityEngine;

namespace AutoChess.Service
{
    public interface IFieldObject
    {
        public Vector3Int MyPosition { get; set; }
    }
}