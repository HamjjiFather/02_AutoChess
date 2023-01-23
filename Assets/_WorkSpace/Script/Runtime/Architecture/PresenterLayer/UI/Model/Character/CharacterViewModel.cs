using System.Collections.Generic;
using AutoChess.UseCase;
using JetBrains.Annotations;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess.Presenter
{
    [UsedImplicitly]
    public class CharacterVieModel : IViewModel
    {
        #region Fields & Property

        [Inject]
        private CharacterListRequestUseCase _characterListRequestUseCase;

        private List<CharacterModelBase> _characterModels;

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
            _characterModels = _characterListRequestUseCase.Execute();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion


    }
}