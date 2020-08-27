using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.Navigation;
using UniRx;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action<AutoChess.FieldLandElement>>;

namespace AutoChess
{
    public class FieldViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private FieldViewLayoutRewardArea _fieldViewLayoutRewardArea;
        
        [Resolver]
        private LineElement[] _lineElements;

        public LineElement[] LineElements => _lineElements;

        [Resolver]
        private Text _adventureCountText;

        [Resolver]
        private Button _inspectButton;

        [Resolver]
        private ScrollSnap _scrollSnap;

        [Resolver]
        private ScrollRect _fieldScroll;

        [Resolver]
        private FieldCharacterElement _fieldCharacterElement;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private BattleCharacterListArea _battleCharacterListArea;

        private readonly EnumActionToDict _fieldTypeActionBefore = new EnumActionToDict ();

        private readonly EnumActionToDict _fieldTypeActionAfter = new EnumActionToDict ();

        private bool _isMoving;


        private List<IDisposable> _disposables = new List<IDisposable> ();

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _inspectButton.onClick.AddListener (ClickInspectButton);
            
            _fieldTypeActionAfter.Add (FieldSpecialType.Battle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.Reward, _ => { Debug.Log ("보상을 얻었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.BossBattle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverSmall, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverMedium, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverLarge, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.Insightful, fieldLandElement =>
            {
                var insightFulPosition = _adventureViewmodel.GetInsightfulPosition ();
                var insightFulLand = GetLandElement (insightFulPosition.First ());
                _scrollSnap.SnapToAsync (insightFulLand.transform).Forget ();
            });
            _fieldTypeActionAfter.Add (FieldSpecialType.Exit, _ =>
            {
                var msgPopupStruct = new MessagePopupStruct
                {
                    ConfirmAction = ExitAdventure,
                    Message = "?현재까지 얻은 보상을 획득하고 마을로 돌아가시겠습니까?"
                };
                NavigationHelper.OpenPopup (NavigationViewType.MessagePopup, msgPopupStruct).Forget();
            });
        }

        #endregion


        #region Methods

        public override void Initialize ()
        {
            ProjectContext.Instance.Container.BindInstance (this);
            _battleCharacterListArea = ProjectContext.Instance.Container.Resolve<BattleCharacterListArea> ();

            base.Initialize ();
        }


        public async UniTask StartAdventure ()
        {
            var fieldScale = Array.ConvertAll (Constant.FieldScale.Split (','), int.Parse);
            var adventureModel = await _adventureViewmodel.StartAdventure (fieldScale);
            Subscribe ();

            await CreateField ();
            adventureModel.AllFieldModel
                .SelectMany (x => x.Value)
                .ZipForEach (_lineElements.SelectMany (x => x.LandElements),
                    (model, landElement) => { ((FieldLandElement) landElement).Element (model); });
            
            _fieldViewLayoutRewardArea.SetArea (_adventureViewmodel.AdventureRewardModel);

            var characterModel = _characterViewmodel.BattleCharacterModels.First ();
            _fieldCharacterElement.SetElement (characterModel);
            var startElement = GetLandElement (adventureModel.StartField.LandPosition);
            _fieldCharacterElement.transform.position = startElement.transform.position;
            _scrollSnap.SnapTo (startElement.GetComponent<RectTransform> ());

            void Subscribe ()
            {
                _disposables.Add(_adventureViewmodel.EndAdventureCommand.Subscribe(_ =>
                {
                    EndAdventure ();
                }));
                _disposables.Add (_adventureViewmodel.AdventureModel.AdventureCount.Subscribe (count =>
                {
                    _adventureCountText.text = count.ToString ();
                }));
            }


            async UniTask CreateField ()
            {
                var fieldElement = await ResourcesLoadHelper.GetResourcesAsync<FieldLandElement> (
                    ResourceRoleType._Prefab, ResourcesType.Element, nameof (FieldLandElement));
                
                _lineElements.Foreach ((element, i) =>
                {
                    var count = fieldScale[i];
                    while (count > 0)
                    {
                        var obj = fieldElement.InstantiateObject<FieldLandElement> (element.transform);
                        obj.transform.SetInstantiateTransform ();
                        element.AddLandElement (obj);
                        count--;
                    }
                });
            }
        }


        public void EndAdventure ()
        {
            _disposables.Foreach (x => x.DisposeSafe ());
            _disposables.Clear ();
            Debug.Log ("end adventure");
        }


        public void ExitAdventure ()
        {
            _adventureViewmodel.EndAdventure ();
            Debug.Log ("exit adventure");
        }
        


        public void EndBattle (bool isWin)
        {
        }


        /// <summary>
        /// 이동 전 클릭 시 이벤트.
        /// </summary>
        public void EventFieldTypeBeforeMoving (FieldLandElement fieldLandElement)
        {
            var fieldType = fieldLandElement.FieldModel.FieldSpecialType.Value;
            if (fieldType == FieldSpecialType.None)
                return;

            _adventureViewmodel.FieldType (fieldLandElement.FieldModel);

            if (_fieldTypeActionBefore.ContainsKey (fieldType))
                _fieldTypeActionBefore[fieldType] (fieldLandElement);
        }


        /// <summary>
        /// 이동 후 클릭 시 이벤트.
        /// </summary>
        public void EventByFieldTypeAfterMoving (FieldLandElement fieldLandElement)
        {
            var fieldType = fieldLandElement.FieldModel.FieldSpecialType.Value;
            if (fieldType == FieldSpecialType.None)
                return;

            _adventureViewmodel.FieldType (fieldLandElement.FieldModel);

            if (_fieldTypeActionAfter.ContainsKey (fieldType))
                _fieldTypeActionAfter[fieldType] (fieldLandElement);
        }


        public LandElement GetLandElement (PositionModel positionModel)
        {
            return _lineElements[positionModel.Column].GetLandElement (positionModel.Row);
        }


        public async UniTask ChangePosition (FieldLandElement fieldLandElement)
        {
            if (_isMoving)
                return;

            _isMoving = true;
            var result = _adventureViewmodel.FindMovingPositions (fieldLandElement.PositionModel);
            await _fieldCharacterElement.MoveTo (result);
            Debug.Log ("Complete Movement");

            EventByFieldTypeAfterMoving (fieldLandElement);

            _isMoving = false;
        }

        #endregion


        #region EventMethods

        private void ClickInspectButton ()
        {
            var aroundPosition =
                PositionHelper.Instance.GetAroundPositionModel (_adventureViewmodel.AdventureModel.AllFieldModel,
                    _adventureViewmodel.NowPosition);
        }

        #endregion
    }
}