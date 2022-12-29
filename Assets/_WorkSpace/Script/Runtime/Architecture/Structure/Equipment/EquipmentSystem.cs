using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    public static class EquipmentDefine
    {
        /// <summary>
        /// 캐릭터가 가질 수 있는 장비 수량 한도.
        /// </summary>
        public const int EquipmentAmountOnCharacter = 3;

        /// <summary>
        /// 일반 등급의 장비가 생성될 확률.
        /// </summary>
        public static Dictionary<EquipmentGradeType, int> CommonEquipmentDropProb = new()
        {
            {EquipmentGradeType.BadlyMade, 15000},
            {EquipmentGradeType.Common, 45000},
            {EquipmentGradeType.WellMade, 25000},
            {EquipmentGradeType.MasterPiece, 15000}
        };


        /// <summary>
        /// 장비 생성시 슬롯이 오픈될 확률. 
        /// </summary>
        public static int OpenSlotProbability = 20000;
    }


    /// <summary>
    /// 장비 생성자.
    /// 장비 생성 순서
    /// 등급 -> 슬롯 수량 -> 개방된 슬롯 수량 -> 개방된 슬롯 개방된 슬롯의 능력치 -> 개방된 슬롯의 능력치 범위
    /// </summary>
    public static class EquipmentGenerator
    {
        #region Fields & Property

        #endregion


        #region Methods

        /// <summary>
        /// 적 캐릭터의 장비 생성 시도.
        /// </summary>
        /// <param name="enemyGradeType"></param>
        /// <returns></returns>
        public static EquipmentBase[] GenerateEquipmentsForEnemy(EnemyGradeType enemyGradeType,
            int uniqueEquipmentIndexes = default)
        {
            var enemyGradeTable = TableDataManager.Instance.EnemyGradeDict.Values
                .First(x => x.EnemyGradeType.Equals(enemyGradeType));

            var equipments = enemyGradeTable.EquipmentProb
                .Where(ep => ProbabilityHelper.Chance(ep))
                .Select(_ => GenerateEquipment())
                .ToArray();

            return equipments;
        }


        public static EquipmentBase ChoiceEquipmentTable(EnemyGradeType enemyGradeType)
        {
            return GenerateEquipment();
        }


        public static EquipmentBase GenerateEquipment()
        {
            var prob = ProbabilityHelper.RandomValue;
            var pickedGradeTypeKvp = EquipmentDefine.CommonEquipmentDropProb
                .First(dp => ProbabilityHelper.Chance(prob, dp.Value));

            var egTable =
                TableDataManager.Instance.EquipmentGradeDict.Values.First(x =>
                    x.EquipmentGradeType.Equals(pickedGradeTypeKvp.Key));

            // 출현 등급에 해당하는 장비를 무작위로 하나 뽑음.
            var eTable = TableDataManager.Instance.EquipmentDict.Values
                .Choice(x => x.EquipmentGradeType.Equals(pickedGradeTypeKvp.Key));

            // 장비 생성.
            // 장비 생성시 부여된 슬롯 수량만큼 빈 슬롯을 만들어놓는다.
            var equipment = new EquipmentBase(eTable, egTable.SlotAmount);

            // 기본으로 부여될 능력치를 슬롯에 부여함.
            eTable.BaseEquipmentStatusIndexes.Foreach(ei =>
            {
                if (!equipment.RemainSlot)
                    return;
                
                var eaTable = TableDataManager.Instance.EquipmentAbilityDict[ei];
                var value = Random.Range(eaTable.Min, eaTable.Max).FloatToInt();
                equipment.AttachAbilityInSlot(eaTable.AbilityType, value);
            });

            // 빈 슬롯이 없을 때 까지 슬롯을 오픈을 시도한다.
            // 슬롯 오픈에 실패할 경우 이후 슬롯들은 비워진 채로 나간다.
            while (equipment.RemainSlot)
            {
                var isOpen = ProbabilityHelper.Chance(EquipmentDefine.OpenSlotProbability);

                if (isOpen)
                {
                    var ei = eTable.AvailEquipmentTypeIndex.Choice();
                    var eaTable = TableDataManager.Instance.EquipmentAbilityDict[ei];
                    var value = Random.Range(eaTable.Min, eaTable.Max).FloatToInt();
                    equipment.AttachAbilityInSlot(eaTable.AbilityType, value);
                }
                else
                {
                    break;
                }
            }

            return equipment;
        }


        // public void Set

        #endregion
    }
}