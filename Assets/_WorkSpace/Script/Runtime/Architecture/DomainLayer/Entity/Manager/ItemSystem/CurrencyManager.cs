using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    /// <summary>
    /// 재화 아이템 관리 클래스.
    /// </summary>
    public class CurrencyManager : ManagerBase
    {
        #region Fields & Property

        [Inject]
        private CurrencyRepository _currencyRepository;
        
        /// <summary>
        /// 관리되는 재화.
        /// </summary>
        private Dictionary<CurrencyType, int> _currencyDict = new();
        
        #endregion


        #region Methods

        #region Override

        public override void Initialize()
        {
            base.Initialize();
            var dao = _currencyRepository.ReadAll();
            _currencyDict = dao.ToDictionary(cd => cd.CurrencyMd.CurrencyType, cd => cd.Amount);
        }

        #endregion


        #region This

        /// <summary>
        /// 재화 충분 여부.
        /// </summary>
        public bool EnoughCurrency(CurrencyType currencyType, int amount) => amount >= _currencyDict[currencyType];


        /// <summary>
        /// 재화 변동.
        /// </summary>
        public void VariationCurrency(CurrencyType currencyType, int amount)
        {
            if (amount >= 0)
            {
                IncreaseCurrency(currencyType, amount);
                return;
            }

            DecreaseCurrency(currencyType, amount);
        }


        /// <summary>
        /// 재화 증가.
        /// </summary>
        private void IncreaseCurrency(CurrencyType currencyType, int amount)
        {
            var cur = _currencyDict[currencyType] + amount;
            _currencyDict[currencyType] = Mathf.Clamp(cur, 0, ItemDefine.LimitCurrencyAmount);
        }


        /// <summary>
        /// 재화 차감.
        /// </summary>
        private void DecreaseCurrency(CurrencyType currencyType, int amount)
        {
            var cur = _currencyDict[currencyType] + amount;
            _currencyDict[currencyType] = Mathf.Clamp(cur, 0, ItemDefine.LimitCurrencyAmount);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}