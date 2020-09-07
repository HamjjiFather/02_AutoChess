using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (RawImage))]
    public class RawImageTexturePropertyBind : BindableProperty<RawImage, Texture>
    {
        protected override Texture GetDelegate () => targetComponent.texture;

        protected override void SetDelegate (Texture value) => targetComponent.texture = value;
    }
}