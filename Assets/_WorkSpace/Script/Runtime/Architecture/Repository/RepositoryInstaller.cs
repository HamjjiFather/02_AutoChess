using System.Linq;
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
            var mscorlib = typeof(RepositoryInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IRepository))))
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