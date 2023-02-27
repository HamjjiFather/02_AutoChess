
using System;
using AutoChess.Presenter;
using KKSFramework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Zenject;

namespace AutoChess.Presenter
{
    public class AdventureField : MonoBehaviour
    {
        #region Fields & Property

        public Camera adventureViewCamera;

        /// <summary>
        /// 캐릭터가 위치한 숲 타일 맵.
        /// </summary>
        private Tilemap _forestTilemap;

        /// <summary>
        /// 캐릭터가 숲타일 내에 위치할 경우
        /// </summary>
        private Color _inColor = new Color(1, 1, 1, 0.46f);

        public BaseBuildingTile baseBuildingTile;
        
        public OutpostBuildingTile[] outpostBuildingTiles;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            outpostBuildingTiles.Foreach(ot => ot.canvas.worldCamera = adventureViewCamera);
        }

        #endregion


        #region This

        public void EnterToForest(Tilemap forestTile)
        {
            _forestTilemap = forestTile;
            _forestTilemap.color = _inColor;
        }


        public void ExitFromForest()
        {
            _forestTilemap.color = Color.white;
            _forestTilemap = default;
        }


        public void Initialize()
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}