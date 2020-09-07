using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.LocalData;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FormationPopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private LineElement[] _lineElements;

        [Resolver]
        private Image[] _characterImages;

        [Resolver ("_characterImages")]
        private CharacterDragEventElement[] _characterEventTrigger;

        [Resolver ("_characterImages")]
        private Animator[] _characterAnimators;

        [Resolver]
        private BattleCharacterListArea _battleCharacterListArea;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private bool _isCreated;

        private List<string> _positionStrings = new List<string> ();

        private GraphicRaycaster GraphicRaycaster => GetComponentInParent<GraphicRaycaster> ();

        private const string FormationTag = "Formation";

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            _characterEventTrigger.ForEach ((element, i) => element.SetElement (i, EndDragAction));
            _characterImages.ForEach (x => x.gameObject.SetActive (false));
        }

        #endregion


        #region Methods

        protected override async UniTask OnPrepareAsync (Parameters parameters)
        {
            if (!_isCreated)
            {
                await CreateField ();
                _lineElements.ForEach (x => x.MyVerticalLayoutGroup.SetLayoutVertical ());
                await UniTask.WaitForEndOfFrame ();
            }

            await base.OnPrepareAsync (parameters);

            // 팝업 생성시 한 번만.
            async UniTask CreateField ()
            {
                var fieldScale = Array.ConvertAll (Constant.BattleFieldScale.Split (','), int.Parse);
                var ableFields = Array.ConvertAll (Constant.FormationableFieldScale.Split (','), int.Parse);
                var fieldElement =
                    await ResourcesLoadHelper.LoadResourcesAsync<LandElement> (
                        $"{ResourceRoleType.Bundles}/{ResourcesType.Element}/{nameof (LandElement)}");

                _lineElements.ForEach ((element, i) =>
                {
                    var count = 0;
                    while (count < fieldScale[i])
                    {
                        var obj = fieldElement.InstantiateObject<LandElement> (element.transform);
                        var positionable = (count <= ableFields[i]);
                        obj.transform.SetLocalReset ();
                        obj.ManualResolve ();
                        obj.SetPosition (i, count);

                        if (positionable)
                        {
                            obj.gameObject.tag = FormationTag;
                            obj.HighlightedField.SetActive (true);
                        }

                        element.AddLandElement (obj);
                        count++;
                    }
                });

                _isCreated = true;
            }

            await base.OnPrepareAsync (parameters);
        }

        
        protected override void OnPush (Parameters parameters)
        {
            var battleCharacters = _characterViewmodel.BattleCharacterModels;
            _battleCharacterListArea.SetArea (battleCharacters);

            _positionStrings = LocalDataHelper.GetCharacterBundle ().BattleCharacterPositions.Split ('/').ToList ();
            battleCharacters.Where (x => x.IsAssigned).ForEach ((model, i) =>
            {
                _characterImages[i].sprite = model.IconImageResources;
                _characterAnimators[i].runtimeAnimatorController = model.CharacterAnimatorResources;

                var targetPosition = Array.ConvertAll (_positionStrings[i].Split (','), int.Parse);
                var land = _lineElements[targetPosition[0]].GetLandElement (targetPosition[1]);
                _characterImages[i].transform.position = land.transform.position;
                _characterImages[i].gameObject.SetActive (true);
            });
            base.OnPush (parameters);
        }



        protected override void OnPopComplete ()
        {
            _characterImages.ForEach (x => x.gameObject.SetActive (false));
            base.OnPopComplete ();
        }


        private void SavePosition ()
        {
            LocalDataHelper.SaveBattleCharacterPositionData (_positionStrings);
        }

        #endregion


        #region EventMethods

        private void EndDragAction (CharacterDragEventElement target, PointerEventData pointerEventData)
        {
            var result = new List<RaycastResult> ();
            GraphicRaycaster.Raycast (pointerEventData, result);

            if (result.Any ())
            {
                var landElement = result.FirstOrDefault (x => x.gameObject.tag.Equals (FormationTag));
                if (landElement.isValid)
                {
                    target.transform.position = landElement.gameObject.transform.position;
                    _positionStrings[target.MyIndex] =
                        landElement.gameObject.GetComponent<LandElement> ().PositionModel.ToString ();
                    SavePosition ();
                    return;
                }
            }

            target.ReturnToOriginPosition ();
        }

        #endregion
    }
}