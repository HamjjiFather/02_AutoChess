using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.InGame;

namespace AutoChess
{
    public partial class BlackSmithBuildingModel
    {
        public class PurchaseSlotModel
        {
            public EquipmentBase PurchaseEquipment;
        }

        #region Fields & Property

        /// <summary>
        /// 구매 가능한 기본 장비 수량.
        /// </summary>
        public const int BasePurEquipmentAmount = 2;

        /// <summary>
        /// 장비의 기본 구매 기간.
        /// </summary>
        public const int BasePurEquipmentPeriod = 2;

        /// <summary>
        /// 장비 구매 슬롯.
        /// </summary>
        public Dictionary<int, PurchaseSlotModel> PurchaseSlotModels;

        /// <summary>
        /// 구매 가능한 장비의 수량.
        /// </summary>
        public int GetPurchasableEquipmentAmount
        {
            get
            {
                return BasePurEquipmentAmount + GetAdditionalAmount();

                int GetAdditionalAmount()
                {
                    return Level switch
                    {
                        >= 2 and < 4 => 1,
                        >= 4 and <= BuildingDefine.MaxLevel => 2,
                        _ => 0
                    };
                }
            }
        }


        public int GetPurchasableEquipmentPeriod
        {
            get
            {
                return BasePurEquipmentPeriod + GetAdditionalAmount();

                int GetAdditionalAmount()
                {
                    return Level switch
                    {
                        // >= 2 and < 4 => -1,
                        // >= 4 and <= BuildingDefine.MaxLevel => -2,
                        _ => 0
                    };
                }
            }
        }


        /// <summary>
        /// 구매 장비 생성 확률.
        /// </summary>
        public EquipmentProbabilityTable PurchaseProbabilityTable
        {
            get
            {
                return Level switch
                {
                    0 => new EquipmentProbabilityTable(new[] {76000, 24000, 0, 0}),
                    1 => new EquipmentProbabilityTable(new[] {60000, 38000, 2000, 0}),
                    2 => new EquipmentProbabilityTable(new[] {36000, 46000, 8000, 0}),
                    3 => new EquipmentProbabilityTable(new[] {20000, 56000, 12000, 2000}),
                    4 => new EquipmentProbabilityTable(new[] {10000, 69000, 16500, 4500}),
                    5 => new EquipmentProbabilityTable(new[] {0, 72000, 20000, 8000}),
                    _ => default
                };
            }
        }

        private int _remainRefreshPeriod;

        #endregion


        #region Methods

        #region Override

        private void Initialize_Purchase()
        {
            // TODO: 저장된 데이터를 가져와야 한다.
            PurchaseSlotModels = new Dictionary<int, PurchaseSlotModel>(GetProductableEquipmentAmount);
            PurchaseSlotModels.AddRange(Enumerable.Range(0, GetProductableEquipmentAmount)
                .ToDictionary(i => i, _ => new PurchaseSlotModel()));
        }


        private void OnLevelUp_Purchase()
        {
            PurchaseSlotModels.EnsureCapacity(GetProductableEquipmentAmount);
            for (var i = 0; i < GetProductableEquipmentAmount - PurchaseSlotModels.Count; i++)
            {
                PurchaseSlotModels.Add(PurchaseSlotModels.Count - 1, new PurchaseSlotModel());
            }
        }


        public void SpendTime_Purchase()
        {
            _remainRefreshPeriod--;

            if (_remainRefreshPeriod != 0) return;
            CreateEquipmentsForPurchase();
            _remainRefreshPeriod = GetPurchasableEquipmentPeriod;
        }

        #endregion


        #region This

        private void CreateEquipmentsForPurchase()
        {
            PurchaseSlotModels.Values.Foreach(psm =>
            {
                var newEquipment = EquipmentGenerator.GenerateEquipment(PurchaseProbabilityTable);
                psm.PurchaseEquipment = newEquipment;
            });
        }

        #endregion


        #region Event

        /// <summary>
        /// 장비 구매.
        /// </summary>
        public void PurchaseEquipment(int arrayIndex)
        {
            var equipment = PurchaseSlotModels[arrayIndex].PurchaseEquipment;
            var useCase = GameSceneInstaller.Instance.Resolve<PurchaseEquipmentUseCase>();
            useCase.Execute(equipment);
        }

        #endregion

        #endregion
    }
}