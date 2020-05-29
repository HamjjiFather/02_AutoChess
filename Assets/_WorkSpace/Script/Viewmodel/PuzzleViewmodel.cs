using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KKSFramework.DesignPattern;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace HexaPuzzle
{
    public enum StageTypes
    {
        FoundTops
    }


    /// <summary>
    /// 장애물이 어디에 그려지는지.
    /// </summary>
    public enum ObstacleLayer
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// 퍼즐을 대체함.
        /// </summary>
        Puzzle,

        /// <summary>
        /// 필드를 대체함.
        /// </summary>
        Field
    }


    /// <summary>
    /// 장애물 타입.
    /// </summary>
    public enum ObstacleTypes
    {
        /// <summary>
        /// 일반 퍼즐임.
        /// </summary>
        None,

        /// <summary>
        /// 팽이.
        /// </summary>
        Top,
    }


    public class PuzzleViewmodel : ViewModelBase
    {
        #region DataModel

        /// <summary>
        /// 모든 라인 모델.
        /// </summary>
        public Dictionary<int, PuzzleLineModel> AllLineModels { get; } = new Dictionary<int, PuzzleLineModel> ();

        /// <summary>
        /// 모든 퍼즐 모델.
        /// </summary>
        public List<PuzzleModel> AllPuzzleModels { get; } = new List<PuzzleModel> ();

        #endregion


        /// <summary>
        /// 이동 예정이 되어있는 데이터.
        /// </summary>
        private readonly Dictionary<PuzzleModel, PuzzlePositionModel> _predicatedFlowSidePositionDict =
            new Dictionary<PuzzleModel, PuzzlePositionModel> ();


        private readonly Dictionary<PuzzleModel, PuzzlePositionModel> _predicatedFlowDownPositions =
            new Dictionary<PuzzleModel, PuzzlePositionModel> ();


        /// <summary>
        /// 정렬, 이동이 완료된 퍼즐들.
        /// </summary>
        private readonly List<PuzzleModel> _alignedPuzzles = new List<PuzzleModel> ();

        /// <summary>
        /// 정렬 완료 이벤트.
        /// </summary>
        private UnityAction _completeCheckPuzzles;

        /// <summary>
        /// player control model.
        /// </summary>
        private readonly PuzzlePlayerControlModel _puzzlePlayerControlModel = new PuzzlePlayerControlModel (false);

        /// <summary>
        /// Matching result model.
        /// </summary>
        private readonly PuzzleMatchingResultModel _puzzleMatchingResultModel = new PuzzleMatchingResultModel ();

        /// <summary>
        /// UniTask Cancellation Token Source.
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// on align puzzles?
        /// </summary>
        private bool _isAligned;

        
        private float _puzzleCheckValue;
        
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private GameViewmodel _gameViewmodel;

#pragma warning restore CS0649

        #endregion
        

        /// <summary>
        /// start game.
        /// </summary>
        public void StartGame (UnityAction completeCheckPuzzle)
        {
            InitializeGame ();
            CreateLines ();
            CreatePuzzles ();
            _completeCheckPuzzles = completeCheckPuzzle;
        }


        /// <summary>
        /// 배열 세팅.
        /// </summary>
        private void CreateLines ()
        {
            AllLineModels.Clear ();
            AllLineModels.Add (0, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Hide),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Hide),
                },
                IsCreateLine = false,
            });
            AllLineModels.Add (1, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Show),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Hide),
                },
                IsCreateLine = true,
            });
            AllLineModels.Add (2, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Show),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Show),
                },
                IsCreateLine = true,
            });
            AllLineModels.Add (3, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Show),
                    new PuzzleLandModel (1, LandTypes.Show),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Show),
                },
                IsCreateLine = true,
            });
            AllLineModels.Add (4, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Show),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Show),
                },
                IsCreateLine = true,
            });
            AllLineModels.Add (5, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Show),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Hide),
                },
                IsCreateLine = true,
            });
            AllLineModels.Add (6, new PuzzleLineModel
            {
                LandDatas = new List<PuzzleLandModel>
                {
                    new PuzzleLandModel (0, LandTypes.Hide),
                    new PuzzleLandModel (1, LandTypes.Hide),
                    new PuzzleLandModel (2, LandTypes.Show),
                    new PuzzleLandModel (3, LandTypes.Show),
                    new PuzzleLandModel (4, LandTypes.Show),
                    new PuzzleLandModel (5, LandTypes.Hide),
                },
                IsCreateLine = false,
            });
        }


        public override void Initialize ()
        {
            // throw new NotImplementedException ();
        }


        /// <summary>
        /// generate lands.
        /// </summary>
        private void InitializeGame ()
        {
            AllLineModels.Clear ();
            AllPuzzleModels.Clear ();
            _predicatedFlowSidePositionDict.Clear ();
            _predicatedFlowDownPositions.Clear ();
            _alignedPuzzles.Clear ();
            
            _cancellationTokenSource?.Cancel ();
            _cancellationTokenSource.DisposeSafe ();
            _cancellationTokenSource = new CancellationTokenSource ();
        }


        /// <summary>
        /// create obstacles.
        /// </summary>
        private List<PuzzlePositionModel> CreateObstaclesPosition ()
        {
            var obsPostition = new List<PuzzlePositionModel> ();
            const int obsCount = 5;

            for (var i = 0; i < obsCount; i++)
            {
                var column = Random.Range (0, AllLineModels.Count);
                var row = AllLineModels[column].LandDatas.Where (x => x.landTypes == LandTypes.Show).RandomIndex ();
                obsPostition.Add (new PuzzlePositionModel (column, row));
            }

            return obsPostition;
        }


        /// <summary>
        /// 퍼즐 생성.
        /// Generate puzzles.
        /// </summary>
        private void CreatePuzzles ()
        {
            var createdObs = CreateObstaclesPosition (); 
            
            do
            {
                Positioning ();
            } while (!CheckMovablePuzzleState ());

            void Positioning ()
            {
                InitializeModels ();

                for (var c = 0; c < AllLineModels.Count; c++)
                {
                    for (var r = 0; r < AllLineModels[c].Count; r++)
                    {
                        var positionModel = new PuzzlePositionModel (c, r);
                        if (!ContainLineModel (positionModel))
                            continue;

                        var puzzleModel = new PuzzleModel ();
                        AllPuzzleModels.Add (puzzleModel);

                        puzzleModel.puzzlePositionModel = positionModel;
                        puzzleModel.ObstacleTypes.Value =
                            createdObs.Contains (positionModel) ? ObstacleTypes.Top : ObstacleTypes.None;
                        SetPuzzleColorTypes (puzzleModel);
                    }
                }
            }

            void InitializeModels ()
            {
                AllPuzzleModels.Clear ();
            }
        }
        
        
        /// <summary>
        /// 모든 퍼즐의 위치를 확인함.
        /// 퍼즐이 이동하여 매칭이 되는 곳이 있는지 체크.
        /// Check all of puzzle position.  
        /// </summary>
        public bool CheckMovablePuzzleState ()
        {
            var colorType = PuzzleColorTypes.None;
            var isMatching = AllPuzzleModels
                .SelectMany (x =>
                {
                    colorType = x.PuzzleColorTypes.Value;
                    return GetAroundPositionModel (x.puzzlePositionModel);
                }).SelectMany (positionModel => new List<int>
                {
                    FindPuzzleCheckDirectionArea (positionModel, colorType, PuzzleCheckTypes.ToVertical),
                    FindPuzzleCheckDirectionArea (positionModel, colorType, PuzzleCheckTypes.ToUpLeftDownRight),
                    FindPuzzleCheckDirectionArea (positionModel, colorType, PuzzleCheckTypes.ToUpRightDownLeft)
                }).Any (x => x > (int) PuzzleMatchingType.ThreeMatching);

            return isMatching;
        }


        /// <summary>
        /// 해당 위치에 위치할 수 있을 퍼즐 색을 만든다.
        /// Generate puzzle color that can be located at the position.
        /// </summary>
        private void SetPuzzleColorTypes (PuzzleModel puzzleModel)
        {
            if (puzzleModel.ObstacleTypes.Value != ObstacleTypes.None)
                return;

            var numberList = Enumerable.Range (0, (int) PuzzleColorTypes.Max).ToList ();
            var countList = new List<int> ();
            while (true)
            {
                var colorType = (PuzzleColorTypes) numberList.RandomSource ();
                puzzleModel.PuzzleColorTypes.Value = colorType;
                numberList.Remove ((int) colorType);

                countList.Add (FindPuzzleCheckDirectionArea (puzzleModel.puzzlePositionModel, colorType,
                    PuzzleCheckTypes.ToVertical));
                countList.Add (FindPuzzleCheckDirectionArea (puzzleModel.puzzlePositionModel, colorType,
                    PuzzleCheckTypes.ToUpLeftDownRight));
                countList.Add (FindPuzzleCheckDirectionArea (puzzleModel.puzzlePositionModel, colorType,
                    PuzzleCheckTypes.ToUpRightDownLeft));

                if (countList.All (x => x < (int) PuzzleMatchingType.ThreeMatching) || numberList.Count == 0)
                {
                    break;
                }

                countList.Clear ();
            }
        }



        #region Puzzle Movement.

        /// <summary>
        /// 퍼즐 위치가 변경됨.
        /// </summary>
        public async UniTask ConvertPuzzlePosition (PuzzleModel origin, PuzzleModel target)
        {
            var originPosition = origin.puzzlePositionModel;

            origin.MoveTo (target.puzzlePositionModel);
            target.MoveTo (originPosition);

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.WaitCheckTime), cancellationToken: _cancellationTokenSource.Token);
        }


        /// <summary>
        /// 정렬, 이동이 완료됨.
        /// </summary>
        public void CompleteAlignedPuzzles (PuzzleModel puzzleModel)
        {
            Debug.Log ($"{puzzleModel} move complete at");
            _predicatedFlowDownPositions.Remove (puzzleModel);
            _predicatedFlowSidePositionDict.Remove (puzzleModel);
            if (_alignedPuzzles.Contains (puzzleModel))
                return;
            _alignedPuzzles.Add (puzzleModel);
        }

        #endregion


        #region Check Match Puzzles.

        /// <summary>
        /// 퍼즐 체크 플로우를 진행함.
        /// Progress the puzzle check flow. 
        /// </summary>
        public async UniTask<bool> PuzzleCheckFlow ()
        {
            _puzzleMatchingResultModel.Reset ();

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.WaitCheckTime), cancellationToken: _cancellationTokenSource.Token);

            FindPuzzle ();
            ChangeSpecialPuzzle ();
            if (!_puzzleMatchingResultModel.isMatching)
                CheckCombinablePuzzle ();

            if (!_puzzleMatchingResultModel.isMatching)
                return false;

            CheckSpecialPuzzle ();
            CheckPuzzle ();

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.WaitCheckTime), cancellationToken: _cancellationTokenSource.Token);
            AlignPuzzles ();

            return true;

            async void AlignPuzzles ()
            {
                if (_isAligned)
                {
                    return;
                }

                _isAligned = true;
                while (_isAligned)
                {
                    await CreateNewPuzzle ();
                    await FlowDownCheck ();
                    await FlowSideCheck ();

                    _isAligned = AllPuzzleModels.Any (x => x.IsChecked) || _predicatedFlowSidePositionDict.Count != 0;
                }

                Debug.LogWarning ("Array Check Ended");
                await UniTask.Delay (TimeSpan.FromSeconds (0.5f), cancellationToken:_cancellationTokenSource.Token);

                ChangeControlMode (false, PuzzleCheckTypes.None, _alignedPuzzles);
                
                // when exist moved puzzles, re-check matching state of the moved puzzle.
                if (await PuzzleCheckFlow ()) return;

                DonePuzzleChecks ();
            }

            void DonePuzzleChecks ()
            {
                _alignedPuzzles.Clear ();
                _gameViewmodel.AddResult (_puzzleCheckValue);
                _completeCheckPuzzles.Invoke ();
                _puzzleCheckValue = 0;
            }
        }

        #endregion


        #region Find Puzzles

        /// <summary>
        /// 매칭이 된 퍼즐을 찾음.
        /// </summary>
        private void FindPuzzle ()
        {
            _puzzlePlayerControlModel.ControlPuzzles.Foreach (FindPuzzles);

            void FindPuzzles (PuzzleModel puzzleModel)
            {
                var verticalCheckModel = FindPuzzleCheckTypeArea (puzzleModel, PuzzleCheckTypes.ToVertical);
                var uldrCheckModel = FindPuzzleCheckTypeArea (puzzleModel, PuzzleCheckTypes.ToUpLeftDownRight);
                var urdlCheckModel = FindPuzzleCheckTypeArea (puzzleModel, PuzzleCheckTypes.ToUpRightDownLeft);

                SetTotalCheckResultModel ();

                void SetTotalCheckResultModel ()
                {
                    var resultPuzzles = new List<PuzzleModel> ();
                    resultPuzzles.AddRange (verticalCheckModel.CheckPuzzles);
                    resultPuzzles.AddRange (uldrCheckModel.CheckPuzzles);
                    resultPuzzles.AddRange (urdlCheckModel.CheckPuzzles);

                    if (!resultPuzzles.Any ())
                        return;

                    var foundContainResult = _puzzleMatchingResultModel.FindContainedResultModels (resultPuzzles);
                    var totalCheckResultModel = foundContainResult.Item1 ?? new TotalPuzzleCheckResultModel ();

                    CheckOverlap (verticalCheckModel, uldrCheckModel);
                    CheckOverlap (verticalCheckModel, urdlCheckModel);
                    CheckOverlap (uldrCheckModel, urdlCheckModel);

                    totalCheckResultModel.SetTotalMatchingType (new[]
                    {
                        verticalCheckModel.PuzzleMatchingTypes,
                        uldrCheckModel.PuzzleMatchingTypes,
                        urdlCheckModel.PuzzleMatchingTypes
                    });

                    totalCheckResultModel.AddRangeMatchPuzzle (resultPuzzles);

                    var isMatching = verticalCheckModel.IsChecked || uldrCheckModel.IsChecked ||
                                     urdlCheckModel.IsChecked;
                    _puzzleMatchingResultModel.SetMathingState (isMatching);

                    if (!foundContainResult.Item2)
                        _puzzleMatchingResultModel.AddCheckResult (totalCheckResultModel);

                    // 중복 라인 체크.
                    void CheckOverlap (PuzzleCheckResultModel firstCheck, PuzzleCheckResultModel secondCheck)
                    {
                        if (!firstCheck.IsChecked || !secondCheck.IsChecked) return;
                        var overlapPuzzle = GetOverlapedPuzzle (firstCheck.CheckPuzzles, secondCheck.CheckPuzzles);
                        totalCheckResultModel.SetMatchingType (PuzzleMatchingType.TOverlap);
                        totalCheckResultModel.SetOverlapPuzzle (overlapPuzzle);
                    }
                }
            }

            PuzzleCheckResultModel FindPuzzleCheckTypeArea (PuzzleModel puzzleModel, PuzzleCheckTypes puzzleCheckTypes)
            {
                var checkResult = new PuzzleCheckResultModel ();
                var continuoslyCount = FindPuzzleCheckDirectionArea (puzzleModel.puzzlePositionModel,
                    puzzleModel.PuzzleColorTypes.Value,
                    puzzleCheckTypes, puzzle => { checkResult.AddCheckedPuzzle (puzzle); });

                checkResult.SetMatchingType ((PuzzleMatchingType) continuoslyCount);

                return checkResult;
            }
        }


        /// <summary>
        /// 체크 영역의 퍼즐들을 체크함.
        /// </summary>
        private int FindPuzzleCheckDirectionArea (PuzzlePositionModel puzzlePositionModel, PuzzleColorTypes colorTypes,
            PuzzleCheckTypes puzzleCheckTypes, UnityAction<PuzzleModel> action = null)
        {
            var continuoslyCount = 0;
            var (checkDir, otherCheckDir) = GetCheckDirectionTypesByPuzzleCheckType (puzzleCheckTypes);
            var positionModels = GetPositionKeysByAngle (puzzlePositionModel, checkDir, otherCheckDir);

            positionModels.List
                .Select ((position, index) => (GetContainPuzzleModel (position), index))
                .Where (valueTuple => valueTuple.Item1 != null)
                .Foreach (valueTuple =>
                {
                    var (foundPuzzle, index) = valueTuple;
                    if (foundPuzzle.PuzzleColorTypes.Value == PuzzleColorTypes.None)
                    {
                        return;
                    }

                    if (foundPuzzle.puzzlePositionModel.Equals (puzzlePositionModel))
                    {
                        Check ();
                        return;
                    }

                    // 체크하려는 포지션에 있는 퍼즐이 대상의 색상과 같지 않음.
                    if (!foundPuzzle.PuzzleColorTypes.Value.Equals (colorTypes)) return;

                    if (IndexCheck (index))
                    {
                        Check ();
                    }

                    void Check ()
                    {
                        continuoslyCount++;
                        action?.Invoke (foundPuzzle);
                    }
                });

            // 체크하려는 포지션에서 대상의 퍼즐 포지션 안쪽에 있는 퍼즐과 체크를 함.
            // ex) 0, 1, 2, 3, 4의 포지션이 있을 경우. 0, 4의 포지션은 각각 1, 3의 포지션의 퍼즐을 체크 함.
            bool IndexCheck (int index)
            {
                if (index != 0 && index != 4) return true;

                var insidePosition = index == 0 ? positionModels.Previous : positionModels.Next;
                if (insidePosition == null)
                    return false;

                var insidePuzzleColorTypes = GetContainPuzzleColorTypes (insidePosition.Value);
                return insidePuzzleColorTypes.Equals (colorTypes);
            }

            return continuoslyCount < (int) PuzzleMatchingType.ThreeMatching ? 0 : continuoslyCount;
        }


        /// <summary>
        /// Change special puzzles.
        /// </summary>
        private void ChangeSpecialPuzzle ()
        {
            _puzzleMatchingResultModel.checkResultModels.Foreach (ChangeSpecialPuzzles);

            void ChangeSpecialPuzzles (TotalPuzzleCheckResultModel totalCheckResultModel)
            {
                if (totalCheckResultModel.PuzzleMatchingTypes == PuzzleMatchingType.TOverlap)
                {
                    CreateSpecialPuzzle (PuzzleSpecialTypes.Bomb);
                }

                if (totalCheckResultModel.PuzzleMatchingTypes == PuzzleMatchingType.FourMatching)
                {
                    CreateSpecialPuzzle (GetPuzzleSpecialType ());
                }

                if (totalCheckResultModel.PuzzleMatchingTypes == PuzzleMatchingType.FiveMatching)
                {
                    CreateSpecialPuzzle (PuzzleSpecialTypes.PickColors);
                }

                void CreateSpecialPuzzle (PuzzleSpecialTypes puzzleSpecialTypes)
                {
                    var changedPuzzleModel = GetChangeTargetPuzzle ();
                    Debug.Log (changedPuzzleModel.ToString ());
                    changedPuzzleModel.ChangeSpecialPuzzle (puzzleSpecialTypes);
                    _puzzleMatchingResultModel.RemovePuzzle (changedPuzzleModel);
                }

                PuzzleModel GetChangeTargetPuzzle ()
                {
                    if (totalCheckResultModel.PuzzleMatchingTypes == PuzzleMatchingType.TOverlap)
                        return totalCheckResultModel.OverlapPuzzle;

                    if (!_puzzlePlayerControlModel.IsPlayerControl)
                        return totalCheckResultModel.CheckPuzzles.RandomSource (x =>
                            x.PuzzleSpecialTypes.Value == PuzzleSpecialTypes.None);

                    return totalCheckResultModel.CheckPuzzles.Contains (_puzzlePlayerControlModel.Origin)
                        ? _puzzlePlayerControlModel.Origin
                        : totalCheckResultModel.CheckPuzzles.Contains (_puzzlePlayerControlModel.Target)
                            ? _puzzlePlayerControlModel.Target
                            : null;
                }

                PuzzleSpecialTypes GetPuzzleSpecialType ()
                {
                    return _puzzlePlayerControlModel.IsPlayerControl
                        ? (PuzzleSpecialTypes) (int) _puzzlePlayerControlModel.PlayerControlDirection
                        : (PuzzleSpecialTypes) Random.Range ((int) PuzzleSpecialTypes.ToVertical,
                            (int) PuzzleSpecialTypes.Bomb);
                }
            }
        }

        #endregion


        #region Check Puzzle

        /// <summary>
        /// 퍼즐 데이터를 초기화함.
        /// Initialize checked puzzle data.
        /// </summary>
        private void CheckPuzzle ()
        {
            var index = 0;
            var obstacles = new List<PuzzleModel> ();
            var dirArray = (CheckDirectionTypes[]) Enum.GetValues (typeof (CheckDirectionTypes));

            _puzzleMatchingResultModel.checkResultModels.Foreach (checkResult =>
            {
                CheckPuzzleValue (checkResult);
                
                checkResult.CheckPuzzles.Foreach (removePuzzleModel =>
                {
                    if (removePuzzleModel == null)
                        return;

                    obstacles.AddRange (dirArray
                        .Select (dir => GetBesideModel (removePuzzleModel, dir))
                        .Where (x => x != null && x.ObstacleTypes.Value == ObstacleTypes.Top));

                    Debug.Log (
                        $"Check {index} : {checkResult.PuzzleMatchingTypes} {removePuzzleModel.PuzzleColorTypes}{removePuzzleModel.puzzlePositionModel}");
                    removePuzzleModel.CheckPuzzle ();
                });
                index++;
            });

            CheckObstacles (obstacles);
        }


        private void CheckPuzzleValue (TotalPuzzleCheckResultModel totalPuzzleCheckResultModel)
        {
            switch (totalPuzzleCheckResultModel.PuzzleMatchingTypes)
            {
                case PuzzleMatchingType.None:
                    break;
                
                case PuzzleMatchingType.ThreeMatching:
                    _puzzleCheckValue += GameConstants.ThreeMatchingPuzzleCheckValue;
                    break;
                
                case PuzzleMatchingType.FourMatching:
                    _puzzleCheckValue += GameConstants.FourMatchingPuzzleCheckValue;
                    break;
                
                case PuzzleMatchingType.FiveMatching:
                    _puzzleCheckValue += GameConstants.FiveMatchingPuzzleCheckValue;
                    break;
                
                case PuzzleMatchingType.TOverlap:
                    _puzzleCheckValue += GameConstants.OverlapMatchingPuzzleCheckValue;
                    break;
                
                case PuzzleMatchingType.CombineSpecial:
                    _puzzleCheckValue += GameConstants.CombineSpecialMatchingPuzzleCheckValue;
                    break;
                
                case PuzzleMatchingType.Pick:
                    _puzzleCheckValue += GameConstants.PickMatchingPuzzleCheckValue;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }


        /// <summary>
        /// 장애물 퍼즐을 체크함.
        /// </summary>
        private void CheckObstacles (IEnumerable<PuzzleModel> obstacles)
        {
            obstacles.Distinct ().Foreach (x =>
            {
                if (x.CheckObstacle ())
                {
                    x.CheckPuzzle ();
                }
            });
        }


        /// <summary>
        /// 움직인 퍼즐이 조합 가능한 퍼즐인지 체크.
        /// </summary>
        /// <returns></returns>
        private void CheckCombinablePuzzle ()
        {
            if (!_puzzlePlayerControlModel.IsPlayerControl)
                return;

            // 이동시킨 퍼즐 둘 다 특수 퍼즐일 경우. 
            if (_puzzlePlayerControlModel.Origin.PuzzleSpecialTypes.Value != PuzzleSpecialTypes.None &&
                _puzzlePlayerControlModel.Target.PuzzleSpecialTypes.Value != PuzzleSpecialTypes.None)
            {
                CombineSpecialTypePuzzle ();
                _puzzleMatchingResultModel.SetMathingState (true);
                return;
            }

            // 둘 다 특수 퍼즐이 아니어도 Pick타입의 퍼즐은 확인함.
            CombinePickTypePuzzle ();

            void CombineSpecialTypePuzzle ()
            {
                var checkResult = new TotalPuzzleCheckResultModel ();
                checkResult.SetMatchingType (PuzzleMatchingType.CombineSpecial);
                checkResult.AddRangeMatchPuzzle (new[]
                {
                    _puzzlePlayerControlModel.Origin,
                    _puzzlePlayerControlModel.Target
                });
                _puzzleMatchingResultModel.AddCheckResult (checkResult);
            }


            void CombinePickTypePuzzle ()
            {
                var isOriginPickType = _puzzlePlayerControlModel.Origin.PuzzleSpecialTypes.Value == PuzzleSpecialTypes.PickColors;
                var isTargetPickType = _puzzlePlayerControlModel.Target.PuzzleSpecialTypes.Value == PuzzleSpecialTypes.PickColors;

                if (!isOriginPickType && !isTargetPickType)
                    return;

                var checkResult = new TotalPuzzleCheckResultModel ();
                _puzzleMatchingResultModel.SetMathingState (true);

                if (isOriginPickType && isTargetPickType)
                {
                    SetPickState (_puzzlePlayerControlModel.Origin, _puzzlePlayerControlModel.Target);
                    return;
                }

                if (isOriginPickType)
                {
                    SetPickState (_puzzlePlayerControlModel.Origin, _puzzlePlayerControlModel.Target);
                    return;
                }

                SetPickState (_puzzlePlayerControlModel.Target, _puzzlePlayerControlModel.Origin);

                void SetPickState (PuzzleModel specialPuzzle, PuzzleModel pickedPuzzle)
                {
                    checkResult.AddCheckedPuzzle (specialPuzzle);
                    checkResult.SetPickedPuzzle (pickedPuzzle);
                    checkResult.SetMatchingType (PuzzleMatchingType.Pick);
                    _puzzleMatchingResultModel.AddCheckResult (checkResult);
                }
            }
        }


        /// <summary>
        /// Find checked puzzle by special type puzzles.
        /// </summary>
        private void CheckSpecialPuzzle ()
        {
            var index = 0;
            while (_puzzleMatchingResultModel.checkResultModels.Count > index)
            {
                var checkModel = _puzzleMatchingResultModel.checkResultModels[index];
                checkModel.CheckPuzzles
                    .Where (x => x.PuzzleSpecialTypes.Value != PuzzleSpecialTypes.None).ToArray ()
                    .Foreach (puzzleModel =>
                    {
                        Debug.Log ($"check special puzzle {index} => {puzzleModel}");
                        ProcessCheckSpecialPuzzle (checkModel, puzzleModel);
                    });

                index++;
            }
        }

        /// <summary>
        /// 특수 퍼즐 영역에 맞는 퍼즐들을 찾음.
        /// </summary>
        private void ProcessCheckSpecialPuzzle (TotalPuzzleCheckResultModel totalPuzzleCheckResultModel, PuzzleModel puzzleModel)
        {
            if (totalPuzzleCheckResultModel.PuzzleMatchingTypes == PuzzleMatchingType.Pick)
            {
                var targetPuzzleColorTypes =
                    totalPuzzleCheckResultModel.PickedPuzzle?.PuzzleColorTypes.Value ?? GetRandomPuzzleColorTypes ();

                if (_puzzlePlayerControlModel.IsPlayerControl)
                {
                    var foundedPuzzles = new List<PuzzleModel> ();

                    switch (totalPuzzleCheckResultModel.PickedPuzzle?.PuzzleSpecialTypes.Value)
                    {
                        case PuzzleSpecialTypes.None:
                            foundedPuzzles = GetPuzzles (x => x.PuzzleColorTypes.Value.Equals (targetPuzzleColorTypes))
                                .ToList ();
                            break;

                        case PuzzleSpecialTypes.ToVertical:
                        case PuzzleSpecialTypes.ToUpLeftDownRight:
                        case PuzzleSpecialTypes.ToUpRightDownLeft:
                            foundedPuzzles = GetPuzzles (x => x.PuzzleColorTypes.Value.Equals (targetPuzzleColorTypes))
                                .ToList ();
                            foundedPuzzles.Foreach (x => x.ChangeSpecialPuzzle (GetRandomLineSpecialTypes ()));
                            break;

                        case PuzzleSpecialTypes.Bomb:
                            foundedPuzzles = GetPuzzles (x => x.PuzzleColorTypes.Value.Equals (targetPuzzleColorTypes))
                                .ToList ();
                            foundedPuzzles.Foreach (x => x.ChangeSpecialPuzzle (PuzzleSpecialTypes.Bomb));
                            break;

                        case PuzzleSpecialTypes.PickColors:
                            foundedPuzzles = AllPuzzleModels.ToList ();
                            break;
                    }

                    totalPuzzleCheckResultModel.AddRangeMatchPuzzle (foundedPuzzles);
                    _puzzleCheckValue += foundedPuzzles.Count * 2;
                }

                IEnumerable<PuzzleModel> GetPuzzles (Func<PuzzleModel, bool> predicate)
                {
                    return AllPuzzleModels.Where (predicate);
                }

                PuzzleSpecialTypes GetRandomLineSpecialTypes ()
                {
                    var rand = Random.Range (0, (int) PuzzleSpecialTypes.ToUpRightDownLeft);
                    return (PuzzleSpecialTypes) rand;
                }
            }

            switch (puzzleModel.PuzzleSpecialTypes.Value)
            {
                case PuzzleSpecialTypes.ToVertical:
                    totalPuzzleCheckResultModel.AddRangeMatchPuzzle (AllPuzzleModels.Where (x =>
                        x.Column == puzzleModel.Column));
                    break;

                case PuzzleSpecialTypes.ToUpLeftDownRight:
                    AllPuzzleFindCheckDir (totalPuzzleCheckResultModel, puzzleModel, CheckDirectionTypes.ToUpLeft,
                        CheckDirectionTypes.ToDownRight);
                    break;

                case PuzzleSpecialTypes.ToUpRightDownLeft:
                    AllPuzzleFindCheckDir (totalPuzzleCheckResultModel, puzzleModel, CheckDirectionTypes.ToUpRight,
                        CheckDirectionTypes.ToDownLeft);
                    break;

                case PuzzleSpecialTypes.Bomb:
                    var aroundPositions = GetAroundPositionModel (puzzleModel.puzzlePositionModel);
                    totalPuzzleCheckResultModel.AddRangeMatchPuzzle (aroundPositions.Select (GetContainPuzzleModel));
                    break;

                case PuzzleSpecialTypes.PickColors:
                    break;

                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }

        // 해당 방향에 해당하는 모든 퍼즐을 찾음.
        void AllPuzzleFindCheckDir (TotalPuzzleCheckResultModel totalPuzzleCheckResultModel, PuzzleModel puzzleModel,
            CheckDirectionTypes checkDirectionTypes, CheckDirectionTypes otherCheckDirectionTypes)
        {
            var foundedPuzzles = new List<PuzzleModel> ();
            GetFoundedPuzzles (checkDirectionTypes);
            GetFoundedPuzzles (otherCheckDirectionTypes);
            totalPuzzleCheckResultModel.AddRangeMatchPuzzle (foundedPuzzles);

            void GetFoundedPuzzles (CheckDirectionTypes checkDir)
            {
                var position = GetPositionByDirectionType (puzzleModel, checkDir);

                while (ExistLineModel (position))
                {
                    if (!ContainLineModel (position))
                    {
                        position = GetPositionByDirectionType (position, checkDir);
                        continue;
                    }

                    var foundPuzzle = GetContainPuzzleModel (position);
                    if (foundPuzzle == null) continue;

                    foundedPuzzles.Add (foundPuzzle);
                    position = GetPositionByDirectionType (foundPuzzle, checkDir);
                }
            }
        }

        #endregion


        // 퍼즐 재정렬.
        #region ArrayPuzzles.

        /// <summary>
        /// 아래로 떨어뜨림.
        /// </summary>
        private async UniTask FlowDownCheck ()
        {
            Debug.Log ($"{nameof (FlowDownCheck)}ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

            AllPuzzleModels
                .Where (CheckMovablePuzzleModel)
                .OrderBy (x => x.Row)
                .Foreach (puzzleModel =>
                {
                    if (!ContainLineModel (puzzleModel.puzzlePositionModel))
                        return;


                    var existLandRows = AllLineModels[puzzleModel.Column].GetBelowRows (puzzleModel.Row);
                    foreach (var row in existLandRows)
                    {
                        var checkPosition = new PuzzlePositionModel (puzzleModel.Column, row);
                        var checkPuzzle = GetContainPuzzleModel (checkPosition);

                        if ((checkPuzzle == null || checkPuzzle.IsChecked ||
                             _predicatedFlowDownPositions.ContainsKey (checkPuzzle)) &&
                            !_predicatedFlowDownPositions.ContainsValue (checkPosition) &&
                            !_predicatedFlowSidePositionDict.ContainsValue (checkPosition))
                        {
                            Debug.Log (
                                $"{puzzleModel} flow down to {checkPosition}");
                            _predicatedFlowDownPositions.SetOrAdd (puzzleModel, checkPosition);
                            puzzleModel.MoveTo (checkPosition);
                        }
                    }
                });

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.FlowCheckTime), cancellationToken: _cancellationTokenSource.Token);
        }


        /// <summary>
        /// 옆으로 떨어짐.
        /// </summary>
        private async UniTask FlowSideCheck ()
        {
            Debug.Log ($"{nameof (FlowSideCheck)}ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

            AllPuzzleModels
                .Where (CheckMovablePuzzleModel)
                .OrderByDescending (x => x.Row)
                .Foreach (puzzleModel =>
                {
                    var isBottomLeft = FlowCheckSide (puzzleModel, CheckDirectionTypes.ToDownLeft);
                    if (!isBottomLeft)
                    {
                        FlowCheckSide (puzzleModel, CheckDirectionTypes.ToDownRight);
                    }
                });

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.FlowCheckTime), cancellationToken: _cancellationTokenSource.Token);

            bool FlowCheckSide (PuzzleModel puzzleModel, CheckDirectionTypes lineDirectionTypes)
            {
                var besidePosition = GetPositionByDirectionType (puzzleModel, lineDirectionTypes);
                if (!CheckMovablePosition (besidePosition))
                    return false;

                var rows = AllLineModels[besidePosition.Column].GetAvobeRows (besidePosition.Row);
                if (rows.Select (x => new PuzzlePositionModel (besidePosition.Column, x))
                    .Any (x => !CheckMovablePosition (x)))
                {
                    return false;
                }

                // to flow side position's line data is not contained or
                // exist puzzle data at flow side target position.
                if (AllLineModels[besidePosition.Column].IsCreateLine)
                    return false;

                Debug.Log ($"{puzzleModel} flow side to {besidePosition}");
                _predicatedFlowSidePositionDict.Add (puzzleModel, besidePosition);
                puzzleModel.MoveTo (besidePosition);
                return true;
            }
        }

        /// <summary>
        /// 새 퍼즐을 만듦.
        /// </summary>
        /// <returns></returns>
        private async UniTask CreateNewPuzzle ()
        {
            Debug.Log ($"{nameof (CreateNewPuzzle)}ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ");

            AllLineModels.Where (x => x.Value.IsCreateLine).Foreach (lineModel =>
            {
                var position = new PuzzlePositionModel (lineModel.Key, lineModel.Value.CreatePuzzleRow ());
                var puzzle = GetContainPuzzleModel (position);
                if (puzzle != null && !puzzle.IsChecked)
                    return;

                var removedPuzzle = AllPuzzleModels.FirstOrDefault (x => x.IsChecked);
                if (removedPuzzle == null)
                    return;

                var puzzleColor =
                    (PuzzleColorTypes) Random.Range ((int) PuzzleColorTypes.None + 1, (int) PuzzleColorTypes.Max);
                Debug.LogWarning ($"create {puzzleColor} at {position}");
                removedPuzzle.ResetPuzzle (puzzleColor, position);
                _alignedPuzzles.Add (removedPuzzle);
            });

            await UniTask.Delay (TimeSpan.FromSeconds (GameConstants.FlowCheckTime), cancellationToken: _cancellationTokenSource.Token);
        }

        #endregion


        #region Get Line Model Data.

        public bool ExistLineModel (PuzzlePositionModel puzzlePositionModel)
        {
            return AllLineModels.ContainsKey (puzzlePositionModel.Column) &&
                   AllLineModels[puzzlePositionModel.Column].ExistRow (puzzlePositionModel.Row);
        }


        public bool ContainLineModel (PuzzlePositionModel puzzlePositionModel)
        {
            return AllLineModels.ContainsKey (puzzlePositionModel.Column) &&
                   AllLineModels[puzzlePositionModel.Column].ContainRow (puzzlePositionModel.Row);
        }

        public bool GetLandState (PuzzlePositionModel puzzlePositionModel)
        {
            return ContainLineModel (puzzlePositionModel) &&
                   AllLineModels[puzzlePositionModel.Column].ContainRow (puzzlePositionModel.Row);
        }

        #endregion


        #region Get Puzzle Model Data.

        /// <summary>
        /// Able to the puzzle move?
        /// </summary>
        public bool CheckMovablePuzzleModel (PuzzleModel puzzleModel)
        {
            /*
             * 퍼즐 모델이 null이 아님.
             * 퍼즐 모델이 삭제예정이 아님.
             * 퍼즐이 이동 예정이 아님.
             * */
            return puzzleModel != null &&
                   !puzzleModel.IsChecked &&
                   !_predicatedFlowSidePositionDict.ContainsKey (puzzleModel) &&
                   !_predicatedFlowDownPositions.ContainsKey (puzzleModel);
        }

        public PuzzleModel GetBesideModel (PuzzlePositionModel puzzlePositionModel, float angle)
        {
            var targetPosition = GetKeyByAngle (puzzlePositionModel, angle);
            return GetContainPuzzleModel (targetPosition);
        }

        public PuzzleModel GetBesideModel (PuzzleModel target, CheckDirectionTypes checkDirectionTypes)
        {
            var targetPosition = GetPositionByDirectionType (target, checkDirectionTypes);
            return GetContainPuzzleModel (targetPosition);
        }

        private PuzzleModel GetContainPuzzleModel (PuzzlePositionModel puzzlePositionModel)
        {
            return AllPuzzleModels.Find (x => x.puzzlePositionModel.Equals (puzzlePositionModel));
        }

        private PuzzleColorTypes GetContainPuzzleColorTypes (PuzzlePositionModel puzzlePositionModel)
        {
            var puzzle = GetContainPuzzleModel (puzzlePositionModel);
            return puzzle?.PuzzleColorTypes.Value ?? PuzzleColorTypes.None;
        }

        private PuzzleColorTypes GetRandomPuzzleColorTypes ()
        {
            var rand = Random.Range (0, (int) PuzzleColorTypes.Max);
            return (PuzzleColorTypes) rand;
        }


        // 두 라인에서 중복이 되는 위치에 있는 퍼즐을 찾아 리턴.
        private PuzzleModel GetOverlapedPuzzle (IEnumerable<PuzzleModel> firstPuzzles,
            IEnumerable<PuzzleModel> secondPuzzles)
        {
            var tempOverlapedPuzzle = firstPuzzles.Overlap (secondPuzzles);

            // 중복 위치에 있는 퍼즐이 없거나, 특수 퍼즐일 경우 두 라인의 매칭된 퍼즐 중에서 랜덤으로 변경.
            if (tempOverlapedPuzzle == null ||
                tempOverlapedPuzzle.PuzzleSpecialTypes.Value != PuzzleSpecialTypes.None)
            {
                tempOverlapedPuzzle = firstPuzzles.Concat (secondPuzzles)
                    .RandomSource (x => x.PuzzleSpecialTypes.Value == PuzzleSpecialTypes.None);
            }

            return tempOverlapedPuzzle;
        }

        #endregion


        #region Puzzle Position Data.

        public PuzzleCheckTypes GetPuzzleDirection (float angle)
        {
            var checkDirection = GetCheckDirectionTypesByAngle (angle);

            switch (checkDirection)
            {
                case CheckDirectionTypes.ToUpward:
                case CheckDirectionTypes.ToDownward:
                    return PuzzleCheckTypes.ToVertical;

                case CheckDirectionTypes.ToUpLeft:
                case CheckDirectionTypes.ToDownRight:
                    return PuzzleCheckTypes.ToUpLeftDownRight;

                case CheckDirectionTypes.ToUpRight:
                case CheckDirectionTypes.ToDownLeft:
                    return PuzzleCheckTypes.ToUpRightDownLeft;

                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }


        /// <summary>
        /// Able to move to the position?
        /// Exist the position.
        /// Not exist puzzle at the position.
        /// Puzzle move scheduled does not exist.
        /// </summary>
        public bool CheckMovablePosition (PuzzlePositionModel puzzlePositionModel)
        {
            return ContainLineModel (puzzlePositionModel) &&
                   GetContainPuzzleModel (puzzlePositionModel) == null &&
                   !_predicatedFlowSidePositionDict.ContainsValue (puzzlePositionModel) &&
                   !_predicatedFlowDownPositions.ContainsValue (puzzlePositionModel);
        }


        /// <summary>
        /// 현재 위치에서 해당 방향, 반대 방향의 위치값 리턴. 
        /// </summary>
        public LinkedListNode<PuzzlePositionModel> GetPositionKeysByAngle (PuzzlePositionModel puzzlePositionModel,
            CheckDirectionTypes checkCheckTypes, CheckDirectionTypes otherCheckTypes)
        {
            var positionNode = new LinkedListNode<PuzzlePositionModel> (puzzlePositionModel);
            var positionList = new LinkedList<PuzzlePositionModel> ();
            var besidePosition = GetKeyByAngle (puzzlePositionModel, (float) checkCheckTypes);
            var beyondPosition = GetKeyByAngle (besidePosition, (float) checkCheckTypes);
            var otherBesidePosition = GetKeyByAngle (puzzlePositionModel, (float) otherCheckTypes);
            var otherBeyondPosition = GetKeyByAngle (otherBesidePosition, (float) otherCheckTypes);

            positionList.AddLast (positionNode);
            positionList.AddFirst (besidePosition);
            positionList.AddFirst (beyondPosition);
            positionList.AddLast (otherBesidePosition);
            positionList.AddLast (otherBeyondPosition);

            return positionNode;
        }


        /// <summary>
        /// 해당 enum 타입 방향에 있는 바로 옆 퍼즐의 키 값을 리턴.
        /// </summary>
        public PuzzlePositionModel GetPositionByDirectionType (PuzzlePositionModel puzzlePositionModel,
            CheckDirectionTypes checkDirectionTypes)
        {
            return GetKeyByAngle (puzzlePositionModel, (float) checkDirectionTypes);
        }


        /// <summary>
        /// 해당 enum 타입 방향에 있는 바로 옆 퍼즐의 키 값을 리턴.
        /// </summary>
        public PuzzlePositionModel GetPositionByDirectionType (PuzzleModel model, CheckDirectionTypes checkDirectionTypes)
        {
            return GetKeyByAngle (model.puzzlePositionModel, (float) checkDirectionTypes);
        }


        /// <summary>
        /// 해당 방향에 있는 바로 옆 퍼즐의 키 값을 리턴. 
        /// </summary>
        public PuzzlePositionModel GetKeyByAngle (PuzzlePositionModel puzzlePositionModel, float angle)
        {
            if (!AllLineModels.ContainsKey (puzzlePositionModel.Column))
            {
                return PuzzlePositionModel.EmptyPuzzlePositionModel;
            }

            // To upper direction.
            if (Enumerable.Range (150, 60).Contains ((int) angle))
            {
                return new PuzzlePositionModel (puzzlePositionModel.Column, puzzlePositionModel.Row + 1);
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
            return new PuzzlePositionModel (puzzlePositionModel.Column, puzzlePositionModel.Row - 1);

            PuzzlePositionModel GetPuzzlePosition (bool toLeft = true, bool toUpper = true)
            {
                var checkColumn = puzzlePositionModel.Column + (toLeft ? -1 : 1);

                if (!AllLineModels.ContainsKey (checkColumn))
                    return PuzzlePositionModel.EmptyPuzzlePositionModel;

                var anEvenNumberColumn = puzzlePositionModel.Column % 2 == 0;
                var posCoeff = anEvenNumberColumn && toUpper ? 0 : anEvenNumberColumn ? -1 : toUpper ? 1 : 0;

                return new PuzzlePositionModel (checkColumn, puzzlePositionModel.Row + posCoeff);
            }
        }

        /// <summary>
        /// 해당 방향에 있는 바로 옆 퍼즐의 키 값을 리턴. 
        /// </summary>
        public CheckDirectionTypes GetCheckDirectionTypesByAngle (float angle)
        {
            // To upper direction.
            if (Enumerable.Range (150, 60).Contains ((int) angle))
            {
                return CheckDirectionTypes.ToUpward;
            }

            // To upper right direction.
            if (Enumerable.Range (210, 60).Contains ((int) angle))
            {
                return CheckDirectionTypes.ToUpRight;
            }

            // To lower right direction.
            if (Enumerable.Range (270, 60).Contains ((int) angle))
            {
                return CheckDirectionTypes.ToDownRight;
            }

            // To upper left direction.
            if (Enumerable.Range (90, 60).Contains ((int) angle))
            {
                return CheckDirectionTypes.ToUpLeft;
            }

            // To lower left direction.
            if (Enumerable.Range (30, 60).Contains ((int) angle))
            {
                return CheckDirectionTypes.ToDownLeft;
            }

            // To lower direction.
            return CheckDirectionTypes.ToDownward;
        }


        private (CheckDirectionTypes, CheckDirectionTypes) GetCheckDirectionTypesByPuzzleCheckType (
            PuzzleCheckTypes puzzleCheckTypes)
        {
            switch (puzzleCheckTypes)
            {
                case PuzzleCheckTypes.ToVertical:
                    return (CheckDirectionTypes.ToUpward, CheckDirectionTypes.ToDownward);

                case PuzzleCheckTypes.ToUpLeftDownRight:
                    return (CheckDirectionTypes.ToUpLeft, CheckDirectionTypes.ToDownRight);

                case PuzzleCheckTypes.ToUpRightDownLeft:
                    return (CheckDirectionTypes.ToUpRight, CheckDirectionTypes.ToDownLeft);

                default:
                    throw new ArgumentOutOfRangeException (nameof (puzzleCheckTypes), puzzleCheckTypes, null);
            }
        }

        /// <summary>
        /// 해당 위치에서 6방향의 위치를 리턴.
        /// </summary>
        private IEnumerable<PuzzlePositionModel> GetAroundPositionModel (PuzzlePositionModel puzzlePositionModel)
        {
            return new[]
            {
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToUpward),
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToDownward),
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToUpLeft),
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToUpRight),
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToDownLeft),
                GetPositionByDirectionType (puzzlePositionModel, CheckDirectionTypes.ToDownRight)
            };
        }

        #endregion


        #region Player Control Model

        /// <summary>
        /// 퍼즐 컨트롤 모델 변경.
        /// </summary>
        public void ChangeControlMode (bool isPlayerControl, PuzzleCheckTypes puzzleCheckTypes,
            IEnumerable<PuzzleModel> controledPuzzles)
        {
            _puzzlePlayerControlModel.IsPlayerControl = isPlayerControl;
            _puzzlePlayerControlModel.PlayerControlDirection = puzzleCheckTypes;
            _puzzlePlayerControlModel.AddControledPuzzles (controledPuzzles);
        }

        #endregion
    }
}