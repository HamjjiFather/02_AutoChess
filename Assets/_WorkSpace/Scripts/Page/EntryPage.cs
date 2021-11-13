using System.Collections;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;

namespace KKSFramework.InGame
{
    public class EntryPage : PageViewBase
    {
        private const float WaitSeconds = 2f;

        protected override void Showed ()
        {
            StartCoroutine (WaitForSeconds ());
            base.Showed ();

            IEnumerator WaitForSeconds ()
            {
                yield return new WaitForSeconds (WaitSeconds);
                SceneLoadManager.Instance.LoadSceneAsync (SceneType.Title, true);
            }
        }
    }
}