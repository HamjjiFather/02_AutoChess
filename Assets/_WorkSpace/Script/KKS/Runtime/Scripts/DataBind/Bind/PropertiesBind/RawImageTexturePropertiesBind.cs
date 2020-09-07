using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class RawImageTextureBindableProperties : BindableProperties<RawImage, Texture[]>
    {
        protected override Texture[] GetDelegate () =>
            targetComponents.Select (x => x.GetComponent<RawImage> ().texture).ToArray ();

        protected override void SetDelegate (Texture[] values) =>
            targetComponents.ZipForEach (values, (comp, value) => { comp.texture = value; });
    }
}