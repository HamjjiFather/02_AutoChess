using System.Drawing.Printing;
using System.Linq;
using AutoChess;
using Cysharp.Threading.Tasks;
using KKSFramework;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class BlackSmithTest
    {
        [Test]
        public void 대장간_레벨업()
        {
            TableDataManager.Instance.LoadTableDatas().Forget();

            var blackSmith = new BlackSmithBuildingModel();

            for (var i = 0; i < blackSmith.MaxLevel; i++)
            {
                blackSmith.Level = i;
                Debug.Log($"레벨: {blackSmith.Level}, 생산 가능한 장비 수량 : {blackSmith.GetProductableEquipmentAmount}");
            }
        }


        [Test]
        public void 대장간_장비_생산()
        {
            var uidIssuancer = new TestUIdIssuancer();

            TableDataManager.Instance.LoadTableDatas().Forget();
            EquipmentGenerator.UniqueIndexIssuancer = uidIssuancer;

            var blackSmith = new BlackSmithBuildingModel();
            blackSmith.Level = blackSmith.MaxLevel;
            blackSmith.Initialize();

            var index = 0;
            while (blackSmith.ProductReserveEquipment())
            {
                Debug.Log($"대장간 장비 생산 의뢰 남은 시간: {blackSmith.ProductEquipmentModels[index++].GetRemainProductPeriod}");
            }

            blackSmith.SpendTime();

            blackSmith.ProductEquipmentModels.Foreach(pem =>
            {
                var equipment = pem.Value.ProductObject as EquipmentBase;
                Debug.Log($"대장간 장비 생산: {equipment}");
            });
        }


        [Test]
        public void 대장간_장비_강화()
        {
            var uidIssuancer = new TestUIdIssuancer();

            TableDataManager.Instance.LoadTableDatas().Forget();
            EquipmentGenerator.UniqueIndexIssuancer = uidIssuancer;

            var equipment = EquipmentGenerator.GenerateEquipment(EquipmentDefine.CommonEquipmentDropProbTable);
            var blackSmith = new BlackSmithBuildingModel();
            blackSmith.Level = blackSmith.MaxLevel;
            blackSmith.Initialize();

            for (var i = 0; i < EquipmentDefine.MaxEnhanceLevel; i++)
            {
                blackSmith.EnhanceEquipment(equipment);
                for (var z = 0; z < equipment.AttachedStatusSlots.Count; z++)
                {
                    Debug.Log($"장비 강화됨, 레벨: {equipment.Level}, {equipment.AttachedStatusSlots[z]}");
                }
            }
        }


        [Test]
        public void 대장간_장비_감정()
        {
            var uidIssuancer = new TestUIdIssuancer();

            TableDataManager.Instance.LoadTableDatas().Forget();
            EquipmentGenerator.UniqueIndexIssuancer = uidIssuancer;

            var equipment = EquipmentGenerator.GenerateEquipment(EquipmentDefine.CommonEquipmentDropProbTable);
            var blackSmith = new BlackSmithBuildingModel();
            blackSmith.Level = blackSmith.MaxLevel;
            blackSmith.Initialize();

            while (equipment.RemainSlot)
            {
                blackSmith.AppraisalEquipment(equipment);
                var slotAbility = equipment.AttachedStatusSlots.Last(x =>
                    x.EquipmentStatusSlotState == EquipmentStatusSlotState.Ability);
                Debug.Log($"장비 감정 됨: {slotAbility}");
            }
        }
    }
}