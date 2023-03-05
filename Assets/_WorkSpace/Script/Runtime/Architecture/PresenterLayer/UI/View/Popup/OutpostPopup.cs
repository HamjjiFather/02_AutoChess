using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.Module;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class OutpostPopup : PopupViewBase
    {
        #region Fields & Property

        [Inject]
        private OutpostViewModel _outpostViewModel;

        public GameObject preBuildAreaObj;

        public GameObject buildButton;
        
        public GameObject postBuildAreaObj;

        public OutpostExtendInfoArea[] extendInfoAreas;

        private OutpostBuildingEntity _outpostBuilding;

        #endregion


        #region Methods

        #region Override

        protected override async UniTask OnPush(object pushValue = null)
        {
            if (pushValue is int outpostIndex)
            {
                _outpostBuilding = _outpostViewModel.GetOutpostBuildingEntity(outpostIndex);
                buildButton.GetComponent<Button>().onClick.AddListener(OnBuildButton_Click);
                SetState();
            }
        }

        #endregion


        #region This

        void SetState()
        {
            preBuildAreaObj.SetActive(!_outpostBuilding.HasBuilt);
            postBuildAreaObj.SetActive(_outpostBuilding.HasBuilt);

            if (_outpostBuilding.HasBuilt)
            {
                PostBuiltState();
                return;
            }

            PreBuildState();

            // 짓기 전 상태 처리.
            void PreBuildState()
            {
                var currencyPresentVo = CurrencyHelper.GetCurrencyPresentVo(BuildingDefine.OutpostBuildCostType,
                    BuildingDefine.OutpostBuildCost);
                
                buildButton.GetComponent<CurrencyInfoArea>().SetArea(currencyPresentVo);
            }

            // 지은 후 상태 처리
            void PostBuiltState()
            {
                var cellModels = _outpostViewModel.ExtendCellModels(_outpostBuilding.OutpostIndex);
                extendInfoAreas.ZipForEach(cellModels, (eia, cm) => eia.SetArea(cm));
            }
        }

        #endregion


        #region Event

        public void OnBuildButton_Click()
        {
            if (_outpostViewModel.ExecuteBuildOutpost(_outpostBuilding))
            {
                SetState();
                return;
            }
        }


        public void OnExtendButton_Click(int index)
        {
        }

        #endregion

        #endregion
    }
}