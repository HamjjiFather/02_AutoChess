using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.Navigation;
using UniRx;
using Zenject;

namespace AutoChess.Presenter
{
    public struct OpenBuildingMenuMsg
    {
        public OpenBuildingMenuMsg(BuildingType buildingType)
        {
            BuildingType = buildingType;
        }

        public BuildingType BuildingType;
    }
    
    public class BasePage : PageViewBase
    {
        #region Fields & Property

        [Inject]
        private BuildingManager _buildingManager;

        [Inject]
        private BlackSmithViewModel _blackSmithViewModel;

        public BuildingAreaBase exploreOfficeBuildingMenuArea;

        public BuildingAreaBase blackSmithBuildingMenuArea;

        public BuildingAreaBase employmentOfficeBuildingMenuArea;

        public BuildingAreaBase warehouseBuildingMenuArea;

        public BuildingAreaBase graveyardBuildingMenuArea;

        private Dictionary<BuildingType, BuildingAreaBase> _areaViewByBuildingType = new();

        private BuildingType _controlledBuildingType;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _areaViewByBuildingType.Add(BuildingType.ExploreOffice, exploreOfficeBuildingMenuArea);
            _areaViewByBuildingType.Add(BuildingType.BlackSmith, blackSmithBuildingMenuArea);
            _areaViewByBuildingType.Add(BuildingType.EmploymentOffice, employmentOfficeBuildingMenuArea);
            _areaViewByBuildingType.Add(BuildingType.Warehouse, warehouseBuildingMenuArea);
            _areaViewByBuildingType.Add(BuildingType.Graveyard, graveyardBuildingMenuArea);
            
            _areaViewByBuildingType.Foreach(kvp => kvp.Value.Initialize(_buildingManager.GetBuilding(kvp.Key)));
            
            MessageBroker.Default.Receive<OpenBuildingMenuMsg>().TakeUntilDestroy(this).Subscribe(msg =>
            {
                _areaViewByBuildingType[_controlledBuildingType].Hide().Forget();
                _controlledBuildingType = msg.BuildingType;
                _areaViewByBuildingType[_controlledBuildingType].Show().Forget();
            });
        }

        #endregion


        #region Methods

        protected override UniTask OnPush(object pushValue = null)
        {
            return base.OnPush(pushValue);
            
        }

        #endregion


        #region EventMethods

        #endregion
    }
}