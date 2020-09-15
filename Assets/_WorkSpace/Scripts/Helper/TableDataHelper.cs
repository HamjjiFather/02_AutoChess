using System;
using System.Linq;
using MasterData;

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
            var arrayIndex = ((int) TableName.Status + 1) * 1000 + (int) statusType;
            return Status.Manager.GetItemByIndex(arrayIndex);
        }
        
        
        public CharacterLevel GetCharacterLevelByExp (float exp)
        {
            return CharacterLevel.Manager.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }

        
        public CharacterLevel GetCharacterLevelByLevel (int level)
        {
            return CharacterLevel.Manager.GetItemByIndex((int) TableName.CharacterLevel * 1000 + level);
        }
    }
}