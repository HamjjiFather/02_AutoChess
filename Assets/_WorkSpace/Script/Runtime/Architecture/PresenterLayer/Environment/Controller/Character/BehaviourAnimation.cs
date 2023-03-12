using System.Collections;
using UnityEngine;

namespace AutoChess
{
    public class BehaviourAnimation : IAnimation
    {
        public BehaviourAnimation(Animator animator)
        {
            _animator = animator;
        }

        #region Fields & Property

        private Animator _animator;
        public Animator Animator
        {
            get => _animator;
            set => _animator = value;
        }

        private bool _waitForCallback;
        
        #endregion


        #region Methods

        #region Override

        public IEnumerator WaitForPlayAnimation(AnimationState state)
        {
            Animator.Play(state.ToString());
            _waitForCallback = true;
            yield return new WaitWhile(() => _waitForCallback);
        }

        #endregion


        #region This

        #endregion


        #region Event

        public void OnProcessPoint()
        {
            _waitForCallback = false;
        }
        
        #endregion

        #endregion
    }
}