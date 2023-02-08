
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace AutoChess
{
    public class AdventureWorld : MonoBehaviour
    {
        #region Fields & Property

        /// <summary>
        /// 캐릭터가 위치한 숲 타일 맵.
        /// </summary>
        private Tilemap _forestTilemap;

        /// <summary>
        /// 캐릭터가 숲타일 내에 위치할 경우
        /// </summary>
        private Color _inColor = new Color(1, 1, 1, 0.46f);

        #endregion


        #region Methods

        #region Override

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

        #endregion


        #region Event

        #endregion

        #endregion
    }
}