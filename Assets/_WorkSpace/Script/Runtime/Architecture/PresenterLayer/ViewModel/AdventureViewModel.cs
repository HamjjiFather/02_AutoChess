using System.Collections.Generic;
using AutoChess.Domain;
using AutoChess.UseCase;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess.Presenter
{
    public class AdventureViewModel : IViewModel
    {
        #region Fields & Property
        
        [Inject]
        private AdvItemListRequestUseCase _advItemListRequestUseCase;

        [Inject]
        private AdvNewItemUseCase _advNewItemUseCase;

        private List<ItemModel> _inventoryItemModels;

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
            _inventoryItemModels = _advItemListRequestUseCase.Execute();
            
            _advNewItemUseCase.Execute(0, 1);
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}