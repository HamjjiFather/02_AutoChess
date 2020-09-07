using System.Linq;
using BaseFrame;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class TextLabelPropertiesBind : BindableProperties<Text, string>
    {
        protected override string GetDelegate () => targetComponents.First ().GetComponent<Text> ().text;

        protected override void SetDelegate (string value) => targetComponents.ForEach (x => x.text = value);
    }
}