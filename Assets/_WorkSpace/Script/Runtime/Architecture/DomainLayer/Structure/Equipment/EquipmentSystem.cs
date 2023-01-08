using System.Collections.Generic;
using System.Linq;
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
        public static EquipmentProbabilityTable CommonEquipmentDropProbTable = new(new[]
        {
            15000, 45000, 25000, 15000
        });

        /// <summary>
        /// 장비 생성시 출현 가능한 등급들.
        /// </summary>
        public static EquipmentGradeType[] UsedEquipmentGradeTypes =
        {
            EquipmentGradeType.BadlyMade,
            EquipmentGradeType.Common,
            EquipmentGradeType.WellMade,
            EquipmentGradeType.MasterPiece
        };

        /// <summary>
        /// 장비 생성시 슬롯이 오픈될 확률. 
        /// </summary>
        public const int OpenSlotProbability = 20000;
        
        /// <summary>
        /// 장비의 최대 강화 레벨.
        /// </summary>
        public const int MaxEnhanceLevel = 10; 
    }


    /// <summary>
    /// 장비 생성자.
    /// 장비 생성 순서
    /// 등급 -> 슬롯 수량 -> 개방된 슬롯 수량 -> 개방된 슬롯 개방된 슬롯의 능력치 -> 개방된 슬롯의 능력치 범위
    /// </summary>
    public static class EquipmentGenerator
    {
        #region Fields & Property

        public static IUniqueIndexIssuancer UniqueIndexIssuancer;

        #endregion


        #region Methods

        /// <summary>
        /// 적 캐릭터의 장비 생성 시도.
        /// </summary>
        public static EquipmentBase[] GenerateEquipmentsForEnemy(EnemyGradeType enemyGradeType,
            int uniqueEquipmentIndexes = default)
        {
            var enemyGradeTable = TableDataManager.Instance.EnemyGradeDict.Values
                .First(x => x.EnemyGradeType.Equals(enemyGradeType));

            var equipments = enemyGradeTable.EquipmentProb
                .Where(ep => ProbabilityHelper.Chance(ep))
                .Select(_ => GenerateEquipment(EquipmentDefine.CommonEquipmentDropProbTable))
                .ToArray();

            return equipments;
        }


        public static EquipmentBase ChoiceEquipmentTable(EnemyGradeType enemyGradeType)
        {
            return GenerateEquipment(EquipmentDefine.CommonEquipmentDropProbTable);
        }


        public static EquipmentBase GenerateEquipment(EquipmentProbabilityTable probList)
        {
            var prob = ProbabilityHelper.RandomValue;
            var pickedGradeType = probList.Chance(prob);

            Debug.Log(pickedGradeType);
            var egTable =
                TableDataManager.Instance.EquipmentGradeDict.Values.First(x =>
                    x.EquipmentGradeType.Equals(pickedGradeType));

            // 출현 등급에 해당하는 장비를 무작위로 하나 뽑음.
            var eTable = TableDataManager.Instance.EquipmentDict.Values
                .Choice(x => x.EquipmentGradeType.Equals(pickedGradeType));

            // 장비 생성.
            // 장비 생성시 부여된 슬롯 수량만큼 빈 슬롯을 만들어놓는다.
            var equipment = new EquipmentBase(UniqueIndexIssuancer.GetUniqueIndex(), eTable, egTable.SlotAmount);

            // 기본으로 부여될 능력치를 슬롯에 부여함.
            eTable.BaseEquipmentStatusIndexes.Foreach(ei =>
            {
                if (!equipment.RemainSlot)
                    return;

                var eaTable = TableDataManager.Instance.EquipmentAbilityDict[ei];
                equipment.AttachAbilityInSlot(eaTable);
            });

            // 빈 슬롯이 없을 때 까지 슬롯을 오픈을 시도한다.
            // 슬롯 오픈에 실패할 경우 이후 슬롯들은 비워진 채로 나간다.
            while (equipment.RemainSlot)
            {
                var isOpen = ProbabilityHelper.Chance(EquipmentDefine.OpenSlotProbability);

                if (isOpen)
                {
                    OpenEquipmentSlot(equipment);
                }
                else
                {
                    break;
                }
            }

            return equipment;
        }


        /// <summary>
        /// 장비 슬롯을 오픈함.
        /// </summary>
        public static void OpenEquipmentSlot(EquipmentBase equipment)
        {
            var ei = equipment.EquipmentTableData.AvailEquipmentTypeIndex.Choice();
            var eaTable = TableDataManager.Instance.EquipmentAbilityDict[ei];
            equipment.AttachAbilityInSlot(eaTable);
        }

        #endregion
    }
}