using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using Random = UnityEngine.Random;
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

        #endregion


        #region Methods

        private void Initialize_Field ()
        {
            _fieldTypeAction.Add (FieldSpecialType.Battle, fieldModel => _battleViewmodel.StartBattle ());
            _fieldTypeAction.Add (FieldSpecialType.BossBattle, fieldModel => _battleViewmodel.StartBattle ());
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
            _adventureFieldData = TableDataManager.Instance.AdventureFieldDict.Values.RandomSource ();

            var allField = CreateField (sizes);
            _adventureModel.SetBaseField (allField);

            var forestField = CreateForestField (allField);
            CreateSpecialFieldType (allField);
            var startField = CreateStartField (allField);

            return (forestField, startField);
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
                    allFieldDict[c].Add (new FieldModel (positionModel));
                }
            }

            return allFieldDict;
        }


        /// <summary>
        /// 숲 필드 생성.
        /// </summary>
        private Dictionary<int, List<FieldModel>> CreateForestField (Dictionary<int, List<FieldModel>> allField)
        {
            var forestDict = new Dictionary<int, List<FieldModel>> ();

            // 숲 수량.
            var forestCount = Random.Range (_adventureFieldData.ForestCount[0], _adventureFieldData.ForestCount[1]);

            for (var i = 0; i < forestCount; i++)
            {
                var forestFields = new List<FieldModel> ();
                var treeCount = Random.Range (_adventureFieldData.TreeCount[0], _adventureFieldData.TreeCount[1]);
                var startForestLand = allField.SelectMany (x => x.Value).RandomSource ();
                var treeField = startForestLand;
                forestFields.Add (treeField);
                treeField.ChangeFieldGroundType (FieldGroundType.Forest);

                for (var z = 0; z < treeCount; z++)
                {
                    var aroundPosition =
                        PositionHelper.Instance.GetAroundPositionModel (allField, treeField.LandPosition);

                    // 근처에 숲이 될만한 지형이 없음.
                    if (aroundPosition.Select (GetFieldModel)
                        .All (x => x.FieldGroundType.Value == FieldGroundType.Forest))
                    {
                        treeField = forestFields
                            .SelectMany (x => PositionHelper.Instance.GetAroundPositionModel (allField, x.LandPosition))
                            .Select (GetFieldModel)
                            .RandomSource (x => x.FieldGroundType.Value != FieldGroundType.Forest);
                    }
                    else
                    {
                        treeField = aroundPosition.Select (GetFieldModel)
                            .RandomSource (x => x.FieldGroundType.Value != FieldGroundType.Forest);
                    }

                    treeField.ChangeFieldGroundType (FieldGroundType.Forest);
                    forestFields.Add (treeField);
                }

                forestDict.Add (i, forestFields);
            }

            return forestDict;
        }


        private void CreateSpecialFieldType (Dictionary<int, List<FieldModel>> allField)
        {
            var fieldTypeArray = ((FieldSpecialType[]) Enum.GetValues (typeof (FieldSpecialType))).Skip (1).ToList ();

            foreach (var fieldType in fieldTypeArray)
            {
                var randomField = allField.SelectMany (x => x.Value)
                    .Where (x => x.FieldSpecialType.Value == FieldSpecialType.None)
                    .RandomSource ();
                randomField.ChangeFieldSpecialType (FieldSpecialType.Insightful);
            }
        }


        private FieldModel CreateStartField (Dictionary<int, List<FieldModel>> allField)
        {
            var startField = allField
                .SelectMany (x => x.Value)
                .RandomSource (x =>
                    x.FieldGroundType.Value == FieldGroundType.None &&
                    x.FieldSpecialType.Value == FieldSpecialType.None);
            return startField;
        }


        /// <summary>
        /// 필드 속성 설정.
        /// </summary>
        public void SetFieldType (FieldModel fieldModel)
        {
            if (_fieldTypeAction.ContainsKey (fieldModel.FieldSpecialType.Value))
                _fieldTypeAction[fieldModel.FieldSpecialType.Value] (fieldModel);

            fieldModel.ChangeFieldSpecialType (FieldSpecialType.None);
        }

        /// <summary>
        /// 천리안 타입.
        /// </summary>
        public IEnumerable<PositionModel> GetInsightfulPosition ()
        {
            var randomField = _adventureModel.AllFieldModel.SelectMany (x => x.Value)
                .RandomSource (x => x.FieldRevealState.Value == FieldRevealState.Sealed);

            if (randomField == default)
            {
                return default;
            }

            var position = randomField.LandPosition;

            var insightPosition =
                PositionHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, position, 2);

            insightPosition.Select (GetFieldModel)
                .Where (fieldModel => fieldModel.FieldRevealState.Value == FieldRevealState.Sealed)
                .Foreach (fieldModel => { fieldModel.ChangeState (FieldRevealState.Revealed); });

            return insightPosition;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}