using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
    public class AdventureRewardModel : ModelBase
    {
        #region Fields & Property

        public IntReactiveProperty RewardCount = new IntReactiveProperty();

        public IntReactiveProperty GoldCount = new IntReactiveProperty();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public void AddRewardCount (int reward)
        {
            RewardCount.Value += reward;
        }

        #endregion
    }
}