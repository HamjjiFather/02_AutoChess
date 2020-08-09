using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using ModestTree;
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
        public IEnumerable<PositionModel> GetAroundPositionModelWith<T> (Dictionary<int, List<T>> landDict,
            PositionModel positionModel) where T : LandModel
        {
            var aroundPosition = new List<PositionModel>
            {
                positionModel,
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownRight)
            };

            aroundPosition.RemoveAll (x => x.Equals (EmptyPosition));
            return aroundPosition;
        }


        public IEnumerable<PositionModel> GetAroundPositionModel<T> (Dictionary<int, List<T>> landDict,
            PositionModel positionModel, int range = 1) where T : LandModel
        {
            var resultPositions = new List<PositionModel> {positionModel};

            for (var i = 0; i < range; i++)
            {
                var aroundPosition = resultPositions
                    .SelectMany (x => GetAroundPositionModel (landDict, x))
                    .Distinct ()
                    .Except (resultPositions)
                    .Except (EmptyPosition).ToList ();
                resultPositions.AddRange (aroundPosition);
            }

            return resultPositions;
        }


        /// <summary>
        /// 해당 위치에서 6방향의 위치를 리턴.
        /// </summary>
        public IEnumerable<PositionModel> GetAroundPositionModel<T> (Dictionary<int, List<T>> landDict,
            PositionModel positionModel) where T : LandModel
        {
            var aroundPosition = new List<PositionModel>
            {
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (landDict, positionModel, CheckDirectionTypes.ToDownRight)
            };

            aroundPosition.RemoveAll (x => x.Equals (EmptyPosition));
            return aroundPosition;
        }


        /// <summary>
        /// 해당 enum 타입 방향에 있는 바로 옆 퍼즐의 키 값을 리턴.
        /// </summary>
        public PositionModel GetPositionByDirectionType<T> (Dictionary<int, List<T>> landDict,
            PositionModel positionModel,
            CheckDirectionTypes checkDirectionTypes) where T : LandModel
        {
            return GetKeyByAngle (landDict, positionModel, (float) checkDirectionTypes);
        }


        /// <summary>
        /// 해당 방향에 있는 바로 옆 퍼즐의 키 값을 리턴. 
        /// </summary>
        public PositionModel GetKeyByAngle<T> (Dictionary<int, List<T>> landDict, PositionModel positionModel,
            float angle) where T : LandModel
        {
            if (!landDict.ContainsKey (positionModel.Column))
            {
                return EmptyPosition;
            }

            PositionModel returnPositionModel;

            switch ((int)angle)
            {
                case var _ when Enumerable.Range (150, 60).Contains ((int) angle):
                    returnPositionModel = new PositionModel (positionModel.Column, positionModel.Row + 1);
                    break;
                
                case var _ when Enumerable.Range (210, 60).Contains ((int) angle):
                    returnPositionModel = GetFieldPosition (false);
                    break;
                
                case var _ when Enumerable.Range (270, 60).Contains ((int) angle):
                    returnPositionModel = GetFieldPosition (false, false);
                    break;
                
                case var _ when Enumerable.Range (90, 60).Contains ((int) angle):
                    returnPositionModel = GetFieldPosition ();
                    break;
                
                case var _ when Enumerable.Range (30, 60).Contains ((int) angle):
                    returnPositionModel = GetFieldPosition (toUpper: false);
                    break;
                
                default:
                    returnPositionModel = new PositionModel (positionModel.Column, positionModel.Row - 1);
                    break;
            }

            return ContainPosition (landDict, returnPositionModel) ? returnPositionModel : EmptyPosition;

            PositionModel GetFieldPosition (bool toLeft = true, bool toUpper = true)
            {
                var checkColumn = positionModel.Column + (toLeft ? -1 : 1);

                if (!landDict.ContainsKey (checkColumn))
                    return EmptyPosition;

                var isLargeColumn = landDict[positionModel.Column].Count > landDict[checkColumn].Count;
                var posCoeff = isLargeColumn && toUpper ? 0 : isLargeColumn ? -1 : toUpper ? 1 : 0;

                return new PositionModel (checkColumn, positionModel.Row + posCoeff);
            }
        }


        /// <summary>
        /// 해당 필드에 포함이 되는 포지션인지 여부.
        /// </summary>
        private bool ContainPosition<T> (IReadOnlyDictionary<int, List<T>> landDict, PositionModel positionModel)
        {
            return landDict.ContainsKey (positionModel.Column) &&
                   landDict[positionModel.Column].ContainIndex (positionModel.Row);
        }


        public float Distance<T> (Dictionary<int, List<T>> landDict, PositionModel checkPosition,
            PositionModel targetPosition) where T : LandModel
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