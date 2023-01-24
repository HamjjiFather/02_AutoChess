using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using JetBrains.Annotations;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess
{
    public struct CurrencyDao : IDAO
    {
        public CurrencyDao(int index, int amount, Currency currencyMd)
        {
            Index = index;
            Amount = amount;
            CurrencyMd = currencyMd;
        }

        public int Index;

        public int Amount;

        public Currency CurrencyMd;
    }

    [UsedImplicitly]
    public class CurrencyRepository : IRepository
    {
        #region Fields & Property

        [Inject]
        private CurrencyBundle _currencyBundle;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This
        
        public List<CurrencyDao> ReadAll()
        {
            return TableDataManager.Instance.CurrencyDict.Values.Select(c =>
            {
                var id = c.Id;
                var set = _currencyBundle.Load(id.ToString());
                return new CurrencyDao(c.Id, set.amount, c);
            }).ToList();
        }

        public void Update(int index, int amount)
        {
            var load = _currencyBundle.Load(index.ToString());
            load.amount = amount;
            _currencyBundle.Save(load);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}