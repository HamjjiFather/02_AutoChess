using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using AutoChess.Domain;
using AutoChess.Dto;
using JetBrains.Annotations;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess.Repository
{
    [UsedImplicitly]
    public class AdventureInventoryRepository : IRepository
    {
        #region Fields & Property

        [Inject]
        private AdventureInventoryBundle _adventureInventoryBundle;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Update(AdventureInventoryDto adventureInventoryDto)
        {
            _adventureInventoryBundle.Bind(adventureInventoryDto.UniqueIndex, adventureInventoryDto.ItemIndex,
                adventureInventoryDto.Amount);
        }


        public List<AdventureInventoryDto> Request()
        {
            var bundleSets = _adventureInventoryBundle.bundleSets
                .Select(x =>
                {
                    var toStr = int.Parse(x.uniqueIndexString);
                    return new AdventureInventoryDto(toStr, x.itemIndex, x.amount);
                }).ToList();
            return bundleSets;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}