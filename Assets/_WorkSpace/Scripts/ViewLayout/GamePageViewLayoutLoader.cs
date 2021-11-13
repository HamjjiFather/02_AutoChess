using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
using KKSFramework.UI;

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

        private GamePage _gamePage;

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
            NavigationHelper.OpenPage (NavigationViewType.AdventurePage, NavigationTriggerState.First).Forget();
        }

        private void ClickFormationButton ()
        {
            NavigationHelper.OpenPopup (NavigationViewType.FormationPopup).Forget();
        }

        #endregion
    }
}