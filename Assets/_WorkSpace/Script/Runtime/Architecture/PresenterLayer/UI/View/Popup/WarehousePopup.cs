using Cysharp.Threading.Tasks;
using KKSFramework.Module;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class WarehousePopup : PopupViewBase
    {
        #region Fields & Property

        [Inject]
        private WarehouseViewModel _warehouseViewModel;

        public ElementLoadModule elementLoader;

        #endregion


        #region Methods

        #region Override

        protected override async UniTask OnPush(object pushValue = null)
        {
            var cellModels = _warehouseViewModel.WarehouseSlotCellModels;
            var param = new ElementLoadModule.ScrollLoaderParameter(25, "_Prefab/Element/WarehouseSlotElement",
                OnCreateElement);
            await elementLoader.Execute(param);
            await base.OnPush(pushValue);

            void OnCreateElement(ElementView element, int index)
            {
                ((element as WarehouseSlotElement)!).SetElement(cellModels[index]);
            }
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}