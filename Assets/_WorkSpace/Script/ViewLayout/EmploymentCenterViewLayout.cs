using KKSFramework.InGame;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class EmploymentCenterViewLayout : ViewLayoutBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        private GamePageView _gamePageView;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void Initialize ()
        {
            _gamePageView = ProjectContext.Instance.Container.Resolve<GamePageView> ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}