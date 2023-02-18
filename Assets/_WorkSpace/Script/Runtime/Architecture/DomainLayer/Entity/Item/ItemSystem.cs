using System.Linq;
using AutoChess.Domain;
using UnityEngine;

namespace AutoChess
{
    public static class ItemDefine
    {
        /// <summary>
        /// 기본 인벤토리 슬롯 수량.
        /// </summary>
        public const int BaseInventorySpace = 25;

        /// <summary>
        /// 재화의 총량.
        /// </summary>
        public const int LimitCurrencyAmount = 999_999_999;
    }

    public static class ItemHelper
    {
        #region Fields & Property

        private const string EquipmentPathFormat = "_Image/Equipment/{0}";

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public static Sprite GetItemSprite(string path) => Resources.Load<Sprite>(path);


        public static Sprite GetEquipmentSprite(string spriteName)
        {
            var path = string.Format(EquipmentPathFormat, spriteName);
            return GetItemSprite(path);
        }


        // public static Sprite GetItemSprite(string globalIndex)
        // {
        //     if (globalIndex.Contains("Equipment_Item"))
        //     {
        //         var table = TableDataManager.Instance.EquipmentDict.Values
        //             .First(x => x.GlobalIndex.Equals(globalIndex))
        //     }
        // }

        #endregion


        #region Event

        #endregion

        #endregion
    }

    public static class ItemGenerator
    {
        /// <summary>
        /// 몬스터로 부터 아이템 생성.
        /// </summary>
        public static void GenerateFromMonster()
        {
        }
    }
}