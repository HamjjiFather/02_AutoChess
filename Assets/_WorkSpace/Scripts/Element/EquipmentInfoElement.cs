using KKSFramework.DataBind;
using KKSFramework.Navigation;
using MasterData;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class EquipmentInfoElement : ElementBase<EquipmentModel>, IResolveTarget
    {
        #region Fields & Property

        public override EquipmentModel ElementData { get; set; }

#pragma warning disable CS0649

        [Resolver]
        private GameObject _equipmentObj;

        [Resolver]
        private GameObject _emptyObj;

        [Resolver]
        private Image _equipmentImage;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        public override void SetElement (EquipmentModel elementData)
        {
            var isEmpty = elementData.UniqueEquipmentId.Equals (Constant.InvalidIndex);
            _emptyObj.SetActive (isEmpty);
            _equipmentObj.SetActive (!isEmpty);

            if (isEmpty) return;
            _equipmentImage.sprite = elementData.IconImageResources;
        }

        #endregion
    }
}