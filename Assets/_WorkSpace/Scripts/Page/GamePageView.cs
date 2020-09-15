using BaseFrame;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class GamePageView : PageController, IResolveTarget
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

        private void Awake ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            _viewLayoutLoader.Initialize ();
        }

        #endregion


        #region Methods

        protected override void OnPush (Parameters parameters)
        {
            _statusView.InitializeStatusView ();
            BackToMain ();
        }



        public void ChangeViewLayout (int index)
        {
            _viewLayoutLoader.SetSubView (index);
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