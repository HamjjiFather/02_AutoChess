using AutoChess;
using KKSFramework.SceneLoad;
using UniRx.Async;
using UnityEngine.UI;
using Zenject;

namespace KKSFramework.Navigation
{
    public class TitlePageView : PageViewBase
    {
        #region Fields & Property

        public Button titleButton;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

#pragma warning restore CS0649

        private bool _isLoaded; 

        #endregion


        private void Awake ()
        {
            titleButton.onClick.AddListener (ClickTitle);
        }

        protected override async UniTask OnShow ()
        {
            await TableDataManager.Instance.LoadTableDatas ();
            await base.OnShow ();
        }

        protected override void Showed ()
        {
            base.Showed ();
            SetViewmodel ();
            _isLoaded = true;
        }

        private void SetViewmodel ()
        {
            _equipmentViewmodel.InitLocalData ();
            _characterViewmodel.InitLocalData ();
        }

        private void ClickTitle ()
        {
            if (!_isLoaded) return;
            _isLoaded = false;
            SceneLoadManager.Instance.ChangeSceneAsync (SceneType.Game).Forget ();
        }
    }
}