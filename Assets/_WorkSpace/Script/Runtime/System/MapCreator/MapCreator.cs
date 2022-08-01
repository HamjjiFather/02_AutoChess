using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AutoChess
{
    public class MapCreator : MonoBehaviour
    {
        #region Fields & Property

        public Tilemap tilemap;

        public Tile tile;
        
        #endregion


        #region Methods

        #region Override

        private void Start()
        {
            CreateMap(5, 5);
        }

        #endregion


        #region This

        public void CreateMap(int x, int y)
        {
            tilemap.SetTile(new Vector3Int(x, y, 1), tile);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}