using System;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using KKSFramework;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

namespace AutoChess
{
    [RequireComponent(typeof(OnScreenButton))]
    public class EnvironmentMouseDownDetector : MonoBehaviour
    {
        #region Fields & Property

        public event Action OnMouseDownEvent;

        private OnScreenButton _onScreenButton;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            GetComponent<OnScreenButton>()
                .GetAsyncPointerDownTrigger()
                .TakeUntilCanceled(destroyCancellationToken)
                .Subscribe(_ => { OnMouseDownEvent.CallSafe(); });
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}