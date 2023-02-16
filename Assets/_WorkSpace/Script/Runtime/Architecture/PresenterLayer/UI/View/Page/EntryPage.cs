using System.Collections;
using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine;

namespace AutoChess.Presenter
{
    public class EntryPage : PageViewBase
    {
        private const float WaitSeconds = 2f;


        protected override async UniTask OnShow(object pushValue = null)
        {
            await base.OnShow(pushValue);
            StartCoroutine (WaitForSeconds ());

            IEnumerator WaitForSeconds ()
            {
                yield return new WaitForSeconds (WaitSeconds);
                SceneLoadProjectManager.Instance.LoadSceneAsync (SceneType.Title, true).Forget();
            }
        }
    }
}