using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public partial class AdventureViewmodel : ViewModelBase
    {
        #region Fields & Property

        public int UniqueId;
        
        /// <summary>
        /// 탐험에서 획득한 장비.
        /// </summary>
        private readonly Dictionary<int, EquipmentModel> _inAdventureEquipmentModels = new Dictionary<int, EquipmentModel> ();

        public Dictionary<int, EquipmentModel> InAdventureEquipmentModels => _inAdventureEquipmentModels;
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public void Initialize_Item ()
        {
            UniqueId = 0;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}