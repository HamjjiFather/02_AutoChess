using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine.UI;

namespace AutoChess
{
    public class FieldViewLayoutRewardArea : AreaBase<AdventureRewardModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Text _rewardCountText;

        [Resolver]
        private Text _goldCountText;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (AdventureRewardModel areaData)
        {
            areaData.GoldCount.TakeUntilDisable (this).SubscribeToText (_goldCountText);
            areaData.RewardCount.TakeUntilDisable (this).SubscribeToText (_rewardCountText);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}