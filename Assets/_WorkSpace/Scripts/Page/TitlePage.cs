using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = Helper.Scene;

namespace KKSFramework.InGame
{
    public class TitlePage : PageController
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

        protected override UniTask OnShowAsync ()
        {
            _isLoaded = true;
            return base.OnShowAsync ();
        }


        private void ClickTitle ()
        {
            if (!_isLoaded) return;
            _isLoaded = false;
            TreeNavigationHelper.ChangeScene (Scene.Game);
        }
    }
}