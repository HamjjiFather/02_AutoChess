using Cysharp.Threading.Tasks;
using DG.Tweening;
using KKSFramework;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class GageElement : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private Slider _slider;

        [Resolver]
        private Slider _effectSlider;

        [Resolver]
        private Image _gageImage;

        [Resolver]
        private Text _sliderText;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetSliderValue (float value)
        {
            _slider.value = value;
        }


        public async UniTask SliderAsync (float value, float asyncSeconds = 0.25f)
        {
            await _slider.DOValue (value, asyncSeconds).WaitForCompleteAsync ();
            _effectSlider.DOValue (value, asyncSeconds).WaitForCompleteAsync ().Forget();
        } 
        
        
        public void SetValueOnlyGageValueAsync (int gageValue, int maxValue)
        {
            _sliderText.text = $"{gageValue}";
            SliderAsync(gageValue/(float)maxValue).Forget();
        }
        
        
        public void SetValueOnlyGageValue (int gageValue, int maxValue)
        {
            _slider.value = (float)gageValue / maxValue;
            _sliderText.text = $"{gageValue}";
        }


        public void SetValueAsync (int gageValue, int maxValue)
        {
            _sliderText.text = $"{gageValue}/{maxValue}";
            SliderAsync(gageValue/(float)maxValue).Forget();
        }
        

        public void SetValue (int gageValue, int maxValue)
        {
            _slider.value = (float)gageValue / maxValue;
            _sliderText.text = $"{gageValue}/{maxValue}";
        }


        public void SetValue (float gageValue, float maxValue)
        {
            _slider.value = gageValue / maxValue;
            _sliderText.text = $"{gageValue}/{maxValue}";
        }


        public void SetGageColor (Color color)
        {
            _gageImage.color = color;
        }


        public void SetCustomText (string text)
        {
            _sliderText.text = text;
        }
        

        #endregion
        

        #region EventMethods

        #endregion
    }
}