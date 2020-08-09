using System;
using UniRx;
using Cysharp.Threading.Tasks;
using KKSFramework;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    [RequireComponent (typeof (ScrollRect))]
    public class ScrollSnap : CachedComponent
    {
        #region Fields & Property

        public ScrollRect scrollRect => GetCachedComponent<ScrollRect> ();

        private bool _isMoving;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private IDisposable _snapToAsyncDisposable;

        #endregion


        public void SnapTo (RectTransform target)
        {
            Canvas.ForceUpdateCanvases ();

            var targetAnchoredPosition =
                (Vector2) scrollRect.transform.InverseTransformPoint (scrollRect.content.position)
                - (Vector2) scrollRect.transform.InverseTransformPoint (target.position);

            var anchoredPosition = scrollRect.content.anchoredPosition;
            var finallyPosition = new Vector2 (scrollRect.horizontal ? targetAnchoredPosition.x : anchoredPosition.x,
                scrollRect.vertical ? targetAnchoredPosition.y : anchoredPosition.y);
            anchoredPosition = finallyPosition;

            scrollRect.content.anchoredPosition = anchoredPosition;
        }


        public async UniTask SnapToAsync (RectTransform target, float duration = 1f)
        {
            if (_isMoving) return;

            _isMoving = true;
            var content = scrollRect.content;
            var startAnchoredPosition = content.anchoredPosition;
            var targetAnchoredPosition =
                (Vector2) scrollRect.transform.InverseTransformPoint (content.position) -
                (Vector2) scrollRect.transform.InverseTransformPoint (target.position);
            
            _snapToAsyncDisposable = Observable.EveryUpdate ().Subscribe (_ =>
            {
                var syncedPosition = Vector2.Lerp (targetAnchoredPosition, startAnchoredPosition, duration);
                var finallyPosition = new Vector2 (scrollRect.horizontal ? syncedPosition.x : startAnchoredPosition.x,
                    scrollRect.vertical ? syncedPosition.y : startAnchoredPosition.y);
                scrollRect.content.anchoredPosition = finallyPosition;

                duration -= Time.deltaTime;

                if (duration > 0) return;
                _isMoving = false;
            });
            
            await UniTask.WaitWhile (() => _isMoving);
            _snapToAsyncDisposable.DisposeSafe ();
        }
    }
}