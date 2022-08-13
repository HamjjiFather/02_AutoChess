using System.Collections.Generic;
using System.Linq;
using AutoChess.Domain;
using AutoChess.Repository;
using JetBrains.Annotations;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    [UsedImplicitly]
    public class AdvItemListRequestUseCase : IUseCaseBase
    {
        #region Fields & Property

        [Inject]
        private AdventureInventoryRepository _adventureInventoryRepository;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }


        public List<ItemModel> Execute()
        {
            var request = _adventureInventoryRepository.Request();
            return request.Select(x => new ItemModel(x.UniqueIndex, x.ItemIndex, x.Amount)).ToList();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}