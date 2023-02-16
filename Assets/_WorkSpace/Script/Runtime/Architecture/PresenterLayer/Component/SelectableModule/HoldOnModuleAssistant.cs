using UnityEngine.Events;

namespace KKSFramework.Module
{
    public class HoldOnModuleAssistant
    {
        #region Fields & Property

        public UnityAction ClickAction;

        private SelectableModuleBehaviour _enhanceButton;

        #endregion


        #region Methods

        #region Override

        public void InitializeAssistant(SelectableModuleBehaviour module, UnityAction clickAction)
        {
            _enhanceButton = module;
            _enhanceButton.HoldOnModuleSubscribe(clickAction.Invoke);
        }

        #endregion


        #region This

        public void UseHoldOnModule(bool use) => _enhanceButton.SetUseHoldOnModuleState(use);


        public void DisposeHoldOnModule() => _enhanceButton.DisposeHoleOnModule();

        public void ReSubscribe() => _enhanceButton.ReSubscribe(_enhanceButton.button);

        public bool Initialized => _enhanceButton.Initialized;

        #endregion


        #region Event

        #endregion

        #endregion
    }
}