using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework;

namespace AutoChess
{
    /// <summary>
    /// 행동 처리 클래스.
    /// </summary>
    public abstract class BehaviourBase
    {
        #region Fields & Property

        /// <summary>
        /// 행동 전체에 대한 토큰 소스.
        /// </summary>
        private CancellationTokenSource _behaviourTokenSource;
        
        /// <summary>
        /// 현재 취하고 있는 행동에 대한 토큰 소스.
        /// </summary>
        private CancellationTokenSource _thisBehaviourTokenSource;
        
        #endregion


        #region Methods

        #region Override
        
        #endregion


        #region This

        /// <summary>
        /// 준비.
        /// </summary>
        public virtual void Ready()
        {
            _behaviourTokenSource = new CancellationTokenSource();
            _thisBehaviourTokenSource = new CancellationTokenSource();
        }
        
        /// <summary>
        /// 행동.
        /// </summary>
        /// <returns></returns>
        public abstract UniTask Execute();

        /// <summary>
        /// 종료.
        /// </summary>
        public virtual void Termination()
        {
            _thisBehaviourTokenSource.Cancel();
            _thisBehaviourTokenSource.DisposeSafe();
            _behaviourTokenSource.Cancel();
            _behaviourTokenSource.DisposeSafe();
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}