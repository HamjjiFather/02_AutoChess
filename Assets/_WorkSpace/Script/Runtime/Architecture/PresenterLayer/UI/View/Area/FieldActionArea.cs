using System;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class FieldActionArea : AreaView
    {
        public readonly struct FieldActionAreaParameter
        {
            public FieldActionAreaParameter(FieldActionType fieldActionType, UnityAction clickAction)
            {
                FieldActionType = fieldActionType;
                ClickAction = clickAction;
            }

            public readonly FieldActionType FieldActionType;

            public readonly UnityAction ClickAction;
        }
        
        #region Fields & Property

        public Button[] fieldActionButtons;

        private FieldActionType _currentFieldAction;

        #endregion


        #region Methods

        #region Override

        protected override UniTask OnShow(object pushValue = null)
        {
            // if (pushValue is FieldActionAreaParameter value)
            // {
            //     _currentFieldAction = value.FieldActionType;
            //     fieldActionButtons[(int)_currentFieldAction].onClick.AddListener(value.ClickAction);
            // }
            
            return base.OnShow(pushValue);
        }


        protected override UniTask OnHide()
        {
            // fieldActionButtons[(int)_currentFieldAction].onClick.RemoveAllListeners();
            // fieldActionButtons[(int)_currentFieldAction].gameObject.SetActive(false);
            // _currentFieldAction = FieldActionType.None;
            return base.OnHide();
        }

        #endregion


        #region This
        
        #endregion


        #region Event

        public void OnBgButton_Click()
        {
            Hide().Forget();
        }

        #endregion

        #endregion
    }
}