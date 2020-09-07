using System.Linq;
using BaseFrame;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public class IntPropertiesBind : BaseValueBindableProperties<Component, int>
    {
        protected override void SetDelegate (int value)
        {
            targetComponents.Select (x => x.GetComponent (targetComponent.GetType ()))
                .ForEach (x => x.GetType ().GetProperty (propertyName)?.SetValue (x, value));
        }
    }
}