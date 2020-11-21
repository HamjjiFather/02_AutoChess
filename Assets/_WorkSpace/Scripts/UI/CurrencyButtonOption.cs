using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    [RequireComponent(typeof(Button))]
    public class CurrencyButtonOption : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

        public bool useConvertColor = true;
        
        public Color buttonTextEnableColor = Color.white;

        public Color buttonTextDisbleColor = Color.red;
        
        public Color currencyTextEnableColor = Color.white;

        public Color currencyTextDisbleColor = Color.red;

#pragma warning disable CS0649

        [Resolver]
        private Text _buttonText;

        [Resolver]
        private Text _currencyText;

#pragma warning restore CS0649

        private Button Button => GetComponent<Button> ();
        
        private int _requireAmount;
        
        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public void SetRequireCurrencyAmount (int reqAmount, string format = "{0}G", bool autoEnableAll = true)
        {
            _requireAmount = reqAmount;
            _currencyText.text = string.Format (format, reqAmount);
            
            if(autoEnableAll)
                EnableTexts ();
        }
        
        
        public bool CheckCurrency (int curCurrency)
        {
            var enough = _requireAmount <= curCurrency;
            Button.interactable = enough;
            
            if(!useConvertColor)
                return enough;

            _currencyText.color = enough ? currencyTextEnableColor : currencyTextDisbleColor;
            _buttonText.color = enough ? buttonTextEnableColor : buttonTextDisbleColor;
            return enough;
        }


        public void SetButtonText (string buttonText)
        {
            _buttonText.text = buttonText;
        }


        public void SetCurrencyAmountText (string currencyText)
        {
            _currencyText.text = currencyText;
        }


        #region Enables

        public void EnableTexts ()
        {
            SetActiveTexts (true, true);
        }

        
        public void DisableTexts ()
        {
            SetActiveTexts (false, false);
        }

        
        public void SetActiveTexts (bool enableButtonText, bool enableCurrencyText)
        {
            _buttonText.gameObject.SetActive (enableButtonText);
            _currencyText.gameObject.SetActive (enableCurrencyText);
        }
        
        #endregion


        #region AddListener

        public void AddListener (UnityAction unityAction)
        {
            Button.onClick.AddListener (unityAction);
        }

        #endregion
        
        
        #endregion


        #region EventMethods

        #endregion
    }
}