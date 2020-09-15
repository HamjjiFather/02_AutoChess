using KKSFramework.DataBind.Extension;
using UnityEngine;

namespace KKSFramework.DataBind
{
   public class MethodBind : Bindable
    {
        #region Fields & Property

        [HideInInspector]
        public string methodName;

        /// <summary>
        /// target comp.
        /// </summary>
        public Component targetComponent;

        public override object BindTarget => BindableExtension.ReturnMethod(targetComponent, methodName);


#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public override void Dispose ()
        {
            targetComponent = null;
            base.Dispose ();
        }

        #endregion
    }
}