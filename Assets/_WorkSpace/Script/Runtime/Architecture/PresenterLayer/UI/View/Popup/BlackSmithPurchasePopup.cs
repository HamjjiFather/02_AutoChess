using System.Linq;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class BlackSmithPurchasePopup : PopupViewBase
    {
        #region Fields & Property

        [Inject]
        private BlackSmithViewModel _blackSmithViewModel;

        public BlackSmithPurchaseItemElement[] elements;

        #endregion


        #region Methods

        #region Override

        protected override UniTask OnPush(object pushValue = null)
        {
            var cellModels = _blackSmithViewModel.GetPurchaseSlotCellModels;
            elements.Foreach((e, i) =>
            {
                var cm = cellModels.ElementAtOrDefault(i);
                e.SetElement(cm);
            });
            return base.OnPush(pushValue);
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}