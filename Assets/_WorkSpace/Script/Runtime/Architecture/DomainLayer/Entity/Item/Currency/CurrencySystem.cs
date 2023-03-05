using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public static class CurrencyDefine
    {
        
    }
    
    public static class CurrencyHelper
    {
        #region Fields & Property

        private static Dictionary<CurrencyType, Sprite> _currencySpriteMap = new();

        public static Func<CurrencyType, int, bool> CheckEnoughCurrencyFunc;

        private const string PathFormat = "_Image/Currency/{0}";
        

        #endregion


        #region This
        

        public static Sprite GetCurrencyIcon(CurrencyType currencyType)
        {
            if (_currencySpriteMap.ContainsKey(currencyType))
                return _currencySpriteMap[currencyType];


            var iconName = TableDataManager.Instance.CurrencyDict[(int) currencyType].IconName;
            var path = string.Format(PathFormat, iconName);
            return Resources.Load<Sprite>(path);
        }

        // public static CurrencyPresentVo ToPresentVo(CurrencyType currencyType, IntReactiveProperty intReactive)
        // {
        //     return new CurrencyPresentVo()
        // }


        public static CurrencyPresentVo GetCurrencyPresentVo(CurrencyType currencyType, int value)
        {
            var sprite = GetCurrencyIcon(currencyType);
            var toString = ToCurrencyString(value);
            var enough = CheckEnoughCurrencyFunc(currencyType, value);
            return new CurrencyPresentVo(sprite, toString, enough);
        }


        public static string ToCurrencyString(int value) => value.ToString("N0");

        #endregion
    }

    public static class CurrencyGenerator
    {
        /// <summary>
        /// 몬스터로 부터 아이템 생성.
        /// </summary>
        public static void GenerateFromMonster()
        {
            
        }
    }
}