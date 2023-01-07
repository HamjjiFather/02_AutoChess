using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    public partial class BlackSmithBuilding
    {
        /// <summary>
        /// 장비 생산 상태.
        /// </summary>
        public enum ProductState
        {
            Empty,
            Reserved,
            Produced,
        }

        /// <summary>
        /// 생산된 장비 슬롯.
        /// </summary>
        public class ProductSlotModel
        {
            public ProductSlotModel(ProductState productState)
            {
                ProductState = productState;
            }

            /// <summary>
            /// 생산 상태.
            /// </summary>
            public ProductState ProductState;

            /// <summary>
            /// 생산 관련 데이터.
            /// </summary>
            public object ProductObject;

            /// <summary>
            /// 생산까지 남은 시간.
            /// </summary>
            public int GetRemainProductDuration
            {
                get
                {
                    if (ProductState == ProductState.Reserved)
                        return (int) ProductObject;

                    return default;
                }
            }
        }

        #region Fields & Property

        #region Product

        /// <summary>
        /// 제작 가능한 장비의 수량.
        /// </summary>
        public int GetProductableEquipmentAmount
        {
            get
            {
                return BuildingDefine.BaseProductableEquipmentAmount + GetAdditionalProductableEquipmentAmount();

                int GetAdditionalProductableEquipmentAmount()
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

        /// <summary>
        /// 장비 제작 기간.
        /// </summary>
        public int GetProductDuration => BuildingDefine.BaseProductEquipmentDuration;

        /// <summary>
        /// 제작 장비 모델.
        /// </summary>
        public Dictionary<int, ProductSlotModel> ProductEquipmentModels;

        /// <summary>
        /// 장비 생성 확률.
        /// </summary>
        public EquipmentProbabilityTable ProductProbabilityTable
        {
            get
            {
                return Level switch
                {
                    _ => new EquipmentProbabilityTable(new[]{15000, 45000, 25000, 15000})
                };
            }
        }

        #endregion

        #endregion


        #region Methods

        #region Override

        private void OnLevelUp_Product()
        {
            ProductEquipmentModels.EnsureCapacity(GetProductableEquipmentAmount);
            for (var i = 0; i < GetProductableEquipmentAmount - ProductEquipmentModels.Count; i++)
            {
                ProductEquipmentModels.Add(ProductEquipmentModels.Count - 1, new ProductSlotModel(ProductState.Empty));
            }
        }


        private void SpendTime_Product()
        {
            // 모든 제작중인 장비들의 기간 단축.
            ProductEquipmentModels.Values
                .Where(pe => pe.ProductState == ProductState.Reserved)
                .Foreach(pe =>
                {
                    var dur = pe.GetRemainProductDuration;
                    pe.ProductObject = Mathf.Max(dur - 1, 0);
                });
            
            // 모든 제작중이면서 기간이 0인 장비 슬롯에 장비를 추가함.
            ProductEquipmentModels
                .Where(pe =>
                    pe.Value.ProductState == ProductState.Reserved && pe.Value.GetRemainProductDuration.Equals(0))
                .ToList()
                .Foreach(kvp =>
                {
                    var products = EquipmentGenerator.GenerateEquipment(ProductProbabilityTable);
                    ProductEquipmentModels[kvp.Key].ProductState = ProductState.Produced;
                    ProductEquipmentModels[kvp.Key].ProductObject = products;
                });
        }

        #endregion


        #region This

        private void Initialize_Product()
        {
            // TODO: 저장된 데이터를 가져와야 한다.
            ProductEquipmentModels = new Dictionary<int, ProductSlotModel>(GetProductableEquipmentAmount);
            ProductEquipmentModels.AddRange(Enumerable.Range(0, GetProductableEquipmentAmount)
                .ToDictionary(i => i, _ => new ProductSlotModel(ProductState.Empty)));
        }

        #endregion


        #region Event

        /// <summary>
        /// 제작 의뢰.
        /// </summary>
        public bool ProductReserveEquipment()
        {
            var hasEmptySlot = ProductEquipmentModels.Values.Count(x => x.ProductState.Equals(ProductState.Empty)) >= 1;
            if (!hasEmptySlot)
            {
                return false;
            }

            var emptyIndex = ProductEquipmentModels.Values.ToList().FindIndex(x => x.ProductState == ProductState.Empty);
            ProductEquipmentModels[emptyIndex].ProductState = ProductState.Reserved;
            ProductEquipmentModels[emptyIndex].ProductObject = BuildingDefine.BaseProductEquipmentDuration;
            return true;
        }


        /// <summary>
        /// 제작 완료된 장비를 폐기.
        /// </summary>
        public bool DeleteProductEquipment(int arrayIndex)
        {
            if (ProductEquipmentModels[arrayIndex].ProductState != ProductState.Empty)
                return false;

            ProductEquipmentModels[arrayIndex].ProductState = ProductState.Empty;
            return true;
        }

        #endregion

        #endregion
    }
}