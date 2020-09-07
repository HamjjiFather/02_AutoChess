using UnityEngine;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class BooleanPropertiesBind : BaseValueBindableProperties<Component, bool[]>
    {
        protected override void SetDelegate (bool[] values)
        {
            targetComponents.ZipForEach (values, (comp, value) =>
            {
                var target = comp.GetComponent (targetComponent.GetType ());
                targetComponent.GetType ().GetProperty (propertyName)?.SetValue (target, value);
            });
        }
    }
}