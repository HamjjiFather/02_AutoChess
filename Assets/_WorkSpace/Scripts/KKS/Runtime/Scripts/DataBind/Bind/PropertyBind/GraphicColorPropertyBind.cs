using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Graphics))]
    public class GraphicColorPropertyBind : BindableProperty<Graphic, Color>
    {
        protected override Color GetDelegate () => targetComponent.color;

        protected override void SetDelegate (Color value) => targetComponent.color = value;
    }
}