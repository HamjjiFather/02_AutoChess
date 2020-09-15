using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using MasterData;
using UnityEngine;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action<AutoChess.FieldModel>>;

namespace AutoChess
{
    public partial class AdventureViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 모험 필드 테이블 데이터.
        /// </summary>
        private AdventureField _adventureFieldData;

        /// <summary>
        /// 필드 이벤트.
        /// </summary>
        private readonly EnumActionToDict _fieldTypeAction = new EnumActionToDict ();
        
        /// <summary>
        /// 위치별 필드 모델.
        /// </summary>
        private readonly Dictionary<PositionModel, FieldModel> _positionFieldDict = new Dictionary<PositionModel, FieldModel> ();


        #endregion


        #region Methods

        private void Initialize_Field ()
        {
            _fieldTypeAction.Add (FieldSpecialType.Battle, fieldModel => _battleViewmodel.StartBattle ());
            _fieldTypeAction.Add (FieldSpecialType.BossBattle, fieldModel => _battleViewmodel.StartBattle ());
            _fieldTypeAction.Add (FieldSpecialType.Reward, _ => { RewardCheck (); });
            _fieldTypeAction.Add (FieldSpecialType.RecoverSmall,
                fieldModel => RecoverHealth (CharacterSideType.Player, 0.1f));
            _fieldTypeAction.Add (FieldSpecialType.RecoverMedium,
                fieldModel => RecoverHealth (CharacterSideType.Player, 0.25f));
            _fieldTypeAction.Add (FieldSpecialType.RecoverLarge,
                fieldModel => RecoverHealth (CharacterSideType.Player, 0.5f));
            _fieldTypeAction.Add (FieldSpecialType.Insightful, _ => { });
        }


        /// <summary>
        /// 탐험 필드 생성.
        /// </summary>
        private (Dictionary<int, List<FieldModel>>, FieldModel) CreateAllFields (IReadOnlyList<int> sizes)
        {
            _adventureFieldData = AdventureField.Manager.Values.First ();

            var allField = CreateField (sizes);
            _adventureModel.SetBaseField (allField);

            var startField = CreateStartField ();
            CreatePath (startField);
            CreateNextField ();
            var forestField = CreateForestField ();
            CreateSpecialFieldType ();

            return (forestField, startField);
        }


        /// <summary>
        /// 다음 층 필드 제작.
        /// </summary>
        public void NewField ()
        {
            Debug.Log ("New Field");
            _adventureModel.AllFieldModel.SelectMany (x => x.Value).ForEach (x => x.Reset ());
            var startField = CreateStartField ();
            CreateNextField ();
            var forestField = CreateForestField ();
            CreateSpecialFieldType ();

            _adventureModel.SetField (forestField, startField);

            var newAroundPosition =
                PathFindingHelper.Instance.GetAroundPositionModelWith (_adventureModel.AllFieldModel,
                    startField.LandPosition);
            _nowPosition.Value = startField.LandPosition;

            newAroundPosition.ForEach (x =>
            {
                if (ContainPosition (x))
                    _adventureModel.AllFieldModel[x.Column][x.Row].ChangeState (FieldRevealState.OnSight);
            });
        }



        /// <summary>
        /// 기본 필드 생성.
        /// </summary>
        private Dictionary<int, List<FieldModel>> CreateField (IReadOnlyList<int> sizes)
        {
            var allFieldDict = new Dictionary<int, List<FieldModel>> ();

            for (var c = 0; c < sizes.Count; c++)
            {
                allFieldDict.Add (c, new List<FieldModel> ());
                for (var r = 0; r < sizes[c]; r++)
                {
                    var positionModel = new PositionModel (c, r);
                    var newField = new FieldModel (positionModel);
                    allFieldDict[c].Add (newField);
                    _positionFieldDict.Add (positionModel, newField);
                }
            }

            return allFieldDict;
        }


        /// <summary>
        /// 시작 필드 생성.
        /// </summary>
        private FieldModel CreateStartField ()
        {
            var startField = _adventureModel.AllFieldModel
                .SelectMany (x => x.Value)
                .Choice (x =>
                    x.FieldGroundType.Value == FieldGroundType.None &&
                    x.FieldSpecialType.Value == FieldSpecialType.None);
            return startField;
        }


        /// <summary>
        /// 필드 제작.
        /// </summary>
        private void CreatePath (FieldModel startField)
        {
            var roomField = new List<FieldModel> {startField};
            var allField = _positionFieldDict.Values;

            var roomCount = _adventureFieldData.FieldPointCount.RandomRange ();
            roomField.AddRange (allField.RandomSources (roomCount));
            roomField.ForEach (point =>
            {
                var aroundPos =
                    PathFindingHelper.Instance.GetAroundPositionModelByGradual (_adventureModel.AllFieldModel,
                        point.LandPosition,
                        _adventureFieldData.FieldCount.RandomRange ());

                aroundPos.ForEach (x => _positionFieldDict[x].FieldExistType.Value = FieldExistType.Exist);
            });

            var connectedField = roomField.Where (x => IsConnectByExistState (x, startField));
            var unconnectedField = roomField.Where (x => !IsConnectByExistState (x, startField));

            unconnectedField.ForEach (field =>
            {
                var alignToDistance = connectedField.MinSources (x =>
                    PathFindingHelper.Instance.Distance (_adventureModel.AllFieldModel, field.LandPosition,
                        x.LandPosition)).First ();

                var pathFields = GetPath (alignToDistance.LandPosition, field.LandPosition);
                pathFields.Select (x => _positionFieldDict[x])
                    .ForEach (x => x.FieldExistType.Value = FieldExistType.Exist);
            });
        }


        /// <summary>
        /// 다음층 필드 생성.
        /// </summary>
        private void CreateNextField ()
        {
            var nextField = _adventureModel.AllFieldModel
                .SelectMany (x => x.Value)
                .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                .Choice (x =>
                    x.FieldGroundType.Value == FieldGroundType.None &&
                    x.FieldSpecialType.Value == FieldSpecialType.None);

            nextField.ChangeFieldSpecialType (FieldSpecialType.Exit);
        }

        /// <summary>
        /// 숲 필드 생성.
        /// </summary>
        private Dictionary<int, List<FieldModel>> CreateForestField ()
        {
            var forestDict = new Dictionary<int, List<FieldModel>> ();

            // 숲 수량.
            var allField = _positionFieldDict.Values.Where (x => x.FieldExistType.Value == FieldExistType.Exist);
            var forestCount = Random.Range (_adventureFieldData.ForestCount[0], _adventureFieldData.ForestCount[1]);
            var treeCount = Random.Range (_adventureFieldData.TreeCount[0], _adventureFieldData.TreeCount[1]);
            var forestPoints = allField.RandomSources (forestCount);
            forestPoints.ForEach (field =>
            {
                var around =
                    PathFindingHelper.Instance.GetAroundPositionModelByGradual (_adventureModel.AllFieldModel,
                        field.LandPosition, treeCount);

                around.ForEach (x => _positionFieldDict[x].ChangeFieldGroundType (FieldGroundType.Forest));
            });

            return forestDict;
        }


        /// <summary>
        /// 이벤트 지형들을 생성.
        /// </summary>
        private void CreateSpecialFieldType ()
        {
            var rewardCount = Random.Range (_adventureFieldData.RewardCount[0], _adventureFieldData.RewardCount[1]);
            rewardCount.ForWhile (() =>
            {
                var randomField = AvailFields ().Choice ();
                randomField.ChangeFieldSpecialType (FieldSpecialType.Reward);
            });

            var battleCount = Random.Range (_adventureFieldData.BattleCount[0], _adventureFieldData.BattleCount[1]);
            battleCount.ForWhile (() =>
            {
                var randomField = AvailFields ().Choice ();
                randomField.ChangeFieldSpecialType (FieldSpecialType.Battle);
            });

            var bossBattleCount = Random.Range (_adventureFieldData.BossBattleCount[0],
                _adventureFieldData.BossBattleCount[1]);
            bossBattleCount.ForWhile (() =>
            {
                var randomField = AvailFields ().Choice ();
                randomField.ChangeFieldSpecialType (FieldSpecialType.BossBattle);
            });

            var insightfulCount = Random.Range (_adventureFieldData.OtherCount[0], _adventureFieldData.OtherCount[1]);
            insightfulCount.ForWhile (() =>
            {
                var randomField = AvailFields ().Choice ();
                randomField.ChangeFieldSpecialType (FieldSpecialType.Insightful);
            });

            IEnumerable<FieldModel> AvailFields ()
            {
                return _adventureModel.AllFieldModel.SelectMany (x => x.Value)
                    .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                    .Where (x => x.FieldSpecialType.Value == FieldSpecialType.None);
            }
        }


        /// <summary>
        /// 필드 속성 설정.
        /// </summary>
        public void FieldType (FieldModel fieldModel)
        {
            if (_fieldTypeAction.ContainsKey (fieldModel.FieldSpecialType.Value))
                _fieldTypeAction[fieldModel.FieldSpecialType.Value] (fieldModel);

            if (fieldModel.FieldSpecialType.Value == FieldSpecialType.Exit)
                return;

            fieldModel.ChangeFieldSpecialType (FieldSpecialType.None);
        }

        /// <summary>
        /// 천리안 타입.
        /// </summary>
        public IEnumerable<PositionModel> GetInsightfulPosition ()
        {
            var randomField = _adventureModel.AllFieldModel
                .SelectMany (x => x.Value)
                .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                .Choice (x => x.FieldRevealState.Value == FieldRevealState.Sealed);

            if (randomField == default)
            {
                return default;
            }

            var position = randomField.LandPosition;
            var insightPosition =
                PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, position, 2);

            insightPosition.Select (GetFieldModel)
                .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                .Where (fieldModel => fieldModel.FieldRevealState.Value == FieldRevealState.Sealed)
                .ForEach (fieldModel => { fieldModel.ChangeState (FieldRevealState.Revealed); });

            return insightPosition;
        }


        public bool IsConnectByExistState (FieldModel origin, FieldModel target)
        {
            var tempResults = new List<FieldModel> {origin};
            PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, origin.LandPosition);
            var aroundFields = new List<FieldModel>
            {
                origin
             };

            while (true)
            {
                if (!aroundFields.Any ())
                    return false;

                if (aroundFields.Contains (target))
                    return true;

                tempResults.AddRange (aroundFields);
                aroundFields = aroundFields.SelectMany (x =>
                        PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel,
                            x.LandPosition))
                    .Select (x => _positionFieldDict[x])
                    .Distinct ()
                    .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                    .Where (x => !x.LandPosition.Equals (PathFindingHelper.Instance.EmptyPosition))
                    .Except (tempResults).ToList ();
            }
        }
        
        
        public bool IsConnectByMovable (FieldModel origin, FieldModel target)
        {
            var tempResults = new List<FieldModel> {origin};
            PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, origin.LandPosition);
            var aroundFields = new List<FieldModel>
            {
                origin
            };

            while (true)
            {
                if (!aroundFields.Any ())
                    return false;

                if (aroundFields.Contains (target))
                    return true;

                tempResults.AddRange (aroundFields);
                aroundFields = aroundFields.SelectMany (x =>
                        PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel,
                            x.LandPosition))
                    .Select (x => _positionFieldDict[x])
                    .Distinct ()
                    .Where (x => x.FieldExistType.Value == FieldExistType.Exist)
                    .Where (x => x.FieldRevealState.Value != FieldRevealState.Sealed)
                    .Where (x => !x.LandPosition.Equals (PathFindingHelper.Instance.EmptyPosition))
                    .Except (tempResults).ToList ();
            }
        }
        
        
        /// <summary>
        /// 현재 위치에서 해당 위치까지 경로를 리턴.
        /// </summary>
        private IEnumerable<PositionModel> GetPath (PositionModel nowPositionModel, PositionModel targetPosition)
        {
            var path = new List<PositionModel> ();
            var aroundPositions =
                PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, nowPositionModel);

            while (true)
            {
                if (!aroundPositions.Any ())
                    break;

                if (aroundPositions.Any (x => x.Equals (targetPosition)))
                {
                    path.Add (targetPosition);
                    break;
                }

                var foundPosition = aroundPositions.Where (ExistFieldData).MinSources (positionModel =>
                        PathFindingHelper.Instance.Distance (_adventureModel.AllFieldModel, positionModel,
                            targetPosition))
                    .First ();

                path.Add (foundPosition);
                aroundPositions =
                    PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, foundPosition);
            }

            return path;
        }


        /// <summary>
        /// 현재 위치에서 해당 위치까지 경로를 리턴.
        /// </summary>
        private FieldTargetResultModel TryGetMovableAroundPosition (PositionModel nowPositionModel,
            PositionModel targetPosition)
        {
            var fieldTargetResult = new FieldTargetResultModel ();
            var aroundPositions =
                PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, nowPositionModel);

            while (true)
            {
                if (!aroundPositions.Any ())
                    break;

                if (aroundPositions.Any (x => x.Equals (targetPosition)))
                {
                    fieldTargetResult.IsConnected = true;
                    fieldTargetResult.FoundPositions.Add (targetPosition);
                    break;
                }

                var foundPosition = aroundPositions.Where (FoundablePosition).MinSources (positionModel =>
                        PathFindingHelper.Instance.Distance (_adventureModel.AllFieldModel, positionModel,
                            targetPosition))
                    .First ();

                fieldTargetResult.FoundPositions.Add (foundPosition);
                aroundPositions =
                    PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, foundPosition);
            }

            return fieldTargetResult;
        }


        public bool ExistFieldData (PositionModel positionModel)
        {
            return _adventureModel.AllFieldModel.ContainsKey (positionModel.Column) &&
                   _adventureModel.AllFieldModel[positionModel.Column].ContainIndex (positionModel.Row);
        }


        public bool ContainPosition (PositionModel positionModel)
        {
            return ExistFieldData (positionModel) &&
                   _adventureModel.AllFieldModel[positionModel.Column][positionModel.Row].FieldExistType.Value ==
                   FieldExistType.Exist;
        }


        public bool FoundablePosition (PositionModel positionModel)
        {
            if (!ExistFieldData (positionModel)) return false;
            var field = _adventureModel.AllFieldModel[positionModel.Column][positionModel.Row];
            return field.FieldRevealState.Value != FieldRevealState.Sealed &&
                   field.FieldExistType.Value == FieldExistType.Exist;
        }

        public FieldModel GetFieldModel (PositionModel positionModel)
        {
            return ContainPosition (positionModel)
                ? _adventureModel.AllFieldModel[positionModel.Column][positionModel.Row]
                : default;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}