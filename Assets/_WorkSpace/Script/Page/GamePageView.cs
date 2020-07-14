using System.Linq;
using AutoChess;
using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine.UI;
using Zenject;

namespace KKSFramework
{
    public class GamePageView : PageViewBase
    {
        #region Fields & Property
        
        public ViewLayoutBase[] subViewObjs;

        public Button[] buttons;
        
        public StatusView statusView;
        
        public BattleCharacterListArea battleCharacterListArea;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            ProjectContext.Instance.Container.BindInstance (battleCharacterListArea);
            battleCharacterListArea.SetArea (_characterViewmodel.BattleCharacterModels);
            
            buttons.Foreach ((button, index) =>
            {
                button.onClick.AddListener (() => SetSubView(index));
            });
            
            subViewObjs.Foreach (x =>
            {
                x.Initialize ();
                x.gameObject.SetActive (false);
            });
            
        }

        #endregion


        #region Methods

        protected override UniTask OnPush (object pushValue = null)
        {
            statusView.InitializeStatusView ();
            SetSubView (0);
            
            return base.OnPush (pushValue);
        }


        public void SetSubView (int index)
        {
            subViewObjs.Where (x => x.gameObject.activeSelf).Foreach (x =>
            {
                x.DisableLayout ().Forget();
            });
            
            subViewObjs[index].ActiveLayout ().Forget();
        }


        #endregion


        #region EventMethods
        

        #endregion
    }
}