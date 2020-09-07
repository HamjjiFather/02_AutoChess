using System.Linq;
using BaseFrame;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class GraphicColorPropertiesBind : BindableProperties<Graphic, Color>
    {
        protected override Color GetDelegate () => targetComponents.First ().GetComponent<Graphic> ().color;

        protected override void SetDelegate (Color value) => targetComponents.ForEach (x => x.color = value);
    }
}