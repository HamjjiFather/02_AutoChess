using System.Linq;
using UnityEngine.UI;
using KKSFramework.DataBind.Extension;

namespace KKSFramework.DataBind
{
    public class TextLabelPropertiesBind : BindableProperties<Text, string[]>
    {
        protected override string[] GetDelegate () =>
            targetComponents.Select (x => x.GetComponent<Text> ().text).ToArray ();

        protected override void SetDelegate (string[] values) =>
            targetComponents.ZipForEach (values, (comp, value) => { comp.text = value; });
    }
}