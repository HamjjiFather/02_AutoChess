using AutoChess.Presenter;
using KKSFramework.Base;

namespace KKSFramework.Presenter
{
    public class ViewModelInstaller : InstallerBase<IViewModel>
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
            RegisterInstallItem<InventoryViewModel>();
            RegisterInstallItem<CharacterViewmodel>();
            RegisterInstallItem<AdventureViewModel>();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}