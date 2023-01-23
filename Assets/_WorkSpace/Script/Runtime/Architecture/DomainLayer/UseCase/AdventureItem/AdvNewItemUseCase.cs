using System.Linq;
using AutoChess.Dto;
using AutoChess.Repository;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    public class AdvNewItemUseCase : IUseCaseBase
    {
        #region Fields & Property

        [Inject]
        private AdventureRepository _adventureRepository;

        [Inject]
        private AdventureInventoryRepository _adventureInventoryRepository;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        public void Execute(string itemIndex, int amount)
        {
            // var itemTableData = TableDataManager.Instance.ItemDict[itemIndex];

            // // 아이템이 중첩 가능함.
            // if (itemTableData.Stackable)
            // {
            //     var inventoryItems = _adventureInventoryRepository.Request();
            //     var alreadyExistInInventory = inventoryItems.Exists(x => x.ItemIndex.Equals(itemIndex));
            //
            //     // 이미 인벤토리에 동일한 아이템을 가지고 있음.
            //     if (alreadyExistInInventory)
            //     {
            //         var sameItemInInventory = inventoryItems.First(x => x.ItemIndex.Equals(itemIndex));
            //         _adventureInventoryRepository.Update(new AdventureInventoryDto(sameItemInInventory.UniqueIndex,
            //             itemIndex, sameItemInInventory.Amount + amount));
            //         return;
            //     }
            // }

            // 새로운 아이템 정보를 발급함.
            var latestIssuancedUniqueIndex = _adventureRepository.GetIssuanceUniqueIndex;
            latestIssuancedUniqueIndex += 1;
            _adventureRepository.Update(latestIssuancedUniqueIndex);
            _adventureInventoryRepository.Update(new AdventureInventoryDto(latestIssuancedUniqueIndex, itemIndex,
                amount));
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}