using UnityEngine;

namespace AutoChess.Presenter
{
    public abstract class GameEnvironmentBase : MonoBehaviour
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This
        
        #endregion


        #region Event

        /// <summary>
        /// 환경이 활성화 됨.
        /// </summary>
        public abstract void OnEnvironmentEnabled(EnvironmentParameterBase environmentParameter);

        /// <summary>
        /// 환경이 비활성화 됨.
        /// </summary>
        public virtual void OnEnvironmentDisabled() { }

        #endregion

        #endregion
    }
}