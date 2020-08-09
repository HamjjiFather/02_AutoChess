using System.Collections.Generic;

namespace AutoChess
{
    public partial class AdventureViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private readonly List<ItemModel> _rewardItems = new List<ItemModel> ();

        #endregion


        #region Methods

        public void AddDropItem (int itemIndex, int amount = 1)
        {
            _rewardItems.Add (new ItemModel
            {
                ItemIndex = itemIndex,
                ItemAmount = amount
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}