using System.Collections.Generic;
using System.Linq;
using AutoChess.UseCase;
using JetBrains.Annotations;
using KKSFramework.InGame;
using KKSFramework.Presenter;
using Zenject;

namespace AutoChess
{
    /// <summary>
    /// 전초기지 증축물 모델.
    /// </summary>
    public readonly struct OustpostExtendCellModel
    {
        public OustpostExtendCellModel(OutpostExtend extend, bool hasBuilt)
        {
            TableData = extend;
            HasBuilt = hasBuilt;
        }

        /// <summary>
        /// 증축물 테이블.
        /// </summary>
        public readonly OutpostExtend TableData;

        /// <summary>
        /// 증축 여부.
        /// </summary>
        public readonly bool HasBuilt;
    }

    public class OutpostViewModel : IViewModel
    {
        #region Fields & Property

        [Inject]
        private BuildingManager _buildingManager;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        public List<OustpostExtendCellModel> ExtendCellModels(int index) =>
            typeof(OutpostExtendType).GetEnumValues().Cast<OutpostExtendType>().Select(x =>
            {
                var td = TableDataManager.Instance.OutpostExtendDict[(int)x];
                var hasBuilt = _buildingManager.OutpostBuildingEntities[index].ExtendBuildingList
                    .Contains((OutpostExtendType) (int)x);
                var cm = new OustpostExtendCellModel(td, hasBuilt);
                return cm;
            }).ToList();

        
        /// <summary>
        /// 전초기지 건물 데이터. 
        /// </summary>
        public OutpostBuildingEntity GetOutpostBuildingEntity(int index) =>
            _buildingManager.OutpostBuildingEntities[index];

        #endregion


        #region Event

        /// <summary>
        /// 전초기지 건설. 
        /// </summary>
        public bool ExecuteBuildOutpost(OutpostBuildingEntity entity)
        {
            entity.HasBuilt = true;
            
            var useCase = GameSceneInstaller.Instance.Resolve<BuildOutpostUseCase>();
            useCase.Execute(entity);

            return true;
        }

        #endregion

        #endregion
    }
}