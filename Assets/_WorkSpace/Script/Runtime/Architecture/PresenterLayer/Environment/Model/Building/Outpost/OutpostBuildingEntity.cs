using System;
using Zenject;

namespace AutoChess
{
    [Flags]
    public enum OutpostAddOnTypes
    {
        WatchTower = 1 << 1,
        
        SettlementPost = 1 << 2,
        
        CaravanPost = 1 << 3,
        
        ArchLab = 1 << 4,
        
        Armory = 1 << 5,
        
        Hospital = 1 << 6,
        
        CollectionCenter = 1 << 7,
    }
    
    /// <summary>
    /// 세계맵에 배치된 전초기지 건물.
    /// </summary>
    public class OutpostBuildingEntity : BuildingEntityBase, IInitializable
    {
        public OutpostBuildingEntity(Outpost buildingTableData) : base()
        {
        }

        #region Fields & Property

        public int OutpostIndex;

        #endregion


        #region Methods

        #region Override

        public override void Build()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}