using Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    
    public class OutpostExtendInfoArea : MonoBehaviour
    {
        #region Fields & Property

        public TextMeshProUGUI buildingNameText;

        public Button buildButton;

        public GameObject builtObj;

        private OustpostExtendCellModel _extendCellModel;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void SetArea(OustpostExtendCellModel cellModel)
        {
            _extendCellModel = cellModel;
            buildingNameText.text = LocalizeHelper.FromName(_extendCellModel.TableData.BuildingName);
            builtObj.SetActive(_extendCellModel.HasBuilt);
            buildButton.gameObject.SetActive(!_extendCellModel.HasBuilt);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}