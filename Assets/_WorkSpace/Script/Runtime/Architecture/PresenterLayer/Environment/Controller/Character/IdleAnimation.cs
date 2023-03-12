using System.Collections;
using UnityEngine;

namespace AutoChess
{
    public class IdleAnimation : IAnimation
    {
        public IdleAnimation(Animator animator)
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

        #endregion


        #region Methods

        #region Override

        public IEnumerator WaitForPlayAnimation(AnimationState state)
        {
            Animator.Play(state.ToString());
            yield break;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}