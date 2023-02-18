using Cysharp.Threading.Tasks;
using KKSFramework.Module;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    [RequireComponent(typeof(BuildingAreaBase))]
    public class WarehouseBuildingAreaModule : ModuleBehaviour 
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
                case (int)BuildingDefine.WarehouseBehaviourType.Store:
                {
                    NavigationHelper.OpenPopupAsync(NavigationViewType.WarehousePopup).Forget();
                    break;
                }
            }
        }

        #endregion

        #endregion
    }
}