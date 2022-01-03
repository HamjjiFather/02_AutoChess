using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class StatusElement : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

        public Image statusIconImage;


#pragma warning disable CS0649

        [Resolver]
        private Property<string> _statusNameText;

        [Resolver]
        private Property<string> _statusValueText;

        [Resolver]
        private Property<string> _statusGradeText;


#pragma warning restore CS0649

        #endregion


        #region Methods


        public void SetSubValueText (string subValueText)
        {
            _statusValueText.Value += subValueText;
        }

        #endregion
    }
}