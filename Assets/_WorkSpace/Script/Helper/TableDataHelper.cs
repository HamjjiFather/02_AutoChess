namespace AutoChess.Helper
{
    public class TableDataHelper
    {
        public static Status GetStatus (StatusType statusType)
        {
            var arrayIndex = (int) DataType.Status + (int) statusType;
            return TableDataManager.Instance.StatusDict[arrayIndex];
        }
    }
}