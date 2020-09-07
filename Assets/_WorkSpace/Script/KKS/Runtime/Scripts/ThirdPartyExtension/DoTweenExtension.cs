﻿using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace KKSFramework
{
    public static class DoTweenExtensions
    {
        public static TweenerCore<float, float, FloatOptions> DOFade (this CanvasGroup target, float endValue,
            float duration)
        {
            var tweenCore = DOTween.To (() => target.alpha, x => target.alpha = x, endValue, duration);
            tweenCore.SetTarget (target);
            return tweenCore;
        }


        public static UniTask<Tweener> WaitForCompleteAsync (this Tweener tweener,
            CancellationToken cancellationToken = default)
        {
            var completionSource = new UniTaskCompletionSource<Tweener> ();
            tweener.OnComplete (() =>
            {
                if (!cancellationToken.IsCancellationRequested) completionSource.TrySetResult (tweener);
            });
            return completionSource.Task;
        }
    }
}