using System;
using BaseFrame;
using KKSFramework.DataBind;

namespace AutoChess
{
    public class AdventureResultPopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        #endregion

        
        #region Methods

        protected override void OnPush (Parameters pushValue)
        {
            base.OnPush (pushValue);
        }


        #endregion


        #region EventMethods

        protected override void OnClickClose ()
        {
            base.OnClickClose ();
        }


        #endregion
    }
}