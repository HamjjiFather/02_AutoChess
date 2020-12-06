using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    [UsedImplicitly]
    public class GameSystemViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        public override void Initialize ()
        {

        }


        #region Methods
        
        public void Combine<T> (IEnumerable<ICombinable> combinables) where T : ICombinable
        {
            var target = combinables.First ();
            target.CombineMaterialModel.Combine ();
        } 

        #endregion


        #region EventMethods

        #endregion
    }
}