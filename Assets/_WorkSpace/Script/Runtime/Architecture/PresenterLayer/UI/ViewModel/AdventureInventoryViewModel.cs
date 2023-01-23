using AutoChess.UseCase;
using JetBrains.Annotations;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess
{
    [UsedImplicitly]
    public class AdventureInventoryViewModel : IViewModel
    {
        #region Fields & Property

        [Inject]
        private AdvItemListRequestUseCase _advItemListRequestUseCase;
        
        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
            
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}