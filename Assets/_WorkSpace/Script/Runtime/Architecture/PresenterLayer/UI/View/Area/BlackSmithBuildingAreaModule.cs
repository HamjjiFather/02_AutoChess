using Cysharp.Threading.Tasks;
using KKSFramework.Module;
using KKSFramework.ModuleAllBlue;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    [RequireComponent(typeof(BuildingAreaBase))]
    public class BlackSmithBuildingAreaModule : ModuleBehaviour
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override void Execute()
        {
            var areaBase = GetComponent<BuildingAreaBase>();
            areaBase.OnBuilingActionButtonClick = OnBuildingActionButton_Click;
        }

        #endregion


        #region This

        #endregion


        #region Event

        private void OnBuildingActionButton_Click(int index)
        {
            switch (index)
            {
                case (int)BuildingDefine.BlackSmithBehaviourType.Purchase:
                {
                    NavigationHelper.OpenPopupAsync(NavigationViewType.BlackSmithPurchasePopup).Forget();
                    break;
                }
            }
        }

        #endregion

        #endregion
    }
}