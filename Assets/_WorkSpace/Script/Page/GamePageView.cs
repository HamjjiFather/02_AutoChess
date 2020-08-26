using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using Zenject;

namespace KKSFramework.InGame
{
    public class GamePageView : PageViewBase
    {
        #region Fields & Property

        public ViewLayoutLoader viewLayoutLoader;
        
        public StatusView statusView;
        
#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            viewLayoutLoader.Initialize ();
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            statusView.InitializeStatusView ();
            BackToMain ();
            
            return base.OnPush (pushValue);
        }


        public void BackToMain ()
        {
            viewLayoutLoader.SetSubView (0);
        }


        #endregion


        #region EventMethods
        

        #endregion
    }
}