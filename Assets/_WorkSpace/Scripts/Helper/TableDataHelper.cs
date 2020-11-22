using System;
using System.Linq;

namespace MasterData
{
    public class TableDataHelper : Singleton<TableDataHelper>
    {
        public TableName GetDataTypeByItemIndex (int index)
        {
            if (Enum.GetValues (typeof (TableName)) is TableName[] dataTypes)
            {
                return dataTypes.Last (x => (int) x <= index);
            }

            return TableName.Character;
        }
        
        
        public Status GetStatus (StatusType statusType)
        {
            return GetBaseTableByEnum<Status> (TableName.Status, (int) statusType);
        }
        
        
        public CharacterLevel GetCharacterLevelByExp (float exp)
        {
            return CharacterLevel.Manager.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }

        
        public CharacterLevel GetCharacterLevelByLevel (int level)
        {
            return GetBaseTableByEnum<CharacterLevel> (TableName.CharacterLevel, (int) TableName.CharacterLevel + level);
        }


        public T GetBaseTableByEnum<T> (TableName tableName, int index) where T : BaseTable
        {
            switch (tableName)
            {
                case TableName.CharacterLevel:
                    return CharacterLevel.Manager.GetItemByIndex (index) as T;
                
                case TableName.Status:
                    return Status.Manager.GetItemByIndex (index) as T;
                
                default:
                    return Equipment.Manager.GetItemByIndex (index) as T;
            }
        }
    }
}