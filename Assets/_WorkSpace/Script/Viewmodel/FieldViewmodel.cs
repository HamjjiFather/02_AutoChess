using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
    public class FieldViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private int[] _fieldSizes;
        
        private readonly Dictionary<int, List<LandModel>> _allLineModels = new Dictionary<int, List<LandModel>> ();

        private ReactiveProperty<PositionModel> _nowPosition = new ReactiveProperty<PositionModel> ();
        
        private readonly PositionModel _startPosition = new PositionModel (10, 5);
        public PositionModel StartPosition => _startPosition;

        #endregion


        public override void Initialize ()
        {

        }


        #region Methods

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

            return _allLineModels;
        }


        public void StartGame ()
        {
            var newAroundPosition = PositionHelper.Instance.GetAroundPositionModelWith (_allLineModels, _startPosition);
            _nowPosition.Value = _startPosition;
            
            newAroundPosition.Foreach (x =>
            {
                ((FieldModel)_allLineModels[x.Column][x.Row]).ChangeState (FieldRevealState.OnSight);
            });
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
            var originAroundPosition = PositionHelper.Instance.GetAroundPositionModel (_allLineModels, _nowPosition.Value);
            var newAroundPosition = PositionHelper.Instance.GetAroundPositionModelWith (_allLineModels, newPosition);
            var revealedPositions = originAroundPosition.Except (newAroundPosition);
            revealedPositions
                .Where (ContainPosition)
                .Select (x => _allLineModels[x.Column][x.Row])
                .Foreach (x => ((FieldModel)x).ChangeState (FieldRevealState.Revealed));
            
            newAroundPosition
                .Where (ContainPosition)
                .Select (x => _allLineModels[x.Column][x.Row])
                .Foreach (x => ((FieldModel)x).ChangeState (FieldRevealState.OnSight));

            _nowPosition.Value = newPosition;
        }


        private FieldTargetResultModel TryGetMovableAroundPosition (PositionModel nowPositionModel, PositionModel targetPosition)
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
                    .MinSources (positionModel => PositionHelper.Instance.Distance (_allLineModels, positionModel, targetPosition))
                    .First();

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


        #endregion


        #region EventMethods

        #endregion
    }
}