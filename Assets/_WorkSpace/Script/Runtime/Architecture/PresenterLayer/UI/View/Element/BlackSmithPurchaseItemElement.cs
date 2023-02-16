using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    public class BlackSmithPurchaseItemElement : ElementView<BlackSmithPurcaseSlotCellModel>
    {
        #region Fields & Property

        public GameObject[] stateObjs;

        private Dictionary<BlackSmithPurchaseSlotState, GameObject> _stateObjMap;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            stateObjs.Foreach(so => so.SetActive(false));
        }

        public override BlackSmithPurcaseSlotCellModel ElementModel { get; set; }

        public override void SetElement(BlackSmithPurcaseSlotCellModel elementData)
        {
            ElementModel = elementData;
            _stateObjMap = (typeof(BlackSmithPurchaseSlotState).GetEnumValues() as BlackSmithPurchaseSlotState[])!
                .ToDictionary(x => x, x => stateObjs[(int) x]);

            var state = elementData?.SlotState ?? BlackSmithPurchaseSlotState.Locked;
            _stateObjMap[state].gameObject.SetActive(true);
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}