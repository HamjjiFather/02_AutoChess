using UnityEngine;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class StringPropertiesBind : BaseValueBindableProperties<Component, string[]>
    {
        protected override void SetDelegate (string[] values)
        {
            targetComponents.ZipForEach (values, (comp, value) =>
            {
                var target = comp.GetComponent (targetComponent.GetType ());
                targetComponent.GetType ().GetProperty (propertyName)?.SetValue (target, value);
            });
        }
    }
}