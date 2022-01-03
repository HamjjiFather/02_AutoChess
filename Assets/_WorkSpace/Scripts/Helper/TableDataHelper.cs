using System;
using System.Linq;
using KKSFramework.TableData;

namespace MasterData
{
    public class TableDataHelper : Singleton<TableDataHelper>
    {
        public DataType GetDataTypeByItemIndex (int index)
        {
            if (Enum.GetValues (typeof (DataType)) is DataType[] dataTypes)
            {
                return dataTypes.Last (x => (int)x <= index);
            }

            return DataType.Character;
        }


        public Ability GetAbility (AbilityType abilityType)
        {
            return GetBaseTableByEnum<Ability> (DataType.Ability, (int)abilityType);
        }


        public EquipmentGradeProb GetEquipmentGradeProb (int level)
        {
            return TableDataManager.Instance.EquipmentGradeProbDict[level];
        }


        public CharacterLevel GetCharacterLevelByExp (float exp)
        {
            return TableDataManager.Instance.CharacterLevelDict.Values.FirstOrDefault (x => x.AccReqExp > exp);
        }


        public CharacterLevel GetCharacterLevelByLevel (int level)
        {
            return GetBaseTableByEnum<CharacterLevel> (DataType.CharacterLevel,
                (int)DataType.CharacterLevel + level);
        }


        public T GetBaseTableByEnum<T> (DataType dataType, int index) where T : TableDataBase
        {
            return TableDataManager.Instance.TotalDataDict[(int)dataType + index] as T;
        }
    }
}