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
        private readonly Queue<PuzzlePositionModel> _movePositions = new Queue<PuzzlePositionModel> ();

        private PuzzlePositionModel _nowPuzzlePosition;

        private Vector2 _beginDragPosition;

        private PuzzleView _puzzleView;

        private PuzzleViewmodel _puzzleViewmodel;

        private PuzzleMovementState _puzzleMovementState;

        private IDisposable _movementDisposable;

        private IDisposable _changeColorDisposable;

        private IDisposable _changeSpecialTypeDisposable;

        private IDisposable _changeObstacleDisposable;


        private void Awake ()
        {
            specialObj.Foreach (x => x.SetActive (false));
            _puzzleMovementState = PuzzleMovementState.None;
        }


        public void SetPuzzleView (PuzzleViewmodel puzzleViewmodel, PuzzleView view)
        {
            _puzzleViewmodel = puzzleViewmodel;
            _puzzleView = view;
        }


        public void SetModel (PuzzleModel puzzleModel)
        {
            PuzzleModel = puzzleModel;

            PuzzleModel.AddCheckpuzzleEvent (Checked);
            PuzzleModel.AddDownPuzzleEvent (Move);
            PuzzleModel.AddObstacleEvent (CheckObstacle);
            PuzzleModel.AddResetPuzzleEvent (InitializePuzzle);

            _changeColorDisposable = PuzzleModel.PuzzleColorTypes.Subscribe (ChangeColor);
            _changeSpecialTypeDisposable = PuzzleModel.PuzzleSpecialTypes.Subscribe (ChangeSpecialType);
            _changeObstacleDisposable = PuzzleModel.ObstacleTypes.Subscribe (ChangeObstacleType);
            
            void ChangeColor (PuzzleColorTypes puzzleColorTypes)
            {
                if (puzzleColorTypes == PuzzleColorTypes.None)
                    return;
                
                switch (puzzleColorTypes)
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
                        puzzleIcon.color = magenta;
                        break;

                    default:
                        puzzleIcon.color = white;
                        break;
                }
            }

            void ChangeSpecialType (PuzzleSpecialTypes puzzleSpecialTypes)
            {
                if (puzzleSpecialTypes == PuzzleSpecialTypes.None)
                    return;
                
                gameObject.SetActive (true);
                specialObj[(int) puzzleSpecialTypes].SetActive (true);
            }

            void ChangeObstacleType (ObstacleTypes obstacleTypes)
            {
                puzzleIcon.gameObject.SetActive (obstacleTypes == ObstacleTypes.None);
                obstacleObj.gameObject.SetActive (obstacleTypes == ObstacleTypes.Top);
            }
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
            transform.position = _puzzleView.GetLandElement (PuzzleModel.puzzlePositionModel).transform.position;
        }
        

        public void Checked (PuzzleModel puzzleModel)
        {
            _puzzleMovementState = PuzzleMovementState.None;
            _movementDisposable.DisposeSafe ();
            _movePositions.Clear ();
            _puzzleView.ContainPuzzle (this);
        }


        public void Move (PuzzlePositionModel puzzlePositionModel)
        {
            if (_movePositions.Contains (puzzlePositionModel)) return;
            _movePositions.Enqueue (puzzlePositionModel);

            if (_puzzleMovementState != PuzzleMovementState.None) return;
            SubscribeMovementAction ();

            void SubscribeMovementAction ()
            {
                _nowPuzzlePosition = _movePositions.Peek ();
                _puzzleMovementState = PuzzleMovementState.Moving;

                var targetPos = _puzzleView.GetLandElement (_nowPuzzlePosition).transform.position;

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
                            if (!PuzzleModel.puzzlePositionModel.Equals (_nowPuzzlePosition))
                                PuzzleModel.puzzlePositionModel.Set (_nowPuzzlePosition);
                        }

                        if (_puzzleMovementState == PuzzleMovementState.Almost &&
                            Vector2.Distance (transform.position, targetPos) <= float.Epsilon)
                        {
                            if (!_movePositions.Any ()) return;
                            _nowPuzzlePosition = _movePositions.Dequeue ();
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
                var besideModel = _puzzleViewmodel.GetBesideModel (PuzzleModel.puzzlePositionModel, angle);

                // 위치를 변경할 퍼즐이 있음.
                if (besideModel != null)
                {
                    _isSelected = false;
                    await _puzzleView.ChangePuzzlePosition (PuzzleModel, besideModel, angle);
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