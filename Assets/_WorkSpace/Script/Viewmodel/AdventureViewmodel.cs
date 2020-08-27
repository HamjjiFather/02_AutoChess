using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.DesignPattern;
using UniRx;
using Zenject;

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

        private readonly ReactiveProperty<PositionModel> _nowPosition = new ReactiveProperty<PositionModel> ();
        public PositionModel NowPosition => _nowPosition.Value;

        /// <summary>
        /// 모험 모델.
        /// </summary>
        private AdventureModel _adventureModel;

        public AdventureModel AdventureModel => _adventureModel;

        #endregion


        public override void Initialize ()
        {
            Initialize_Field ();
        }


        #region Methods

        /// <summary>
        /// 모험 시작.
        /// </summary>
        public async UniTask<AdventureModel> StartAdventure (int[] sizes)
        {
            StartAdventure_Rewards ();
            
            _adventureModel = new AdventureModel (Constant.MaxAdventureCount);

            var (forestField, startField) = CreateAllFields (sizes);
            _adventureModel.SetField (forestField, startField);

            var newAroundPosition =
                PositionHelper.Instance.GetAroundPositionModelWith (_adventureModel.AllFieldModel,
                    startField.LandPosition);
            _nowPosition.Value = startField.LandPosition;

            newAroundPosition.Foreach (x =>
            {
                if (ContainPosition (x))
                    _adventureModel.AllFieldModel[x.Column][x.Row].ChangeState (FieldRevealState.OnSight);
            });

            EndAdventureCommand = new ReactiveCommand ();
            _battleViewmodel.StartAdventure ();

            await UniTask.Delay (TimeSpan.FromSeconds (1));

            return _adventureModel;
        }


        public void EndAdventure ()
        {
            EndAdventureCommand.Execute ();
            EndAdventureCommand.DisposeSafe ();
            _battleViewmodel.EndAdventure ();
        }


        public FieldTargetResultModel FindMovingPositions (PositionModel newPosition)
        {
            if (_nowPosition.Value.Equals (newPosition))
                return null;

            var result = TryGetMovableAroundPosition (_nowPosition.Value, newPosition);
            return result;
        }


        public void CompleteMove (PositionModel completePositionModel)
        {
            AdventureModel.DecreaseAdventureCount ();
            SetSight (completePositionModel);
            _nowPosition.Value = completePositionModel;
        }

        public void SetSight (PositionModel newPosition)
        {
            var originAroundPosition =
                PositionHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, _nowPosition.Value);
            var newAroundPosition =
                PositionHelper.Instance.GetAroundPositionModelWith (_adventureModel.AllFieldModel, newPosition);
            var revealedPositions = originAroundPosition.Except (newAroundPosition);
            revealedPositions
                .Where (ContainPosition)
                .Select (x => _adventureModel.AllFieldModel[x.Column][x.Row])
                .Foreach (x => x.ChangeState (FieldRevealState.Revealed));

            newAroundPosition
                .Where (ContainPosition)
                .Select (x => _adventureModel.AllFieldModel[x.Column][x.Row])
                .Foreach (x => x.ChangeState (FieldRevealState.OnSight));
        }


        public void RecoverHealth (CharacterSideType sideType, float recoverPercent)
        {
            var characters = _battleViewmodel.GetAllOfEqualElements (sideType);
            characters.Foreach (element =>
                element.battleCharacterPackage.battleSystemModule.SetHealthByPercent (recoverPercent));
        }


        #region Position

        private FieldTargetResultModel TryGetMovableAroundPosition (PositionModel nowPositionModel,
            PositionModel targetPosition)
        {
            var fieldTargetResult = new FieldTargetResultModel ();
            var aroundPositions =
                PositionHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, nowPositionModel);

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
                        PositionHelper.Instance.Distance (_adventureModel.AllFieldModel, positionModel, targetPosition))
                    .First ();

                fieldTargetResult.FoundPositions.Add (foundPosition);
                aroundPositions =
                    PositionHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, foundPosition);
            }

            return fieldTargetResult;
        }


        public bool ContainPosition (PositionModel positionModel)
        {
            return _adventureModel.AllFieldModel.ContainsKey (positionModel.Column) &&
                   _adventureModel.AllFieldModel[positionModel.Column].ContainIndex (positionModel.Row);
        }


        public bool FoundablePosition (PositionModel positionModel)
        {
            return ContainPosition (positionModel) &&
                   _adventureModel.AllFieldModel[positionModel.Column][positionModel.Row].FieldRevealState.Value !=
                   FieldRevealState.Sealed;
        }

        public FieldModel GetFieldModel (PositionModel positionModel)
        {
            return ContainPosition (positionModel)
                ? _adventureModel.AllFieldModel[positionModel.Column][positionModel.Row]
                : default;
        }

        #endregion

        #endregion


        #region EventMethods

        #endregion
    }
}