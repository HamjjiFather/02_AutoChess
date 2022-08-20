using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UnityEngine;
using Random = System.Random;

namespace AutoChess
{
    public static class GameExtension
    {
        public static int FloatToInt (this float value)
        {
            return Mathf.RoundToInt (value);
        }

        public static bool ContainIndex<TSource> (this IEnumerable<TSource> source, int index)
        {
            return Enumerable.Range (0, source.Count ()).Contains (index);
        }


        public static TSource MinSource<TSource> (this IEnumerable<TSource> sources, Func<TSource, int> selector)
        {
            return sources.OrderBy (selector).First ();
        }


        public static TSource MinSource<TSource> (this IEnumerable<TSource> sources, Func<TSource, float> selector)
        {
            return sources.OrderBy (selector).First ();
        }

        public static IEnumerable<TSource> MinSources<TSource> (this IEnumerable<TSource> sources,
            Func<TSource, int> selector)
        {
            var minValue = sources.Min (selector);
            return sources.Where (x => selector.Invoke (x) == minValue);
        }

        public static IEnumerable<TSource> MinSources<TSource> (this IEnumerable<TSource> sources,
            Func<TSource, float> selector)
        {
            var minValue = sources.Min (selector);
            return sources.Where (x => Math.Abs (selector.Invoke (x) - minValue) < float.Epsilon);
        }


        public static TSource MaxSource<TSource> (this IEnumerable<TSource> sources, Func<TSource, int> selector)
        {
            return sources.OrderByDescending (selector).First ();
        }


        public static TSource MaxSource<TSource> (this IEnumerable<TSource> sources, Func<TSource, float> selector)
        {
            return sources.OrderByDescending (selector).First ();
        }


        public static IEnumerable<TSource> RandomSources<TSource> (this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentException (nameof (source));

            if (count >= source.Count ())
                return source;

            var sourceArray = source.ToArray ();
            var enumerable = Enumerable.Range (0, source.Count ()).ToList ();
            var returnSources = new List<TSource> ();
            for (var i = 0; i < count; i++)
            {
                var randValue = enumerable.Choice ();
                var result = sourceArray[randValue];
                enumerable.Remove (randValue);
                returnSources.Add (result);
            }

            return returnSources;
        }


        /// <summary>
        ///     enqueue many times.
        /// </summary>
        public static void Enqueues<T> (this Queue<T> queue, IEnumerable<T> enumerable)
        {
            enumerable.Foreach (queue.Enqueue);
        }


        /// <summary>
        ///     dequeue specific times.
        /// </summary>
        public static IEnumerable<T> Dequeues<T> (this Queue<T> queue, int count)
        {
            var newCount = Mathf.Min (count, queue.Count);
            if (newCount <= 0)
            {
                Debug.Log ("is Zero Count");
                return null;
            }

            var newQueue = new Queue<T> ();
            while (newCount > 0)
            {
                newQueue.Enqueue (queue.Dequeue ());
                newCount--;
            }

            return newQueue;
        }


        public static T Choice<T> (this IEnumerable<T> list)
        {
            var itemIdx = new Random ().Next (list.Count ());
            return list.ToList ()[itemIdx];
        }


        public static T Choice<T> (this IEnumerable<T> list, Func<T, bool> selector)
        {
            return list.ToArray ().Where (selector).Choice ();
        }


        public static void SetLocalReset (this Transform target)
        {
            target.localEulerAngles = Vector3.zero;
            target.position = Vector3.zero;
            target.localPosition = Vector3.one;
        }


        #region Int

        public static int RandomRange (this IEnumerable<int> bound)
        {
            return UnityEngine.Random.Range (bound.Min (), bound.Max ());
        }


        public static int CirculationRange(this int t, int min, int max)
        {
            var value = t > max ? min : t < min ? max : t;
            return value;
        }

        #endregion


        #region While

        public static void ForWhile (this int cycle, Action invokeAction)
        {
            var i = 0;
            while (i < cycle)
            {
                invokeAction.Invoke ();
                i++;
            }
        }


        public static void ForWhile (this int cycle, Action<int> invokeAction)
        {
            var i = 0;
            while (i < cycle)
            {
                invokeAction.Invoke (i);
                i++;
            }
        }

        #endregion


        #region Dictionary

        /// <summary>
        ///     If not a Key exists, add the Key Value to Dictionary.
        /// </summary>
        public static void ContainAndAdd<TK, TV> (this Dictionary<TK, TV> dict, TK key, TV value)
        {
            if (!dict.ContainsKey (key))
                dict.Add (key, value);
        }


        public static void AddRange<TK, TV> (this Dictionary<TK, TV> dict, Dictionary<TK, TV> targetDict)
        {
            targetDict.Foreach (target => { dict.ContainAndAdd (target.Key, target.Value); });
        }

        #endregion
    }
}