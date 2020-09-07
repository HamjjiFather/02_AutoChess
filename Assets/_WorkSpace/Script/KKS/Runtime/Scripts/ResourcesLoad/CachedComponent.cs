using System;
using System.Collections.Generic;
using UnityEngine;

namespace KKSFramework
{
    /// <summary>
    /// 캐시 컴포넌트 클래스.
    /// 2018.06.03. 작성.
    /// </summary>
    public class CachedComponent : MonoBehaviour
    {
        #region Fields & Property

        /// <summary>
        /// 이 오브젝트에 캐시된 컴포넌트 딕셔너리.
        /// </summary>
        private Dictionary<Type, Component> _cachedComponentDict;

        public Dictionary<Type, Component> CachedComponentDict
        {
            get
            {
                if (_cachedComponentDict == null)
                {
                    _cachedComponentDict = new Dictionary<Type, Component> ();
                }

                return _cachedComponentDict;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// 캐시된 컴포넌트 리턴.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Component GetCachedComponent (Type type)
        {
            CachedComponentDict.TryGetValue (type, out var tempComponent);
            return tempComponent ? tempComponent : CachedComponentDict[type] = GetComponent (type);
        }

        /// <summary>
        /// 컴포넌트를 추가하고 캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Component AddCachedComponent (Type type)
        {
            var tempComponent = gameObject.AddComponent (type);
            CachedComponentDict.Add (type, tempComponent);
            return tempComponent ? tempComponent : CachedComponentDict[type] = GetComponent (type);
        }

        /// <summary>
        /// 캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCachedComponent<T> () where T : Component
        {
            return (T) GetCachedComponent (typeof (T));
        }

        /// <summary>
        /// 컴포넌트를 추가하고 캐시된 제네릭 컴포넌트 리턴.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddCachedComponent<T> () where T : Component
        {
            return (T) AddCachedComponent (typeof (T));
        }

        #endregion
    }
}