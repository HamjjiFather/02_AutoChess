using AutoChess.Domain;

namespace AutoChess
{
    public class ItemSlotBase
    {
        #region Fields & Property

        /// <summary>
        /// 비어있는지 여부?
        /// </summary>
        public bool IsEmpty => StoredItemUniqueIndex == Constant.InvalidIndex;

        /// <summary>
        /// 보관된 아이템.
        /// </summary>
        public int StoredItemUniqueIndex;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        /// <summary>
        /// 이 슬롯에 해당 유니크 인덱스를 가진 아이템을 보관함.
        /// </summary>
        public void Store(int uniqueIndex)
        {
            StoredItemUniqueIndex = uniqueIndex;
        }


        /// <summary>
        /// 아이템을 빼냄.
        /// </summary>
        public int TakeOut()
        {
            var returnValue = StoredItemUniqueIndex;
            StoredItemUniqueIndex = Constant.InvalidIndex;
            return returnValue;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}