using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace AutoChess
{
    /// <summary>
    /// 세계맵에 배치된 전초기지 건물.
    /// </summary>
    public class OutpostBuildingEntity : BuildingEntityBase, IInitializable
    {
        public OutpostBuildingEntity(Outpost buildingTableData, bool hasBuilt, int[] extends) : base()
        {
            TableData = buildingTableData;
            OutpostIndex = TableData.Id;
            HasBuilt = hasBuilt;
            ExtendBuildingList = extends.Select(e => (OutpostExtendType)e).ToList();
        }

        #region Fields & Property

        protected readonly Outpost TableData;

        public readonly int OutpostIndex;

        public bool HasBuilt;

        public readonly List<OutpostExtendType> ExtendBuildingList;

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
            HasBuilt = true;
        }

        public override void SpendTime()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnLevelUp(int level)
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize()
        {
        }

        #endregion


        #region This

        /// <summary>
        /// 전초기지 증축.
        /// </summary>
        public void AddExtendBuilding(OutpostExtendType extend)
        {
            if (ExtendBuildingList.Contains(extend))
                return;

            ExtendBuildingList.Add(extend);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}