using UnityEngine;

namespace KKSFramework.ResourcesLoad
{
    /// <summary>
    /// 풀링으로 관리되는 오브젝트 베이스 클래스.
    /// </summary>
    public class PooingComponent : PrefabComponent
    {
        #region Fields & Property

        private string _poolingPath;

        #endregion


        #region Methods

        /// <summary>
        /// 오브젝트가 생성됨.
        /// </summary>
        public T Created<T> (string poolingPath) where T : PooingComponent
        {
            gameObject.SetActive (true);
            _poolingPath = poolingPath;
            OnCreated ();
            return GetComponent<T> ();
        }

        /// <summary>
        /// 오브젝트가 생성됨.
        /// </summary>
        public T Created<T> (Transform parents, string poolingPath) where T : PooingComponent
        {
            transform.SetParent (parents);
            return Created<T> (poolingPath);
        }

        /// <summary>
        /// 오브젝트가 파괴되지 않고 풀링 매니저에 등록됨.
        /// </summary>
        public virtual void Despawn ()
        {
            var myObj = gameObject;
            myObj.SetActive (false);
            ObjectPoolingHelper.Create (_poolingPath, myObj);
            OnPooling ();
        }

        /// <summary>
        /// 풀링에서 해제됨 (오브젝트 활성화).
        /// </summary>
        public void Spawn ()
        {
            gameObject.SetActive (true);
            OnUnpooled ();
        }

        /// <summary>
        /// 개체 생성시 실행할 함수.
        /// </summary>
        protected virtual void OnCreated ()
        {
            // Debug.Log ($"{nameof (PooingComponent)} : OnCreated");
        }

        /// <summary>
        /// 풀링될 때 실행 할 함수.
        /// </summary>
        protected virtual void OnPooling ()
        {
            // Debug.Log ($"{nameof (PooingComponent)} : OnPooling");
        }

        /// <summary>
        /// 풀링에서 해제될때 실행 할 함수.
        /// </summary>
        protected virtual void OnUnpooled ()
        {
            // Debug.Log ($"{nameof (PooingComponent)} : OnUnpooled");
        }

        #endregion
    }
}