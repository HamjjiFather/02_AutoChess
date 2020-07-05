using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
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

        public void SetSliderValue (float value)
        {
            slider.value = value;
        }
        
        
        public void SetValueOnlyGageValue (int gageValue, int maxValue)
        {
            slider.value = (float)gageValue / maxValue;
            sliderText.text = $"{gageValue}";
        }
        

        public void SetValue (int gageValue, int maxValue)
        {
            slider.value = (float)gageValue / maxValue;
            sliderText.text = $"{gageValue}/{maxValue}";
        }


        public void SetValue (float gageValue, float maxValue)
        {
            slider.value = gageValue / maxValue;
            sliderText.text = $"{gageValue}/{maxValue}";
        }


        public void SetGageColor (Color color)
        {
            var block = slider.colors;
            block.disabledColor = color;
            slider.colors = block;
        }
        

        #endregion
        

        #region EventMethods

        #endregion
    }
}