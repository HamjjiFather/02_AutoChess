using System.Linq;
using KKSFramework.Base;

namespace KKSFramework.Presenter
{
    public class EnvironmentManagerInstaller : InstallerBase<IEnvironmentManager>
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
            var mscorlib = typeof(EnvironmentManagerInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IEnvironmentManager))))
            {
                RegisterInstallItem(type);
            }
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}