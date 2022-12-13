using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace KKSFramework.GameSystem
{
    public enum HexagonalType
    {
        FlatTop,
        PointTop
    }

    public class HexagonalAStarPathFinder
    {
        #region Fields & Property

        public const HexagonalType HexagonShapeBase = HexagonalType.PointTop;

        private Tilemap _tilemap;

        private readonly Dictionary<Vector3Int, List<Vector3Int>> _aroundPositions = new();

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void SetTileMap(Tilemap tilemap)
        {
            _tilemap = tilemap;
        }


        /// <summary>
        /// 길 찾기.
        /// </summary>
        public List<Vector3Int> GetPathPoints(Vector3Int origin, Vector3Int target)
        {
            var onFinding = true;
            var checkedPoints = new List<Vector3Int>()
            {
                origin
            };

            while (onFinding)
            {
                TryGetPathPoints(origin);
            }

            return BackTracking(checkedPoints, checkedPoints.Last(), origin);

            void TryGetPathPoints(Vector3Int current)
            {
                var neighbor = Neighbors(current).Except(checkedPoints).OrderBy(v3I => Distance(target, v3I)).ToArray();

                if (neighbor.Contains(target))
                {
                    checkedPoints.Add(target);
                    onFinding = false;
                }
                else
                {
                    checkedPoints.AddRange(neighbor);
                    var nearestNeighbor = neighbor.First();
                    TryGetPathPoints(nearestNeighbor);
                }
            }
        }


        /// <summary>
        /// 찾아진 경로에 대해서 백트래킹을 실시하여 최종 루트를 찾아냄.
        /// </summary>
        public List<Vector3Int> BackTracking(List<Vector3Int> foundPoints, Vector3Int origin, Vector3Int targetPoint)
        {
            var onFinding = true;
            var newPathPoints = new List<Vector3Int>();

            while (onFinding)
            {
                TryGetPathPoints(origin);
            }

            void TryGetPathPoints(Vector3Int current)
            {
                // 주변 위치중 찾아진 경로에 대해서만. 
                var neighbor = Neighbors(current)
                    .Where(foundPoints.Contains)
                    .Except(newPathPoints)
                    .OrderBy(v3I => Distance(targetPoint, v3I))
                    .ToArray();

                if (neighbor.Contains(targetPoint))
                {
                    newPathPoints.Add(targetPoint);
                    onFinding = false;
                }
                else
                {
                    var nearestNeighbor = neighbor.First();
                    newPathPoints.Add(nearestNeighbor);
                    TryGetPathPoints(nearestNeighbor);
                }
            }

            return newPathPoints;
        }


        public int Distance(Vector3Int a, Vector3Int b)
        {
            var dist = Math.Max(Math.Abs(Math.Abs(a.x) - Math.Abs(b.x)), Math.Abs(Math.Abs(a.y) - Math.Abs(b.y)));
            return dist;
        }


        public List<Vector3Int> Neighbors(Vector3Int point)
        {
            if (!_aroundPositions.ContainsKey(point))
            {
                var list = new List<Vector3Int>
                {
                    point + new Vector3Int(0, 1, 0),
                    point + new Vector3Int(1, 0, 0),
                    point + new Vector3Int(0, -1, 0),
                    point + new Vector3Int(-1, -1, 0),
                    point + new Vector3Int(-1, 0, 0),
                    point + new Vector3Int(-1, 1, 0)
                };
                _aroundPositions.Add(point, list);
            }

            return _aroundPositions[point];
        }


        // public Tile GetTile(Vector3Int point)
        // {
        //     var tile = _tilemap.GetTile(point);
        //     tile.
        //     
        // }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}