using System.Linq;
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
            var mscorlib = typeof(ViewModelInstaller).Assembly;
            foreach (var type in mscorlib.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IViewModel))))
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