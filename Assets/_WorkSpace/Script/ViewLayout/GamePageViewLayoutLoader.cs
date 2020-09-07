using BaseFrame.Navigation;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
using KKSFramework.UI;
using UnityEngine;

namespace AutoChess
{
    public class GamePageViewLayoutLoader : ViewLayoutLoaderWithButton
    {
        #region Fields & Property
        
#pragma warning disable CS0649
        
        [Resolver]
        private ButtonExtension _formationButton;
        
        [Resolver]
        private ButtonExtension _fieldViewButton;

#pragma warning restore CS0649

        private GamePageView _gamePageView;

        #endregion


        #region UnityMethods

        public override void Initialize ()
        {
            base.Initialize ();
            _fieldViewButton.AddListener (ClickFieldButton);
            _formationButton.AddListener (ClickFormationButton);
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods
        
        private void ClickFieldButton ()
        {
            TreeNavigationHelper.PushRootPage (Page.AdventurePage, transitionType:TransitionType.Fade);
        }
        
        private void ClickFormationButton ()
        {
            TreeNavigationHelper.PushPopup (Popup.FormationPopup);
        }

        #endregion
    }
}