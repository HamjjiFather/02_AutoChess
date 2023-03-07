using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.PlayerLoop;
using Zenject;

namespace AutoChess
{
    public class CheatInstaller : MonoInstaller
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        public override void InstallBindings()
        {
            // base.InstallBindings();
        }

#if UNITY_EDITOR
        public override void Start()
        {
            base.Start();
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Keyboard.current.f1Key.IsPressed())
                {
                    MessageBroker.Default.Publish(new RegionInfoUpdateMessage(1));
                }
            });
        }
#endif

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}