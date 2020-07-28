using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using UniRx;
using Zenject;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action>;

namespace AutoChess
{
    public partial class AdventureViewmodel : ViewModelBase
    {
        #region Fields & Property

        public ReactiveCommand StartAdventureCommand { get; private set; } = new ReactiveCommand ();

        public ReactiveCommand EndAdventureCommand { get; private set; }

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;
        
#pragma warning restore CS0649

        private readonly EnumActionToDict _fieldTypeAction = new EnumActionToDict();
        
        private int[] _fieldSizes;

        private readonly Dictionary<int, List<LandModel>> _allLineModels = new Dictionary<int, List<LandModel>> ();

        private ReactiveProperty<PositionModel> _nowPosition = new ReactiveProperty<PositionModel> ();

        private readonly PositionModel _startPosition = new PositionModel (10, 5);
        public PositionModel StartPosition => _startPosition;
        
        

        #endregion


        public override void Initialize ()
        {
            _fieldTypeAction.Add (FieldType.RecoverSmall, () => RecoverHealth (CharacterSideType.Player, 0.1f));
            _fieldTypeAction.Add (FieldType.RecoverMedium, () => RecoverHealth (CharacterSideType.Player, 0.25f));
            _fieldTypeAction.Add (FieldType.RecoverLarge, () => RecoverHealth (CharacterSideType.Player, 0.5f));
        }


        #region Methods

        /// <summary>
        /// 모험 시작.
        /// </summary>
        public void StartAdventure ()
        {
            var newAroundPosition = PositionHelper.Instance.GetAroundPositionModelWith (_allLineModels, _startPosition);
            _nowPosition.Value = _startPosition;

            newAroundPosition.Foreach (x =>
            {
                ((FieldModel) _allLineModels[x.Column][x.Row]).ChangeState (FieldRevealState.OnSight);
            });

            EndAdventureCommand = new ReactiveCommand ();
            _battleViewmodel.StartAdventure ();
        }

        
        public void EndAdventure ()
        {
            EndAdventureCommand.Execute ();
            EndAdventureCommand.DisposeSafe ();
            _battleViewmodel.EndAdventure ();
        }
        
        
        /// <summary>
        /// 탐험 필드 생성.
        /// </summary>
        public Dictionary<int, List<LandModel>> SetFieldSizes (int[] sizes)
        {
            _fieldSizes = sizes;

            for (var c = 0; c < _fieldSizes.Length; c++)
            {
                _allLineModels.Add (c, new List<LandModel> ());
                for (var r = 0; r < _fieldSizes[c]; r++)
                {
                    var positionModel = new PositionModel (c, r);
                    _allLineModels[c].Add (new FieldModel (positionModel));
                }
            }

            var fieldTypeArray = ((FieldType[]) Enum.GetValues (typeof (FieldType))).Skip (1).ToList ();

            foreach (var fieldType in fieldTypeArray)
            {
                var randomField = _allLineModels.SelectMany (x => x.Value)
                    .Where (x => ((FieldModel) x).FieldType.Value == FieldType.None)
                    .RandomSource ();
                ((FieldModel) randomField).ChangeFieldType (FieldType.Battle);
            }

            return _allLineModels;
        }


        public FieldTargetResultModel FindMovingPositions (PositionModel newPosition)
        {
            if (_nowPosition.Value.Equals (newPosition))
                return null;

            var result = TryGetMovableAroundPosition (_nowPosition.Value, newPosition);
            return result;
        }


        public void SetSight (PositionModel newPosition)
        {
            var originAroundPosition =
                PositionHelper.Instance.GetAroundPositionModel (_allLineModels, _nowPosition.Value);
            var newAroundPosition = PositionHelper.Instance.GetAroundPositionModelWith (_allLineModels, newPosition);
            var revealedPositions = originAroundPosition.Except (newAroundPosition);
            revealedPositions
                .Where (ContainPosition)
                .Select (x => _allLineModels[x.Column][x.Row])
                .Foreach (x => ((FieldModel) x).ChangeState (FieldRevealState.Revealed));

            newAroundPosition
                .Where (ContainPosition)
                .Select (x => _allLineModels[x.Column][x.Row])
                .Foreach (x => ((FieldModel) x).ChangeState (FieldRevealState.OnSight));

            _nowPosition.Value = newPosition;
        }


        public void SetFieldType (FieldModel fieldModel)
        {
            if(_fieldTypeAction.ContainsKey (fieldModel.FieldType.Value))
                _fieldTypeAction[fieldModel.FieldType.Value] ();
            
            fieldModel.FieldType.Value = FieldType.None;
        }

        
        private void RecoverHealth (CharacterSideType sideType, float recoverPercent)
        {
            var characters = _battleViewmodel.GetAllOfEqualElements (sideType);
            characters.Foreach (element => element.battleCharacterPackage.battleSystemModule.SetHealthByPercent (recoverPercent));
        }


        #region Position

        private FieldTargetResultModel TryGetMovableAroundPosition (PositionModel nowPositionModel,
            PositionModel targetPosition)
        {
            var fieldTargetResult = new FieldTargetResultModel ();
            var aroundPositions = PositionHelper.Instance.GetAroundPositionModel (_allLineModels, nowPositionModel);

            while (true)
            {
                if (aroundPositions.Any (x => x.Equals (targetPosition)))
                {
                    fieldTargetResult.FoundPositions.Add (targetPosition);
                    break;
                }

                var foundPosition = aroundPositions
                    .Where (FoundablePosition)
                    .MinSources (positionModel =>
                        PositionHelper.Instance.Distance (_allLineModels, positionModel, targetPosition))
                    .First ();

                fieldTargetResult.FoundPositions.Add (foundPosition);
                aroundPositions = PositionHelper.Instance.GetAroundPositionModel (_allLineModels, foundPosition);
            }

            return fieldTargetResult;
        }


        public bool ContainPosition (PositionModel positionModel)
        {
            return _allLineModels.ContainsKey (positionModel.Column) &&
                   _allLineModels[positionModel.Column].ContainIndex (positionModel.Row);
        }


        public bool FoundablePosition (PositionModel positionModel)
        {
            return ContainPosition (positionModel) &&
                   ((FieldModel) _allLineModels[positionModel.Column][positionModel.Row]).RevealState.Value !=
                   FieldRevealState.Sealed;
        }

        public FieldModel GetFieldModel (PositionModel positionModel)
        {
            if (ContainPosition (positionModel))
            {
                return _allLineModels[positionModel.Column][positionModel.Row] as FieldModel;
            }

            return _allLineModels.SelectMany (x => x.Value).First () as FieldModel;
        }

        #endregion

        #endregion


        #region EventMethods

        #endregion
    }
}