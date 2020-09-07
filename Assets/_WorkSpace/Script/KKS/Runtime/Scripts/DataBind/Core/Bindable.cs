using System.Linq;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class Bindable : MonoBehaviour
    {
        #region Fields & Property

        /// <summary>
        /// 구분 키 값.
        /// </summary>
        [SerializeField]
        protected string containerPath = string.Empty;

        public string ContainerPath => containerPath;

        /// <summary>
        /// 컨테이너.
        /// </summary>
        private Context _targetContext;

        public Context TargetContext
        {
            get
            {
                if (_targetContext != null) return _targetContext;
                var containerInParents = GetComponentsInParent<Context> (true).First (x => x.gameObject != gameObject);
                if (containerInParents == null)
                {
                    Debug.LogError ("there is no 'Context' component in parents object to bind");
                    return default;
                }

                _targetContext = containerInParents;

                return _targetContext;
            }
            private set => _targetContext = value;
        }

        public abstract object BindTarget { get; }

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Reset ()
        {
            containerPath = containerPath.Equals (string.Empty) ? gameObject.name : containerPath;
        }

        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        #region Methods

        public virtual void Dispose ()
        {
            TargetContext = null;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}