using System;
using AutoChess.Helper;
using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class BattleCharacterInfoElement : ElementBase<CharacterModel>
    {
        #region Fields & Property

        public StarGradeArea starGradeArea;
        
        public Image characterImage;

        public Text characterLevelText;

        public Text characterNameText;

        public GageElement expGageElement;

        public GageElement hpGageElement;

        public GageElement skillGageElement;

        public Button elementButton;

        public Transform inductSelectImage;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        public override CharacterModel ElementData { get; set; }

        private UnityEvent _clickInfoEvent = new UnityEvent ();

        private IDisposable _healthDisposable;

        private IDisposable _expDisposable;

        #endregion


        private void Awake ()
        {
            elementButton.onClick.AddListener (ClickElementButton);
        }


        #region Methods

        public override void SetElement (CharacterModel characterModel)
        {
            ElementData = characterModel;

            starGradeArea.SetArea (StarGrade.Grade1);
            
            characterNameText.GetTranslatedString (ElementData.CharacterData.Name);
            
            characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, characterModel.CharacterData.SpriteResName);

            var health = ElementData.GetTotalStatusValue (StatusType.Health).FloatToInt ();
            hpGageElement.SetValue (health, ElementData.StatusModel.MaxHealth.FloatToInt ());

            var valueReactive = new FloatReactiveProperty (health); 
            _healthDisposable = valueReactive.Subscribe (hp =>
            {
                var rectT = hpGageElement.GetComponent<RectTransform> ();
                rectT.sizeDelta = new Vector2 (Mathf.Clamp (hp, 0, 760), rectT.sizeDelta.y);
                hpGageElement.SetValueAsync (hp.FloatToInt (), hp.FloatToInt ());
            });

            _expDisposable = ElementData.Exp.Subscribe (exp =>
            {
                var level = TableDataHelper.Instance.GetCharacterLevelByExp (exp);
                var nowExp = (int) (exp - level.CoExp);

                expGageElement.SetValue (nowExp, (int) level.ReqExp);
                characterLevelText.text = $"Lv.{level.LevelString}";
            });
        }


        public void RegistActiveAction (Action<CharacterModel> unityAction)
        {
            _clickInfoEvent.AddListener (() =>
            {
                unityAction.Invoke (ElementData);
            });
            
            inductSelectImage.gameObject.SetActive (true);
        }


        /// <summary>
        /// 선택 이벤트 삭제.
        /// </summary>
        public void RemoveAllEvents ()
        {
            _clickInfoEvent?.RemoveAllListeners ();
            inductSelectImage.gameObject.SetActive (false);
        }
        
        #endregion


        private void ClickElementButton ()
        {
            _clickInfoEvent?.Invoke ();
        }
    }
}