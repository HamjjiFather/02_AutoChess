using AutoChess.Helper;
using KKSFramework.DesignPattern;
using KKSFramework.GameSystem.GlobalText;
using Zenject;

namespace AutoChess
{
    public class SkillViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private StatusViewmodel _statusViewmodel;

        [Inject]
        private CommonColorSetting _commonColorSetting;

#pragma warning restore CS0649

        #endregion


        public override void Initialize ()
        {
        }


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}