using KKSFramework;
using KKSFramework.DataBind;
using MasterData;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class StatusView : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private LobbyViewmodel _lobbyViewmodel;

        [Inject]
        private ItemViewmodel _itemViewmodel;

        [Resolver]
        private CurrencyElement[] _currencyElements;

        [Resolver]
        private Property<string> _playerLevelText;

        [Resolver]
        private Property<float> _playerExpGage;

        [Resolver]
        private Button _settingButton;

        [Resolver]
        private Button _backButton;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void InitializeStatusView (UnityAction backButtonAction)
        {
            _settingButton.onClick.AddListener (OnClickSettingButton);

            _currencyElements.Foreach ((x, index) =>
            {
                x.SetElement (_itemViewmodel.CurrencyModels[(CurrencyType) index]);
            });

            _playerLevelText.Value = _lobbyViewmodel.PlayerExpModel.PlayerLevelTable.LevelString;
            _playerExpGage.Value = _lobbyViewmodel.PlayerExpModel.ExpProportion;
            _lobbyViewmodel.ChangePlayerExpModel.Subscribe (expModel =>
            {
                _playerExpGage.Value = expModel.IsMaxLevel ? 1 : expModel.Exp / expModel.PlayerLevelTable.ReqExp;
                _playerLevelText.Value = expModel.PlayerLevelTable.LevelString;
            });

            _backButton.onClick.AddListener (backButtonAction);
        }


        public void ConvertButton (bool isSetting)
        {
            _settingButton.gameObject.SetActive (isSetting);
            _backButton.gameObject.SetActive (!isSetting);
        }

        #endregion


        #region EventMethods

        private void OnClickSettingButton ()
        {
        }

        #endregion
    }
}