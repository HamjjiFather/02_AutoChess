using System.Linq;
using BaseFrame;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class RawImageTextureBindableProperties : BindableProperties<RawImage, Texture>
    {
        protected override Texture GetDelegate () => targetComponents.First ().GetComponent<RawImage> ().texture;

        protected override void SetDelegate (Texture value) => targetComponents.ForEach (x => x.texture = value);
    }
}