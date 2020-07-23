using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoChess
{
    /// <summary>
    /// 라인 체크 방향.
    /// </summary>
    public enum CheckDirectionTypes
    {
        ToUpward = 180,
        ToUpRight = 240,
        ToDownRight = 300,
        ToDownward = 0,
        ToDownLeft = 60,
        ToUpLeft = 120
    }
    
    public class PositionHelper : Singleton<PositionHelper>
    {
        #region Fields & Property
        
        public PositionModel EmptyPosition { get; } = new PositionModel (-1, -1);

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        
        /// <summary>
        /// 해당 위치를 포함한 6방향의 위치를 리턴.
        /// </summary>
        public IEnumerable<PositionModel> GetAroundPositionModelWith (Dictionary<int, List<LandModel>> landDict, PositionModel positionModel)
        {
            return new[]
            {
                positionModel,
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownRight)
            };
        }
        
        
        /// <summary>
        /// 해당 위치에서 6방향의 위치를 리턴.
        /// </summary>
        public IEnumerable<PositionModel> GetAroundPositionModel (Dictionary<int, List<LandModel>> landDict, PositionModel positionModel)
        {
            return new[]
            {
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownRight)
            };
        }
        
        
        /// <summary>
        /// 해당 enum 타입 방향에 있는 바로 옆 퍼즐의 키 값을 리턴.
        /// </summary>
        public PositionModel GetPositionByDirectionType (Dictionary<int, List<LandModel>> landDict, PositionModel positionModel,
            CheckDirectionTypes checkDirectionTypes)
        {
            return GetKeyByAngle (landDict, positionModel, (float) checkDirectionTypes);
        }


        /// <summary>
        /// 해당 방향에 있는 바로 옆 퍼즐의 키 값을 리턴. 
        /// </summary>
        public PositionModel GetKeyByAngle (Dictionary<int, List<LandModel>> landDict, PositionModel positionModel, float angle)
        {
            if (!landDict.ContainsKey (positionModel.Column))
            {
                return EmptyPosition;
            }

            // To upper direction.
            if (Enumerable.Range (150, 60).Contains ((int) angle))
            {
                return new PositionModel (positionModel.Column, positionModel.Row + 1);
            }

            // To upper right direction.
            if (Enumerable.Range (210, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (false);
            }

            // To lower right direction.
            if (Enumerable.Range (270, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (false, false);
            }

            // To upper left direction.
            if (Enumerable.Range (90, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition ();
            }

            // To lower left direction.
            if (Enumerable.Range (30, 60).Contains ((int) angle))
            {
                return GetPuzzlePosition (toUpper: false);
            }

            // To lower direction.
            return new PositionModel (positionModel.Column, positionModel.Row - 1);

            PositionModel GetPuzzlePosition (bool toLeft = true, bool toUpper = true)
            {
                var checkColumn = positionModel.Column + (toLeft ? -1 : 1);

                if (!landDict.ContainsKey (checkColumn))
                    return EmptyPosition;

                var isLargeColumn = landDict[positionModel.Column].Count > landDict[checkColumn].Count;
                var posCoeff = isLargeColumn && toUpper ? 0 : isLargeColumn ? -1 : toUpper ? 1 : 0;

                return new PositionModel (checkColumn, positionModel.Row + posCoeff);
            }
        }
        
        
        public float Distance (Dictionary<int, List<LandModel>> landDict, PositionModel checkPosition, PositionModel targetPosition)
        {
            if (!(landDict.ContainsKey (targetPosition.Column) &&
                  landDict[targetPosition.Column].ContainIndex (targetPosition.Row)))
            {
                return float.MaxValue;
            }

            var coeffValue = Mathf.Min (landDict[checkPosition.Column].Count % 2, 0.5f);
            return Mathf.Abs (checkPosition.Row - targetPosition.Row) +
                Mathf.Abs (checkPosition.Column - targetPosition.Column) - coeffValue;
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}