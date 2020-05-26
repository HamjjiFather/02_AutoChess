using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DesignPattern;

namespace HexaPuzzle
{
    public class GameViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private float _resultValue;

        #endregion


        public override void Initialize ()
        {

        }


        #region Methods

        public void AddResult (float value)
        {
            _resultValue += value;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}