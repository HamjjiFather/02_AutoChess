using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public partial class AdventureViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private int _exp;

        private AdventureRewardModel _adventureRewardModel;

        public AdventureRewardModel AdventureRewardModel => _adventureRewardModel;

        private readonly List<ItemModel> _rewardItems = new List<ItemModel> ();

        #endregion


        #region Methods

        public void StartAdventure_Rewards ()
        {
            _adventureRewardModel = new AdventureRewardModel ();
        }


        public void AddExp (int value)
        {
            _exp += value;
        }


        /// <summary>
        /// 보물 상자 보상 체크.
        /// </summary>
        public void RewardCheck ()
        {
            var random = Random.Range (0, 1);
            switch (random)
            {
                case 0:
                    AddDropItem ((int) DataType.Equipment);
                    break;
                
                case 1:
                    AddDropGold (Random.Range (100, 200));
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// 드랍 아이템 추가.
        /// </summary>
        public void AddDropItem (int itemIndex, int amount = 1)
        {
            _adventureRewardModel.AddRewardCount (amount);
            _rewardItems.Add (new ItemModel
            {
                ItemIndex = itemIndex,
                ItemAmount = amount
            });
        }


        /// <summary>
        /// 골드 추가.
        /// </summary>
        public void AddDropGold (int gold)
        {
            _adventureRewardModel.AddGoldCount (gold);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}