using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class WarehouseSlotElement : ElementView<WarehouseSlotCellModel>
    {
        #region Fields & Property
        
        public override WarehouseSlotCellModel ElementModel { get; set; }

        public GameObject[] stateObjs;

        public Image iconImage;
        
        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            stateObjs.Foreach(so => so.SetActive(false));
        }

        public override void SetElement(WarehouseSlotCellModel elementData)
        {
            ElementModel = elementData;
            stateObjs[(int)ElementModel.SlotState].SetActive(true);

            if (ElementModel.SlotState == WarehouseSlotState.Stored)
            {
                // iconImage.sprite = 
            }
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion


    }
}