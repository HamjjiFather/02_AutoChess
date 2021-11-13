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

        public FieldModel NowField => _positionFieldDict[NowPosition];

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
            _adventureModel.AllFieldModel.SelectMany (x => x.Value)
                .Foreach (x => x.ChangeState (FieldRevealState.Sealed));

            var newAroundPosition =
                PathFindingHelper.Instance.GetAroundPositionModelWith (_adventureModel.AllFieldModel,
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


        public FieldTargetResultModel FindMovingPositions (FieldModel fieldModel)
        {
            if (_nowPosition.Value.Equals (fieldModel.LandPosition))
                return null;
            
            var result = TryGetMovableAroundPosition (_nowPosition.Value, fieldModel.LandPosition);
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
                PathFindingHelper.Instance.GetAroundPositionModel (_adventureModel.AllFieldModel, _nowPosition.Value);
            var newAroundPosition =
                PathFindingHelper.Instance.GetAroundPositionModelWith (_adventureModel.AllFieldModel, newPosition);
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

        #endregion
    }
}