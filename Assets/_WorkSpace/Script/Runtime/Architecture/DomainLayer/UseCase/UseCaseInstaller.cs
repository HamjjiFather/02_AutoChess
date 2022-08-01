using AutoChess.UseCase;
using KKSFramework.Base;

namespace KKSFramework.Domain
{
    public class UseCaseInstaller : InstallerBase<IUseCaseBase>
    {
        #region Fields & Property
        

        #endregion


        #region Methods

        #region Override

        protected override BindOption BindOption => BindOption.AsTransient;

        #endregion


        #region This

        public override void PrepareInstaller()
        {
            RegisterInstallItem<CharacterListRequestUseCase>();
            RegisterInstallItem<AdvItemListRequestUseCase>();
            RegisterInstallItem<AdvCharListUseCase>();
            RegisterInstallItem<NewItemUseCase>();
        }

        public void Installation()
        {
            base.Installation();
        }

        public override void Finish()
        {
            base.Finish();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}