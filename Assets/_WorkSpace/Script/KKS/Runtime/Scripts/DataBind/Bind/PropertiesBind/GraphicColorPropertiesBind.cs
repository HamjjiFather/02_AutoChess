using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class GraphicColorPropertiesBind : BindableProperties<Graphic, Color[]>
    {
        protected override Color[] GetDelegate () =>
            targetComponents.Select (x => x.GetComponent<Graphic> ().color).ToArray ();

        protected override void SetDelegate (Color[] values) =>
            targetComponents.ZipForEach (values, (comp, value) => comp.GetComponent<Graphic> ().color = value);
    }
}