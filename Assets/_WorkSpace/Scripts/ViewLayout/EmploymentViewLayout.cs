using System.Linq;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class EmploymentViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private EmployCharacterInfoArea _employCharacterInfoArea;

        [Resolver]
        private EmployableCharacterListArea _employableCharacterListArea;

        [Resolver]
        private GameObject _buttonArea;

        [Resolver]
        private CurrencyButtonOption _employCurrencyButton;

        [Resolver]
        private CurrencyButtonOption _allEmployCurrencyButton;

        [Resolver]
        private Button _rejectButton;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private ItemViewmodel _itemViewmodel;

        [Inject]
        private CommonColorSetting _colorSetting;

#pragma warning restore CS0649

        private GamePage _gamePage;

        /// <summary>
        /// 현재 선택된 캐릭터 모델.
        /// </summary>
        private CharacterModel _characterModel;

        /// <summary>
        /// 모두 등용 가격.
        /// </summary>
        private int _allEmployPrice;

        /// <summary>
        /// 등용 가격.
        /// </summary>
        private int _employPrice;

        /// <summary>
        /// 모두 등용 가격 충족.
        /// </summary>
        private bool _enoughAllEmployPrice;

        /// <summary>
        /// 등용 가격 충족.
        /// </summary>
        private bool _enoughEmployPrice;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods


        protected override void OnInitialized ()
        {
            base.OnInitialized ();
            _employCurrencyButton.AddListener (ClickEmployButton);
            _allEmployCurrencyButton.AddListener (ClickAllEmployButton);
        }

        
        protected override UniTask OnActiveAsync (object parameters)
        {
            _characterViewmodel.SetNewEmployment ();
            _characterViewmodel.NewEmployCharacterModels ();
            _employableCharacterListArea.SetArea (ClickCharacterElement);
            UpdateLayout ();
            Subscribe ();
            return base.OnActiveAsync (parameters);

            void Subscribe ()
            {
                _itemViewmodel.CurrencyModels[CurrencyType.Gold].CurrencyAmount.Subscribe (SetEmployPriceText);
                SetEmployPriceText (_itemViewmodel.CurrencyModels[CurrencyType.Gold].CurrencyAmount.Value);

                void SetEmployPriceText (int amount)
                {
                    _employCurrencyButton.SetRequireCurrencyAmount (_employPrice);
                    _enoughEmployPrice = _employCurrencyButton.CheckCurrency (amount);
                    _allEmployCurrencyButton.SetRequireCurrencyAmount (_allEmployPrice);
                    _enoughAllEmployPrice = _allEmployCurrencyButton.CheckCurrency (amount);
                }
            }
        }


        public void UpdateLayout ()
        {
            var existEmployCharacter = _characterViewmodel.AllEmployableCharacterModels.Any ();
            _buttonArea.SetActive (existEmployCharacter);
            
            if (!existEmployCharacter)
            {
                _allEmployPrice = 0;
                _employPrice = 0;
                _employCurrencyButton.SetRequireCurrencyAmount (0);
                _employCharacterInfoArea.EmptyArea ();
                
            }
            else
            {
                _allEmployPrice =
                    _characterViewmodel.AllEmployableCharacterModels.Sum (model =>
                        _characterViewmodel.GetEmployPrice (model));
                ClickCharacterElement (_characterViewmodel.AllEmployableCharacterModels.First ());
            }
            
            _allEmployCurrencyButton.SetRequireCurrencyAmount (_allEmployPrice);
        }
        
        
        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _employCharacterInfoArea.SetArea (characterModel);
            _employPrice = _characterViewmodel.GetEmployPrice (characterModel);
            _employCurrencyButton.SetRequireCurrencyAmount (_employPrice);
        }


        private void ClickAllEmployButton ()
        {
            if(_enoughAllEmployPrice)
                WaitForMessagePopup ();
            else
                MessagePopup.PushNotEnoughPrice ();
            
            void WaitForMessagePopup ()
            {
                var format = LocalizeHelper.FromDescription ("DESC_0002");
                var msgPopupModel = new MessagePopup.Model
                {
                    msg = string.Format (format, _allEmployPrice),
                    confirmAction = Confirm
                };
                NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, msgPopupModel).Forget();
                
                void Confirm ()
                {
                    _characterViewmodel.EmployAll ();
                    UpdateLayout ();
                    _employableCharacterListArea.UpdateArea ();
                }
            }
        } 


        private void ClickEmployButton ()
        {
            if(_enoughEmployPrice)
                WaitForMessagePopup ();
            else
                MessagePopup.PushNotEnoughPrice ();
            
            void WaitForMessagePopup ()
            {
                var format = LocalizeHelper.FromDescription ("DESC_0003");
                var msgPopupModel = new MessagePopup.Model
                {
                    msg = string.Format (format, _employPrice),
                    confirmAction = Confirm
                };
                NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, msgPopupModel).Forget();
                
                void Confirm ()
                {
                    _characterViewmodel.EmployCharacter (_characterModel);
                    UpdateLayout ();
                    _employableCharacterListArea.UpdateArea ();
                }
            }
        }

        #endregion
    }
}