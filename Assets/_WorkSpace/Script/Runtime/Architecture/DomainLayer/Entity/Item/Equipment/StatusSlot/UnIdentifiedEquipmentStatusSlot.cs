namespace AutoChess
{
    public class UnIdentifiedEquipmentStatusSlot : IEquipmentStatusSlot
    {
        #region Fields & Property

        public int SlotIndex => Constant.InvalidIndex;

        public EquipmentStatusSlotState EquipmentStatusSlotState => EquipmentStatusSlotState.UnIdentified;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}