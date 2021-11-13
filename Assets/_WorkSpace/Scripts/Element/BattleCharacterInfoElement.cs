using System;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using MasterData;
using ResourcesLoad;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class BattleCharacterInfoElement : ElementBase<CharacterModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private StarGradeArea _starGradeArea;

        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private Property<string> _characterLevelText;

        [Resolver]
        private Property<string> _characterNameText;

        [Resolver]
        private GageElement _expGageElement;

        [Resolver]
        private GageElement _hpGageElement;
        
        public GageElement HpGageElement => _hpGageElement;

        [Resolver]
        private GageElement _skillGageElement;
        
        public GageElement SkillGageElement => _skillGageElement;

        [Resolver]
        private Button _elementButton;

        [Resolver]
        private GameObject _inductSelectImage;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        public bool IsAssigned => ElementData.IsAssigned;
        
        public override CharacterModel ElementData { get; set; }

        private UnityEvent _clickInfoEvent = new UnityEvent ();

        private IDisposable _healthDisposable;

        private IDisposable _expDisposable;

        #endregion


        private void Awake ()
        {
            _elementButton.onClick.AddListener (ClickElementButton);
        }


        #region Methods

        public override void SetElement (CharacterModel characterModel)
        {
            ElementData = characterModel;
            
            if (!ElementData.IsAssigned)
            {
                return;
            }

            _starGradeArea.SetArea (StarGrade.Grade1);

            _characterNameText.Value = LocalizeHelper.FromName (ElementData.CharacterData.Name);
            _characterImage.Value = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, characterModel.CharacterData.SpriteResName);

            var health = ElementData.GetTotalStatusValue (StatusType.Health).FloatToInt ();
            _hpGageElement.SetValue (health, ElementData.StatusModel.MaxHealth.FloatToInt ());

            var valueReactive = new FloatReactiveProperty (health);
            _healthDisposable = valueReactive.Subscribe (hp =>
            {
                var rectT = _hpGageElement.GetComponent<RectTransform> ();
                rectT.sizeDelta = new Vector2 (Mathf.Clamp (hp, 0, 760), rectT.sizeDelta.y);
                _hpGageElement.SetValueAsync (hp.FloatToInt (), hp.FloatToInt ());
            });

            _expDisposable = ElementData.Exp.Subscribe (exp =>
            {
                var level = TableDataHelper.Instance.GetCharacterLevelByExp (exp);
                var nowExp = (int) (exp - level.CoExp);

                _expGageElement.SetValue (nowExp, (int) level.ReqExp);
                _characterLevelText.Value = $"Lv.{level.LevelString}";
            });
        }


        public void RegistActiveAction (Action<CharacterModel> unityAction)
        {
            _clickInfoEvent.AddListener (() => { unityAction.Invoke (ElementData); });

            _inductSelectImage.SetActive (true);
        }


        /// <summary>
        /// 선택 이벤트 삭제.
        /// </summary>
        public void RemoveAllEvents ()
        {
            _clickInfoEvent?.RemoveAllListeners ();
            _inductSelectImage.SetActive (false);
        }

        #endregion


        private void ClickElementButton ()
        {
            _clickInfoEvent?.Invoke ();
        }
    }
}