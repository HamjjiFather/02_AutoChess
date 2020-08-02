using System;
using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
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
        InsightFull,
        Event,
        Reward,
        FakeReward,
        RecoverSmall,
        RecoverMedium,
        RecoverLarge,
        Knowledge,
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

        public ReactiveProperty<FieldGroundType> FieldGroundType =
            new ReactiveProperty<FieldGroundType> (AutoChess.FieldGroundType.None);

        public ReactiveProperty<FieldSpecialType> FieldSpecialType =
            new ReactiveProperty<FieldSpecialType> (AutoChess.FieldSpecialType.None);

        public ReactiveProperty<FieldRevealState> FieldRevealState =
            new ReactiveProperty<FieldRevealState> (AutoChess.FieldRevealState.Sealed);

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

        public void ChangeState (FieldRevealState state)
        {
            FieldRevealState.Value = state;
        }


        public void ChangeFieldGroundType (FieldGroundType fieldGroundType)
        {
            FieldGroundType.Value = fieldGroundType;
        }


        public void ChangeFieldSpecialType (FieldSpecialType fieldSpecialType)
        {
            FieldSpecialType.Value = fieldSpecialType;
        }

        #endregion
    }
}