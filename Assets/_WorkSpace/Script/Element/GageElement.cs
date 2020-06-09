using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HexaPuzzle
{
    public class GageElement : MonoBehaviour
    {
        #region Fields & Property

        public Slider slider;

        public Text sliderText;
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public virtual void SetValue (IntReactiveProperty gageValue, int maxValue)
        {
            gageValue.Subscribe (value =>
            {
                SetValue (gageValue.Value, maxValue);
            });
        }


        public void SetValue (int gageValue, int maxValue)
        {
            slider.value = (float)gageValue / maxValue;
            sliderText.text = $"{gageValue}/{maxValue}";
        }
        

        #endregion
        


        #region EventMethods

        #endregion
    }
}