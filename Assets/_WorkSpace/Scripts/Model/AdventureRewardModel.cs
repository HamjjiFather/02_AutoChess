using System.Collections.Generic;
using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
    public class AdventureRewardModel : ModelBase
    {
        public int UniqueIndex;

        #region Fields & Property

        public IntReactiveProperty RewardCount = new IntReactiveProperty();

        public IntReactiveProperty GoldCount = new IntReactiveProperty();
        
        /// <summary>
        /// 탐험에서 획득한 장비.
        /// </summary>
        public readonly Dictionary<int, EquipmentModel> _inAdventureEquipmentModels =
            new Dictionary<int, EquipmentModel> ();

        public Dictionary<int, EquipmentModel> InAdventureEquipmentModels => _inAdventureEquipmentModels;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public void AddRewardCount (int reward)
        {
            RewardCount.Value += reward;
        }

        public void AddGoldCount (int gold)
        {
            GoldCount.Value += gold;
        }

        #endregion
    }
}