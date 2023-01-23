using System;
using System.Collections.Generic;
using System.Linq;
using AutoChess.Domain;
using AutoChess.UseCase;
using KKSFramework.Presenter;
using UniRx;
using Zenject;

namespace AutoChess.Presenter
{
    public class InventoryViewModel : IViewModel
    {
        #region Fields & Property


        
        #endregion

        #region Methods

        #region Override

        public void Initialize()
        {
            // _itemModels = _inventoryUseCase.Excute().ToList();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}