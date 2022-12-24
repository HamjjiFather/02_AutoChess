namespace AutoChess
{
    /// <summary>
    /// 장비 생성자.
    /// 장비 생성 순서
    /// 등급 -> 슬롯 수량 -> 개방된 슬롯 수량 -> 개방된 슬롯  개방된 슬롯의 능력치 -> 개방된 슬롯의 능력치 범위
    /// </summary>
    public static class EquipmentGenerator
    {
        #region Fields & Property

        #endregion


        #region Methods

        public static EquipmentBase GenerateEquipment()
        {
            return new EquipmentBase(TableDataManager.Instance.EquipmentDict[0]);
        }

        #endregion
    }
}