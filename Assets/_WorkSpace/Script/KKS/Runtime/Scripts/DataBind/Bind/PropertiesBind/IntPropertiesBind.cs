using UnityEngine;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class IntPropertiesBind : BaseValueBindableProperties<Component, int[]>
    {
        protected override void SetDelegate (int[] values)
        {
            targetComponents.ZipForEach (values, (comp, value) =>
            {
                var target = comp.GetComponent (targetComponent.GetType ());
                targetComponent.GetType ().GetProperty (propertyName)?.SetValue (target, value);
            });
        }
    }
}