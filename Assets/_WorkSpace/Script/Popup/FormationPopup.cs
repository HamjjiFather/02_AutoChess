using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using Helper;
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
        private Transform _characterParents;

        [Resolver]
        private BattleCharacterListArea _battleCharacterListArea;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private List<string> _positionStrings = new List<string> ();

        private GraphicRaycaster GraphicRaycaster => GetComponentInParent<GraphicRaycaster> ();

        private const string FormationTag = "Formation";

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override async UniTask OnPrepareAsync (Parameters parameters)
        {
            await CreateField ();
            await UniTask.WaitForEndOfFrame ();
            await CharacterSpawn ();
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
                
                _lineElements.ForEach (x => x.MyVerticalLayoutGroup.SetLayoutVertical ());

            }

            await base.OnPrepareAsync (parameters);
        }


        private async UniTask CharacterSpawn ()
        {
            var formCharacter = await ResourcesLoadHelper.LoadResourcesAsync<FormationCharacterElement> (
                ResourceRoleType.Bundles,
                ResourcesType.Element, "FormationCharacterElement");

            var battleCharacters = _characterViewmodel.BattleCharacterModels;
            _battleCharacterListArea.SetArea (battleCharacters);

            _positionStrings = LocalDataHelper.GetCharacterBundle ().BattleCharacterPositions.Split ('/').ToList ();
            battleCharacters.Where (x => x.IsAssigned).ForEach ((x, i) =>
            {
                var formationCharacterElement = Instantiate (formCharacter);
                formationCharacterElement.transform.SetParentLocalReset (_characterParents);
                formationCharacterElement.SetElement (x);
                formationCharacterElement.SetElement (i, EndDragAction);

                var targetPosition = Array.ConvertAll (_positionStrings[i].Split (','), int.Parse);
                var land = _lineElements[targetPosition[0]].GetLandElement (targetPosition[1]);
                formationCharacterElement.transform.position = land.transform.position;
            });
        }


        private void SavePosition ()
        {
            LocalDataHelper.SaveBattleCharacterPositionData (_positionStrings);
        }

        #endregion


        #region EventMethods

        private void EndDragAction (FormationCharacterElement target, PointerEventData pointerEventData)
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