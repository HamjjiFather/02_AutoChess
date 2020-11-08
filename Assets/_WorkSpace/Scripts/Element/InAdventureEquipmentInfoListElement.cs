using UniRx;
using UnityEngine;

namespace AutoChess
{
    public class InAdventureEquipmentInfoListElementModel : EquipmentInfoListElementModel
    {
        public BoolReactiveProperty Selected = new BoolReactiveProperty(false);
        
        public BoolReactiveProperty Succession = new BoolReactiveProperty(false);
    }
    
    public class InAdventureEquipmentInfoListElement : EquipmentInfoListElement
    {
        #region Fields & Property

        public GameObject selectedObj;

        public GameObject successionObj;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private InAdventureEquipmentInfoListElementModel InAdventureEquipment =>
            ElementData as InAdventureEquipmentInfoListElementModel;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetElement (EquipmentInfoListElementModel elementData)
        {
            ElementData = elementData;
            SetBaseInfo ();

            InAdventureEquipment.Selected.Subscribe (selectedObj.SetActive);
            InAdventureEquipment.Succession.Subscribe (successionObj.SetActive);
            
            elementButton.onClick.RemoveAllListeners ();
            elementButton.onClick.AddListener (() =>
            {
                InAdventureEquipment.Selected.Value = !InAdventureEquipment.Selected.Value;
                elementData.ElementClick.Invoke (elementData.EquipmentModel);
            });
        }


        #endregion


        #region EventMethods

        #endregion
    }
}