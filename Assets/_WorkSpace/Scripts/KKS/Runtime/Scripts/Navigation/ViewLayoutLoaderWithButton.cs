using BaseFrame;
using KKSFramework.DataBind;
using UnityEngine.UI;

namespace KKSFramework.Navigation
{
    /// <summary>
    /// 페이지에서 규칙적으로 호출되는 ViewLayout들을 호출하기 위한 컴포넌트.
    /// </summary>
    public class ViewLayoutLoaderWithButton : ViewLayoutLoaderBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Button[] _layoutViewButton;

        public Button[] LayoutViewButton => _layoutViewButton;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            _layoutViewButton?.ForEach ((button, index) =>
            {
                button.onClick.AddListener (() => ClickLayoutViewButton (index));
            });
            base.Awake ();
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        private void ClickLayoutViewButton (int index)
        {
            SetSubView (index);
        }

        #endregion
    }
}