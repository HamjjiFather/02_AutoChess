using AutoChess;
using BaseFrame;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace KKSFramework.InGame
{
    public class GamePage : PageController, IResolveTarget
    {
        #region Fields & Property
        
#pragma warning disable CS0649
        
        [Resolver]
        private StatusView _statusView;

        [Resolver]
        private ViewLayoutLoaderWithButton _viewLayoutLoader;
        
        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649
        

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            ProjectContext.Instance.Container.BindInstance (this);
            _viewLayoutLoader.Initialize ();
        }

        #endregion


        #region Methods

        protected override void OnPush (Parameters parameters)
        {
            _statusView.InitializeStatusView (BackToMain);
            _viewLayoutLoader.SetChangeAction (ChangeViewLayoutLoader);
            BackToMain ();

            void ChangeViewLayoutLoader (int nowLayout)
            {
                _statusView.ConvertButton (nowLayout < 0);
            }
        }
        

        public void BackToMain ()
        {
            _viewLayoutLoader.CloseViewLayout ();
        }

        
        #endregion


        #region EventMethods
        

        #endregion
    }
}