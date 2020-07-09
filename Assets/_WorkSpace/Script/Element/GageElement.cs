using BaseFrame.DoTween.Async.Triggers;
using DG.Tweening;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class GageElement : MonoBehaviour
    {
        #region Fields & Property

        public Slider slider;

        public Slider effectSlider;

        public Image gageImage;

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


        public async UniTask SliderAsync (float value, float asyncSeconds = 0.25f)
        {
            await slider.DOValue (value, asyncSeconds).WaitForCompleteAsync ();
            effectSlider.DOValue (value, asyncSeconds).WaitForCompleteAsync ().Forget();
        } 
        
        
        public void SetValueOnlyGageValueAsync (int gageValue, int maxValue)
        {
            sliderText.text = $"{gageValue}";
            SliderAsync(gageValue/(float)maxValue).Forget();
        }
        
        
        public void SetValueOnlyGageValue (int gageValue, int maxValue)
        {
            slider.value = (float)gageValue / maxValue;
            sliderText.text = $"{gageValue}";
        }


        public void SetValueAsync (int gageValue, int maxValue)
        {
            sliderText.text = $"{gageValue}/{maxValue}";
            SliderAsync(gageValue/(float)maxValue).Forget();
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
            gageImage.color = color;
        }


        public void SetCustomText (string text)
        {
            sliderText.text = text;
        }
        

        #endregion
        

        #region EventMethods

        #endregion
    }
}