using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Text))]
    public class TextLabelPropertyBind : BindableProperty<Text, string>
    {
        protected override string GetDelegate () => targetComponent.text;

        protected override void SetDelegate (string value) => targetComponent.text = value;
    }
}