using Cysharp.Threading.Tasks;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess.Presenter
{
    public readonly struct FieldActionMsg
    {
        public FieldActionMsg(FieldActionType fieldActionType)
        {
            FieldActionType = fieldActionType;
        }

        public readonly FieldActionType FieldActionType;
    }
    
    public class AdventurePage : PageViewBase
    {
        #region Fields & Property

        [Inject]
        private EnvironmentConverter _environmentConverter;

        [Inject]
        private BuildingManager _buildingManager;

        public FieldActionArea fieldActionArea;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnPush(object pushValue = null)
        {
            MessageBroker.Default.Receive<FieldActionMsg>().TakeUntilDestroy(this).Subscribe(fam =>
            {
                var param = new FieldActionArea.FieldActionAreaParameter(fam.FieldActionType, OnShowOutpostMenu);
                fieldActionArea.Show(param).Forget();
            });
            
            return base.OnPush(pushValue);
        }


        
        
        #endregion


        #region EventMethods

        private void OnShowOutpostMenu()
        {
            fieldActionArea.Hide().Forget();
        }

        #endregion
    }
}