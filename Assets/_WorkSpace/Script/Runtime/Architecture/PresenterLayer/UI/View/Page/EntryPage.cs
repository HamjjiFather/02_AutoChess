using System.Collections;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;

namespace AutoChess.Presenter
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
                SceneLoadProjectManager.Instance.LoadSceneAsync (SceneType.Title, true);
            }
        }
    }
}