using UnityEngine;
using Zenject;

namespace HexaPuzzle
{
    public class StatusView : MonoBehaviour
    {
        #region Fields & Property

        public CurrencyElement[] currencyElements;

#pragma warning disable CS0649

        [Inject]
        private GameViewmodel _gameViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void InitializeStatusView ()
        {
            currencyElements.Foreach ((x, index) =>
            {
                x.SetCurrencyElemency (_gameViewmodel.CurrencyModels[index]);
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}