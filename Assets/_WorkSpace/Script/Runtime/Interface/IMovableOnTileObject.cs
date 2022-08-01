using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public interface IMovableOnTileObject : IOnTileObject
    {
        public List<Vector3Int> PathPoints { get; set; }

        public void GoTo(IMovableOnTileObject movableObject);
    }
}