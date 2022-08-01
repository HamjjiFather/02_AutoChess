using UnityEngine;

namespace AutoChess
{
    public interface IOnTileObject
    {
        public Vector3Int GridPosition { get; set; }
    }
}