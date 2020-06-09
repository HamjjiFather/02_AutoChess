using System;
using HexaPuzzle;
using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework
{
    public class GamePageView : PageViewBase
    {
        #region Fields & Property

        public StatusView statusView;

        public GameObject[] bottomViewPageObj;
        
        public PuzzleView puzzleView;

        public Button puzzleViewButton;
        
        public Button specialPuzzleViewButton;

        public Button battleCharacterViewButton;
        

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            puzzleViewButton.onClick.AddListener (ClickPuzzle);
            specialPuzzleViewButton.onClick.AddListener (ClickSpecialPuzzle);
            battleCharacterViewButton.onClick.AddListener (ClickBattleCharacterPuzzle);
            SetSubviewPage (0);
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            statusView.InitializeStatusView ();
            puzzleView.InitializePuzzleView ();
            
            return base.OnPush (pushValue);
        }

        
        public void SetSubviewPage (int index)
        {
            bottomViewPageObj.Foreach (x => x.SetActive (false));
            bottomViewPageObj[index].SetActive (true);
        }

        #endregion


        #region EventMethods

        private void ClickPuzzle ()
        {
            SetSubviewPage (0);
        }
        
        
        private void ClickSpecialPuzzle ()
        {
            SetSubviewPage (1);
        }
        
        
        private void ClickBattleCharacterPuzzle ()
        {
            SetSubviewPage (2);
        }
        

        #endregion
    }
}