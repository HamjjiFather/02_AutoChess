using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class GameManagerBase : IInitialize
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override
        
        public virtual void Initialize ()
        {
            // throw new System.NotImplementedException ();
        }

        public virtual void InitAfterLoadTableData ()
        {
            // throw new System.NotImplementedException ();
        }

        public virtual void InitAfterLoadLocalData ()
        {
            // throw new System.NotImplementedException ();
        }

        public virtual void InitFinally ()
        {
            // throw new System.NotImplementedException ();
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}