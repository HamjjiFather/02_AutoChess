using HexaPuzzle;
using KKSFramework.Navigation;
using UniRx.Async;

namespace KKSFramework
{
    public class GamePageView : PageViewBase
    {
        #region Fields & Property

        public StatusView statusView;
        
        public SummonView summonView;
        
        public PuzzleView puzzleView;
        

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            statusView.InitializeStatusView ();
            puzzleView.InitializePuzzleView ();
            
            return base.OnPush (pushValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}