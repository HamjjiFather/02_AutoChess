using System;
using UnityEngine.Events;

namespace HexaPuzzle
{
    public enum PuzzleSpecialTypes
    {
        None = -1,
        ToVertical = 0,
        ToUpLeftDownRight = 1,
        ToUpRightDownLeft = 2,
        Bomb = 3,
        PickColors = 4
    }


    /// <summary>
    /// 퍼즐 색상.
    /// </summary>
    public enum PuzzleColorTypes
    {
        None = -1,
        Blue = 0,
        Orange,
        Green,
        Yellow,
        Purple,
        Max,
    }

    /// <summary>
    /// 퍼즐 모델.
    /// </summary>
    [Serializable]
    public class PuzzleModel
    {
        /// <summary>
        /// 퍼즐 색상 타입.
        /// </summary>
        public PuzzleColorTypes PuzzleColorTypes;

        /// <summary>
        /// 퍼즐 체크 타입.
        /// </summary>
        public PuzzleSpecialTypes PuzzleSpecialTypes = PuzzleSpecialTypes.None;

        /// <summary>
        /// 장애물 혹은 스테이지 타겟.
        /// </summary>
        public ObstacleTypes ObstacleTypes;

        /// <summary>
        /// 현재 자신이 속한 위치.
        /// </summary>
        public PositionModel PositionModel;

        /// <summary>
        /// 색상, 또는 모습이 변경됬을 경우 실행할 이벤트.
        /// </summary>
        public UnityAction<PuzzleModel> ChangePuzzleEvent;

        /// <summary>
        /// 퍼즐이 삭제됬을 경우 실행할 이벤트.
        /// </summary>
        public UnityAction<PuzzleModel> CheckPuzzleEvent;

        /// <summary>
        /// 아래로 내려가는 이벤트. 
        /// </summary>
        public UnityAction<PositionModel> MoveToPositionEvent;

        /// <summary>
        /// Check obstacle puzzle checked callback event.
        /// </summary>
        public UnityAction<int> ObstacleEvent;

        /// <summary>
        /// Change to special puzzle callback event.
        /// </summary>
        public UnityAction<PuzzleSpecialTypes> ChangeSpecialPuzzleEvent;


        public UnityAction ResetPuzzleEvent;


        /// <summary>
        /// 장애물 체크 카운트.
        /// </summary>
        private int _obstacleCount;

        /// <summary>
        /// 이 퍼즐이 체크 되었는지.
        /// /// </summary>
        public bool IsChecked { get; private set; }

        /// <summary>
        /// 현재 Column.
        /// </summary>
        public int Column => PositionModel.Column;

        /// <summary>
        /// 현재 Row.
        /// </summary>
        public int Row => PositionModel.Row;


        public void AddChangePuzzleEvent (UnityAction<PuzzleModel> action)
        {
            ChangePuzzleEvent = action;
        }


        public void AddCheckpuzzleEvent (UnityAction<PuzzleModel> action)
        {
            CheckPuzzleEvent = action;
        }


        public void AddDownPuzzleEvent (UnityAction<PositionModel> action)
        {
            MoveToPositionEvent = action;
        }


        public void AddObstacleEvent (UnityAction<int> action)
        {
            ObstacleEvent = action;
        }


        public void AddResetPuzzleEvent (UnityAction action)
        {
            ResetPuzzleEvent = action;
        }


        public void AddChangeSpecialTypeEvent (UnityAction<PuzzleSpecialTypes> action)
        {
            ChangeSpecialPuzzleEvent = action;
        }


        public void CheckPuzzle ()
        {
            if (IsChecked)
            {
                return;
            }

            IsChecked = true;
            ObstacleTypes = ObstacleTypes.None;
            PositionModel.Clear ();
            CheckPuzzleEvent?.Invoke (this);
        }


        /// <summary>
        /// convert special type state.
        /// </summary>
        public void ChangeSpecialPuzzle (PuzzleSpecialTypes puzzleSpecialTypes)
        {
            IsChecked = false;
            PuzzleSpecialTypes = puzzleSpecialTypes;
            ChangeSpecialPuzzleEvent?.Invoke (puzzleSpecialTypes);
        }


        /// <summary>
        /// move to position.
        /// </summary>
        public void MoveTo (PositionModel positionModel)
        {
            MoveToPositionEvent?.Invoke (positionModel);
        }


        public bool CheckObstacle ()
        {
            _obstacleCount++;
            ObstacleEvent?.Invoke (_obstacleCount);

            return _obstacleCount >= 2;
        }


        public void ResetPuzzle (PuzzleColorTypes puzzleColorTypes, PositionModel positionModel)
        {
            IsChecked = false;
            PuzzleColorTypes = puzzleColorTypes;
            PuzzleSpecialTypes = PuzzleSpecialTypes.None;
            PositionModel = positionModel;
            ResetPuzzleEvent?.Invoke ();
        }

        public override string ToString ()
        {
            return $"{PuzzleColorTypes} {PuzzleSpecialTypes} {PositionModel}";
        }
    }
}