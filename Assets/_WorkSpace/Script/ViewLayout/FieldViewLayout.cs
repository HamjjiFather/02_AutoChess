using System;
using System.Linq;
using KKSFramework;
using KKSFramework.Navigation;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using EnumActionToDict = System.Collections.Generic.Dictionary<System.Enum, System.Action<AutoChess.FieldLandElement>>;

namespace AutoChess
{
    public class FieldViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public Text adventureCountText;

        public LineElement[] lineElements;

        public FieldCharacterElement fieldCharacterElement;

        public ScrollRect fieldScroll;

        public ScrollSnap snap;

#pragma warning disable CS0649

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

        private IDisposable _adventureCountDisposable;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _fieldTypeActionAfter.Add (FieldSpecialType.Battle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.BossBattle, _ => { Debug.Log ("전투가 시작되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverSmall, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverMedium, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.RecoverLarge, _ => { Debug.Log ("체력이 회복되었습니다."); });
            _fieldTypeActionAfter.Add (FieldSpecialType.Insightful, fieldLandElement =>
            {
                var insightFulPosition = _adventureViewmodel.GetInsightfulPosition ();
                var insightFulLand = GetLandElement (insightFulPosition.First());
                snap.SnapToAsync (insightFulLand.rectTransform).Forget();
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


        public void StartAdventure (AdventureModel adventureModel)
        {
            adventureModel.AllFieldModel.SelectMany (x => x.Value)
                .ZipForEach (lineElements.SelectMany (x => x.landElements),
                    (model, landElement) => { ((FieldLandElement) landElement).SetElement (model); });

            var characterModel = _characterViewmodel.BattleCharacterModels.First ();
            fieldCharacterElement.SetElement (characterModel);

            var startElement = GetLandElement (adventureModel.StartField.LandPosition);
            fieldCharacterElement.transform.SetParent (startElement.characterPositionTransform);
            fieldCharacterElement.transform.localPosition = Vector3.zero;

            snap.SnapTo (startElement.GetComponent<RectTransform> ());

            _adventureCountDisposable = _adventureViewmodel.AdventureModel.AdventureCount.Subscribe (count =>
            {
                adventureCountText.text = count.ToString ();
            });
        }


        public void EndAdventure ()
        {
            _adventureCountDisposable.DisposeSafe ();
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

            _adventureViewmodel.SetFieldType (fieldLandElement.FieldModel);

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

            _adventureViewmodel.SetFieldType (fieldLandElement.FieldModel);

            if (_fieldTypeActionAfter.ContainsKey (fieldType))
                _fieldTypeActionAfter[fieldType] (fieldLandElement);
        }


        public LandElement GetLandElement (PositionModel positionModel)
        {
            return lineElements[positionModel.Column].landElements[positionModel.Row];
        }


        public async UniTask ChangePosition (FieldLandElement fieldLandElement)
        {
            if (_isMoving)
                return;

            _isMoving = true;
            var result = _adventureViewmodel.FindMovingPositions (fieldLandElement.PositionModel);
            await fieldCharacterElement.MoveTo (result);
            Debug.Log ("Complete Movement");

            EventByFieldTypeAfterMoving (fieldLandElement);

            _isMoving = false;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}