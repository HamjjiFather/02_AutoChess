using System;
using System.Linq;
using KKSFramework.TableData;

namespace AutoChess
{
    public class TableDataHelper : Singleton<TableDataHelper>
    {
        public T GetTableData<T> (DataType dataType, int index) where T : TableDataBase
        {
            return TableDataManager.Instance.TotalDataDict[(int) dataType + index] as T;
        }


        public DataType GetDataTypeByItemIndex (int index)
        {
            if (Enum.GetValues (typeof (DataType)) is DataType[] dataTypes)
            {
                return dataTypes.Last (x => (int) x <= index);
            }

            return DataType.None;
        }
        
        
        public Status GetStatus (StatusType statusType)
        {
            var arrayIndex = (int) DataType.Status + (int) statusType;
            return TableDataManager.Instance.StatusDict[arrayIndex];
        }
        
        
        public CharacterLevel GetCharacterLevelByExp (float exp)
        {
            return TableDataManager.Instance.CharacterLevelDict.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }

        
        public CharacterLevel GetCharacterLevelByLevel (int level)
        {
            return TableDataManager.Instance.CharacterLevelDict[(int) DataType.CharacterLevel + level];
        }
    }
}