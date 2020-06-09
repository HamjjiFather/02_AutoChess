using System;
using BaseFrame;
using BaseFrame.DoTween.Async.Triggers;
using DG.Tweening;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace HexaPuzzle
{
    public class SummonGageElement : MonoBehaviour
    {
        #region Fields & Property

        public Text gageText;

        public Slider[] silders;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private const float GageAnimationTime = 2f; 

        #endregion


        #region Methods

        public async UniTask SetGageAnimation (float gageValue)
        {
            InitSlider ();
            
            var i = 0;
            var time = gageValue / 100 + (gageValue % 100 > 0 ? 1 : 0);

            gageText.text = $"{gageValue}";
            
            while (gageValue > 0)
            {
                var gage = Math.Min (gageValue, 100);
                var silder = silders[i % 2];
                silder.transform.SetAsLastSibling ();

                silder.value = 0;
                await silder.DOValue (gage / 100f, GageAnimationTime / time).WaitForCompleteAsync ();

                gageValue -= gage;
                i++;
            }

            await UniTask.Delay (TimeSpan.FromSeconds (2));
        }

        private void InitSlider ()
        {
            silders.Foreach (x => x.value = 0);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}