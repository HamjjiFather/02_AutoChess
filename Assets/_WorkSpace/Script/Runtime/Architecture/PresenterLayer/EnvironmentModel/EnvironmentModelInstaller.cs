using KKSFramework.Base;
using KKSFramework.Presenter;

namespace AutoChess.Presenter
{
    public class EnvironmentModelInstaller: InstallerBase<IEnvironmentModel>
    {
        #region Fields & Property

        protected override BindOption BindOption => BindOption.AsSingle;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public override void PrepareInstaller()
        {
            RegisterInstallItem<AdventureEnvironmentModel>();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}