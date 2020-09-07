using UniRx;

namespace AutoChess
{
    public enum FieldExistType
    {
        Empty,
        Exist
    }

    public enum FieldGroundType
    {
        None,
        Forest,
        Rock,
    }

    public enum FieldSpecialType
    {
        None,
        Battle,
        BossBattle,
        Insightful,
        Event,
        Reward,
        FakeReward,
        RecoverSmall,
        RecoverMedium,
        RecoverLarge,
        Knowledge,
        Exit,

        Store,
        Smith,
        Bar,
    }


    public enum FieldRevealState
    {
        /// <summary>
        /// 볼 수없음.
        /// </summary>
        Sealed,

        /// <summary>
        /// 드러났으나, 현재 시야에는 없음.
        /// </summary>
        Revealed,

        /// <summary>
        /// 현재 시야에 있음.
        /// </summary>
        OnSight,
    }


    public class FieldModel : LandModel
    {
        #region Fields & Property

        public readonly ReactiveProperty<FieldExistType> FieldExistType =
            new ReactiveProperty<FieldExistType> (AutoChess.FieldExistType.Empty);

        public readonly ReactiveProperty<FieldGroundType> FieldGroundType =
            new ReactiveProperty<FieldGroundType> (AutoChess.FieldGroundType.None);

        public readonly ReactiveProperty<FieldSpecialType> FieldSpecialType =
            new ReactiveProperty<FieldSpecialType> (AutoChess.FieldSpecialType.None);

        public readonly ReactiveProperty<FieldRevealState> FieldRevealState =
            new ReactiveProperty<FieldRevealState> (AutoChess.FieldRevealState.Sealed);

        public readonly BoolReactiveProperty FieldHighlight = new BoolReactiveProperty (false);

        public PositionModel LandPosition;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private bool _isRevealed;

        #endregion


        public FieldModel (PositionModel landPosition)
        {
            LandPosition = landPosition;
        }


        #region Methods

        public void Reset ()
        {
            FieldExistType.Value = AutoChess.FieldExistType.Empty;
            FieldGroundType.Value = AutoChess.FieldGroundType.None;
            FieldSpecialType.Value = AutoChess.FieldSpecialType.None;
            FieldRevealState.Value = AutoChess.FieldRevealState.Sealed;
            FieldHighlight.Value = false;
        }


        public void ChangeState (FieldRevealState state)
        {
            if (FieldExistType.Value == AutoChess.FieldExistType.Empty)
                return;
            
            FieldRevealState.Value = state;
        }


        public void ChangeFieldGroundType (FieldGroundType fieldGroundType)
        {
            if (FieldExistType.Value == AutoChess.FieldExistType.Empty)
                return;
            
            FieldGroundType.Value = fieldGroundType;
        }


        public void ChangeFieldSpecialType (FieldSpecialType fieldSpecialType)
        {
            if (FieldExistType.Value == AutoChess.FieldExistType.Empty)
                return;
            
            FieldSpecialType.Value = fieldSpecialType;
        }


        public void ChangeHighlight (bool isHighlight)
        {
            if (FieldExistType.Value == AutoChess.FieldExistType.Empty)
                return;
            
            FieldHighlight.Value = isHighlight;
        }

        #endregion
    }
}