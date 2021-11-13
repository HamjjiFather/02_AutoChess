using UniRx.Async;
using UnityEngine;

namespace AutoChess
{
    public class ViewLayoutBase : MonoBehaviour
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods


        public virtual void Initialize ()
        {
        }
        

        public virtual async UniTask ActiveLayout ()
        {
            gameObject.SetActive (true);
            await UniTask.CompletedTask;
        }


        public virtual async UniTask DisableLayout ()
        {
            gameObject.SetActive (false);
            await UniTask.CompletedTask;
        }
        
        #endregion


        #region EventMethods

        #endregion
    }
}