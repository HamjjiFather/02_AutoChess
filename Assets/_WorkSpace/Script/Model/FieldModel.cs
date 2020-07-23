using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
    public enum FieldType
    {
        None,
        Battle,
        BossBattle,
        Event,
        Reward,
        CampSite,
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

        public FieldType FieldType;

        public ReactiveProperty<FieldRevealState> RevealState =
            new ReactiveProperty<FieldRevealState> (FieldRevealState.Sealed);
        
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
            RevealState.Value = state;
        }

        #endregion
    }
}