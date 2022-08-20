using System.Linq;
using AutoChess;
using KKSFramework.Base;

namespace KKSFramework.Repository
{
    public class ManagerBaseInstaller : InstallerBase<ManagerBase>
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
            var mscorlib = typeof(ManagerBaseInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ManagerBase))))
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