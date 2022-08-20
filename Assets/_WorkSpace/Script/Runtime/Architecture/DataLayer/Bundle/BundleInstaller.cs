using System;
using System.Linq;
using KKSFramework.Base;

namespace KKSFramework.Data
{
    public class BundleInstaller : InstallerBase<IBundleBase>
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        protected override BindOption BindOption => BindOption.AsSingle;


        public override void PrepareInstaller()
        {
            var mscorlib = typeof(BundleInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(BundleBase))))
            {
                RegisterInstallItem(type);
            }
        }


        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}