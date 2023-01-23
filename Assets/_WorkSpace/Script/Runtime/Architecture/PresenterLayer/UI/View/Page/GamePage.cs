using AutoChess;
using AutoChess.Presenter;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess.Presenter
{
    public class GamePage : PageViewBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject] private CharacterViewModel _characterViewModel;
        
#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnPush(object pushValue = null)
        {
            return base.OnPush(pushValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}