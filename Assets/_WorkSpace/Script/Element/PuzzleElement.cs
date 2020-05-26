using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Color;

namespace HexaPuzzle
{
    public enum PuzzleMovementState
    {
        None,
        Moving,
        Almost,
    }

    /// <summary>
    /// 퍼즐 조각.
    /// </summary>
    public class PuzzleElement : MonoBehaviour
    {
        public PuzzleModel PuzzleModel { get; private set; }

        public Image puzzleIcon;

        public GameObject obstacleObj;

        public GameObject[] specialObj;

        private bool _isSelected;

        /// <summary>
        /// move position.
        /// </summary>
        private readonly Queue<PositionModel> _movePositions = new Queue<PositionModel> ();

        private PositionModel _nowPosition;

        private Vector2 _beginDragPosition;

        private GamePageView _gamePageView;

        private PuzzleViewmodel _puzzleViewmodel;

        private PuzzleMovementState _puzzleMovementState;

        private IDisposable _movementDisposable;


        private void Awake ()
        {
            specialObj.Foreach (x => x.SetActive (false));
            _puzzleMovementState = PuzzleMovementState.None;
        }
        //
        // private void Update ()
        // {
        //     if (!_isMove)
        //         return;
        //
        //     var targetPos = _gamePageView.GetLandElement (_nowPosition).transform.position;
        //     transform.position = Vector2.MoveTowards (transform.position, targetPos, 0.015f);
        //
        //     if (!(Vector2.Distance (transform.position, targetPos) <= 0.05f)) return;
        //     if (!PuzzleModel.PositionModel.Equals (_nowPosition))
        //         PuzzleModel.PositionModel.Set (_nowPosition);
        //
        //
        //     if (!(Vector2.Distance (transform.position, targetPos) <= float.Epsilon)) return;
        //
        //     _movePositions.Dequeue ();
        //
        //     if (_movePositions.Count == 0)
        //     {
        //         _puzzleViewmodel.CompleteAlignedPuzzles (PuzzleModel);
        //         _movePositions.Clear ();
        //         _isMove = false;
        //         return;
        //     }
        //
        //     _nowPosition = _movePositions.Peek ();
        // }


        public void SetPuzzleView (PuzzleViewmodel puzzleViewmodel, GamePageView view)
        {
            _puzzleViewmodel = puzzleViewmodel;
            _gamePageView = view;
        }


        public void SetModel (PuzzleModel puzzleModel)
        {
            PuzzleModel = puzzleModel;

            PuzzleModel.AddChangePuzzleEvent (ChangeShape);
            PuzzleModel.AddCheckpuzzleEvent (Checked);
            PuzzleModel.AddDownPuzzleEvent (Move);
            PuzzleModel.AddObstacleEvent (CheckObstacle);
            PuzzleModel.AddChangeSpecialTypeEvent (ChangeSpecialType);
            PuzzleModel.AddResetPuzzleEvent (InitializePuzzle);

            ChangeShape (PuzzleModel);
        }


        /// <summary>
        /// Reset puzzle data.
        /// </summary>
        public void InitializePuzzle ()
        {
            _puzzleMovementState = PuzzleMovementState.None;
            _movementDisposable.DisposeSafe ();
            gameObject.SetActive (true);
            specialObj.Foreach (x => x.SetActive (false));
            _movePositions.Clear ();
            var targetPos = _gamePageView.GetLandElement (PuzzleModel.PositionModel).transform.position;
            transform.position = targetPos;
            ChangeShape (PuzzleModel);
        }


        /// <summary>
        /// 색상 변경.
        /// </summary>
        private void ChangeShape (PuzzleModel puzzleModel)
        {
            puzzleIcon.gameObject.SetActive (PuzzleModel.ObstacleTypes == ObstacleTypes.None);
            obstacleObj.gameObject.SetActive (puzzleModel.ObstacleTypes == ObstacleTypes.Top);

            switch (puzzleModel.PuzzleColorTypes)
            {
                case PuzzleColorTypes.Blue:
                    puzzleIcon.color = blue;
                    break;

                case PuzzleColorTypes.Orange:
                    puzzleIcon.color = new Color (1f, 0.4f, 0f);
                    break;

                case PuzzleColorTypes.Green:
                    puzzleIcon.color = green;
                    break;

                case PuzzleColorTypes.Yellow:
                    puzzleIcon.color = yellow;
                    break;

                case PuzzleColorTypes.Purple:
                    puzzleIcon.color = new Color (1f, 0f, 1f);
                    break;

                default:
                    puzzleIcon.color = white;
                    break;
            }
        }

        public void Checked (PuzzleModel puzzleModel)
        {
            _puzzleMovementState = PuzzleMovementState.None;
            _movementDisposable.DisposeSafe ();
            _movePositions.Clear ();
            _gamePageView.ContainPuzzle (this);
        }


        public void Move (PositionModel positionModel)
        {
            if (_movePositions.Contains (positionModel)) return;
            _movePositions.Enqueue (positionModel);

            if (_puzzleMovementState != PuzzleMovementState.None) return;
            SubscribeMovementAction ();

            void SubscribeMovementAction ()
            {
                _nowPosition = _movePositions.Peek ();
                _puzzleMovementState = PuzzleMovementState.Moving;

                var targetPos = _gamePageView.GetLandElement (_nowPosition).transform.position;

                _movementDisposable.DisposeSafe ();
                _movementDisposable = Observable.EveryUpdate ()
                    .TakeWhile (_ => _movePositions.Any ())
                    .Subscribe (_ =>
                    {
                        transform.MoveTowards (transform.position, targetPos, Time.deltaTime);

                        if (_puzzleMovementState == PuzzleMovementState.Moving &&
                            Vector2.Distance (transform.position, targetPos) <= GameConstants.PuzzleMovementSpeed)
                        {
                            _puzzleMovementState = PuzzleMovementState.Almost;
                            if (!PuzzleModel.PositionModel.Equals (_nowPosition))
                                PuzzleModel.PositionModel.Set (_nowPosition);
                        }

                        if (_puzzleMovementState == PuzzleMovementState.Almost &&
                            Vector2.Distance (transform.position, targetPos) <= float.Epsilon)
                        {
                            if (!_movePositions.Any ()) return;
                            _nowPosition = _movePositions.Dequeue ();
                            _puzzleMovementState = PuzzleMovementState.Moving;
                        }
                    }, () =>
                    {
                        _puzzleMovementState = PuzzleMovementState.None;
                        _puzzleViewmodel.CompleteAlignedPuzzles (PuzzleModel);
                        _movePositions.Clear ();
                    });
            }
        }


        public void CheckObstacle (int count)
        {
            if (count == 1)
            {
                obstacleObj.GetComponent<Image> ().color = red;
                return;
            }

            Checked (PuzzleModel);
        }

        public void ChangeSpecialType (PuzzleSpecialTypes puzzleSpecialTypes)
        {
            gameObject.SetActive (true);
            specialObj[(int) puzzleSpecialTypes].SetActive (true);
        }


        public void OnDragBegin (BaseEventData baseEventData)
        {
            _isSelected = true;
            _beginDragPosition = baseEventData.currentInputModule.input.mousePosition;
        }

        public async void OnDrag (BaseEventData baseEventData)
        {
            if (!_isSelected)
                return;

            var curDragPosition = baseEventData.currentInputModule.input.mousePosition;
            var dist = Vector2.Distance (_beginDragPosition, curDragPosition);

            if (dist > 100f)
            {
                var angle = GetAngle ();
                var besideModel = _puzzleViewmodel.GetBesideModel (PuzzleModel.PositionModel, angle);

                // 위치를 변경할 퍼즐이 있음.
                if (besideModel != null)
                {
                    _isSelected = false;
                    await _gamePageView.ChangePuzzlePosition (PuzzleModel, besideModel, angle);
                }
            }

            float GetAngle ()
            {
                var x = _beginDragPosition.x - curDragPosition.x;
                var y = _beginDragPosition.y - curDragPosition.y;
                var value = (float) (Mathf.Atan2 (x, y) / Math.PI * 180f);
                if (value < 0) value += 360f;

                return value;
            }
        }
    }
}