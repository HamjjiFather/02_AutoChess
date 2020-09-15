using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (Image))]
    public class ImageSpritePropertyBind : BindableProperty<Image, Sprite>
    {
        protected override Sprite GetDelegate () => targetComponent.sprite;

        protected override void SetDelegate (Sprite value) => targetComponent.sprite = value;
    }
}