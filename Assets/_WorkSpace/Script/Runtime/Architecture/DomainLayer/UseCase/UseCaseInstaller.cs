using System.Linq;
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
            var mscorlib = typeof(UseCaseInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IUseCaseBase))))
            {
                RegisterInstallItem(type);
            }
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