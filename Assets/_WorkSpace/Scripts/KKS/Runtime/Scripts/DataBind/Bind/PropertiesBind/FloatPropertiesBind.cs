using UnityEngine;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class FloatPropertiesBind : BaseValueBindableProperties<Component, float[]>
    {
        protected override void SetDelegate (float[] values)
        {
            targetComponents.ZipForEach (values, (comp, value) =>
            {
                var target = comp.GetComponent (targetComponent.GetType ());
                targetComponent.GetType ().GetProperty (propertyName)?.SetValue (target, value);
            });
        }
    }
}