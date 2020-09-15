using UnityEditor;
using UnityEngine;


namespace Helper
{
    public static class AppHelper
    {
        public static void Quit ()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        public static void Destroy (Object obj)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Object.Destroy(obj);
            else
                Object.DestroyImmediate(obj);
#else
                Object.Destroy (obj);
#endif
        }
    }
}