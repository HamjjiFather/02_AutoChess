using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using KKSFramework.SceneLoad;
using UnityEngine.UI;

namespace AutoChess.Presenter
{
    public class TitlePage : PageViewBase
    {
        #region Fields & Property

        public Button titleButton;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private bool _isLoaded; 

        #endregion


        private void Awake ()
        {
            titleButton.onClick.AddListener (ClickTitle);
        }

        protected override UniTask OnShow ()
        {
            _isLoaded = true;
            return base.OnShow ();
        }


        private void ClickTitle ()
        {
            if (!_isLoaded) return;
            _isLoaded = false;
            SceneLoadProjectManager.Instance.ChangeSceneAsync (SceneType.Game).Forget();
        }
    }
}