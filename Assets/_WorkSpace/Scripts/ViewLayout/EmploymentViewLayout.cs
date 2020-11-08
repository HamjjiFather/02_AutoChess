using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.InGame;
using KKSFramework.Navigation;
using MasterData;
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
        private CharacterInfoArea _characterInfoArea;

        [Resolver]
        private EmployableCharacterListArea _employableCharacterListArea;

        [Resolver]
        private Button _allEmployButton;

        [Resolver]
        private Property<string> _allEmployPriceText;

        [Resolver]
        private Property<Color> _allEmployTextColor;

        [Resolver]
        private Button _employButton;

        [Resolver]
        private Property<string> _employPriceText;
        
        [Resolver]
        private Property<Color> _employTextColor;

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

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void Initialize ()
        {
            base.Initialize ();
            _employButton.onClick.AddListener (ClickEmployButton);
            _allEmployButton.onClick.AddListener (ClickAllEmployButton);
        }


        public override UniTask ActiveLayout ()
        {
            _characterViewmodel.NewEmployCharacterModels ();
            _employableCharacterListArea.SetArea (ClickCharacterElement);
            UpdateLayout ();
            Subscribe ();
            return base.ActiveLayout ();

            void Subscribe ()
            {
                _itemViewmodel.CurrencyModels[CurrencyType.Gold].CurrencyAmount.Subscribe (SetEmployPriceText);
                SetEmployPriceText (_itemViewmodel.CurrencyModels[CurrencyType.Gold].CurrencyAmount.Value);

                void SetEmployPriceText (int amount)
                {
                    var allEnough = amount > _allEmployPrice;
                    _allEmployTextColor.Value = _colorSetting.GetPriceColor (allEnough);

                    var enough = amount > _employPrice;
                    _employTextColor.Value = _colorSetting.GetPriceColor (enough);
                }
            }
        }


        public void UpdateLayout ()
        {
            _employableCharacterListArea.UpdateArea ();

            var existEmployCharacter = _characterViewmodel.AllEmployableCharacterModels.Any ();
            if (!existEmployCharacter)
            {
                _allEmployPrice = 0;
                _employPrice = 0;
                _employPriceText.Value = $"{_employPrice}G";
            }
            else
            {
                _allEmployPrice =
                    _characterViewmodel.AllEmployableCharacterModels.Sum (model =>
                        _characterViewmodel.GetEmployPrice (model));
                ClickCharacterElement (_characterViewmodel.AllEmployableCharacterModels.First ());
            }
            
            _allEmployPriceText.Value = $"{_allEmployPrice}G";
        }

        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _characterInfoArea.SetArea (characterModel);
            _employPrice = _characterViewmodel.GetEmployPrice (characterModel);
            _employPriceText.Value = $"{_employPrice}G";
        }


        private void ClickAllEmployButton ()
        {
            WaitForMessagePopup ().Forget();
            
            async UniTask WaitForMessagePopup ()
            {
                var popupCode = await TreeNavigationHelper.WaitForPopPushPopup (nameof(MessagePopup));
                if (popupCode == PopupEndCode.Ok)
                {
                    
                }
            }
        }


        private void ClickEmployButton ()
        {
            WaitForMessagePopup ().Forget();
            
            async UniTask WaitForMessagePopup ()
            {
                var param = TreeNavigationHelper.SpawnParam ();
                param[MessagePopup.MessagePopupStringKey] = "고용하시겠습니까?";
                var popupCode = await TreeNavigationHelper.WaitForPopPushPopup (nameof(MessagePopup), param);
                
                if (popupCode == PopupEndCode.Ok)
                {
                    _characterViewmodel.EmployCharacter (_characterModel);
                    UpdateLayout ();
                }
            }
        }

        #endregion
    }
}