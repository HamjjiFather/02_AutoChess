using AutoChess;
using AutoChess.Repository;
using KKSFramework.Base;

namespace KKSFramework.Repository
{
    public class RepositoryInstaller : InstallerBase<IRepository>
    {
        #region Fields & Property

        protected override BindOption BindOption => BindOption.AsTransient;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public override void PrepareInstaller()
        {
            RegisterInstallItem<AdventureInventoryRepository>();
            RegisterInstallItem<AdventureRepository>();
            RegisterInstallItem<CharacterRepository>();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}