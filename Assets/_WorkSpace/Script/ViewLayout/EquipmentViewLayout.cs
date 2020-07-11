using UniRx.Async;

namespace AutoChess
{
    public class EquipmentViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public EquipmentInfoArea equipmentInfoArea;

        public EquipmentListArea equipmentListArea;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods
        
        public override UniTask ActiveLayout ()
        {
            equipmentListArea.SetArea (ClickEquipmentElement);
            return base.ActiveLayout ();
        }

        #endregion


        #region EventMethods
        
        private void ClickEquipmentElement (EquipmentModel equipmentModel)
        {
            equipmentInfoArea.SetArea (equipmentModel);
        }

        #endregion
    }
}