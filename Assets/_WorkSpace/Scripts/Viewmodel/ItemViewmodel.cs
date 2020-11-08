using System.Collections.Generic;
using KKSFramework.DesignPattern;
using MasterData;

namespace AutoChess
{
    public class ItemViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 게임 재화.
        /// </summary>
        private readonly Dictionary<CurrencyType, CurrencyModel> _currencyModels =
            new Dictionary<CurrencyType, CurrencyModel> ();

        public Dictionary<CurrencyType, CurrencyModel> CurrencyModels => _currencyModels;

        #endregion


        public override void Initialize ()
        {
            _currencyModels.Add (CurrencyType.Gold, new CurrencyModel (CurrencyType.Gold, 1000));
            _currencyModels.Add (CurrencyType.SoulStone, new CurrencyModel (CurrencyType.SoulStone, 100));
            _currencyModels.Add (CurrencyType.StoneOfInheritance,
                new CurrencyModel (CurrencyType.StoneOfInheritance, 5));
        }


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}