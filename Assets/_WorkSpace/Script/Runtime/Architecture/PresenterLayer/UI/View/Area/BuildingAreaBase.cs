using System;
using KKSFramework;
using KKSFramework.Navigation;
using TMPro;
using UnityEngine.UI;

namespace AutoChess
{
    public class BuildingAreaBase : AreaView
    {
        #region Fields & Property

        public TextMeshProUGUI buildingNameText, buildingLevelText, buildingExpText;

        public Button[] buildingActionButtons;

        public Action<int> OnBuilingActionButtonClick; 

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public virtual void Initialize(BuildingEntityBase buildingEntity)
        {
            buildingLevelText.text = StringHelper.ToLevelString(buildingEntity.Level);
            buildingExpText.text = $"({buildingEntity.Exp}/{buildingEntity.RequireExp})";
            
            buildingActionButtons.Foreach((bt, i) => bt.onClick.AddListener(() => OnBuilingActionButtonClick.CallSafe(i)));
        }
        
        #endregion


        #region Event

        #endregion

        #endregion
    }
}