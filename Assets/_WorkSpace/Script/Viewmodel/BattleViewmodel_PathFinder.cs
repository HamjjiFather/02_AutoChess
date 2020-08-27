using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UnityEngine;

namespace AutoChess
{
    public partial class BattleViewmodel
    {
        #region Fields & Property

        private int[] _fieldScale;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private IEnumerable<CharacterModel> AllOfCharacterModels =>
            BattleAiCharacterModels.Concat (_characterViewmodel.BattleCharacterModels).ToList ();

        private IEnumerable<BattleCharacterElement> AllOfBattleCharacterElements =>
            PlayerCharacterElements.Concat (AiCharacterElements);

        private readonly Dictionary<int, List<LandModel>> _allLineModels = new Dictionary<int, List<LandModel>> ();

        #endregion


        #region Methods

        private void InitializeLines ()
        {
            _fieldScale = Array.ConvertAll (Constant.BattleFieldScale.Split (','), int.Parse);
            for (var c = 0; c < _fieldScale.Length; c++)
            {
                _allLineModels.Add (c, new List<LandModel> ());

                for (var r = 0; r < _fieldScale[c]; r++)
                {
                    _allLineModels[c].Add (new LandModel ());
                }
            }
        }


        public bool TryGetOtherCharacterAtPosition (CharacterSideType characterSideType, PositionModel positionModel,
            out BattleCharacterElement battleCharacterElement)
        {
            var foundCharacterElement =
                AllOfBattleCharacterElements.SingleOrDefault (x => x.ElementData.PositionModel.Equals (positionModel));
            if (foundCharacterElement is default (BattleCharacterElement) ||
                foundCharacterElement.ElementData.CharacterSideType.Equals (characterSideType))
            {
                battleCharacterElement = default;
                return false;
            }

            battleCharacterElement = foundCharacterElement;
            return true;
        }


        public List<BattleCharacterElement> GetAllCharacterElements (CharacterSideType sideType,
            SkillTarget skillTarget)
        {
            return sideType == CharacterSideType.Player && skillTarget == SkillTarget.Enemy ||
                   sideType == CharacterSideType.AI && skillTarget == SkillTarget.Ally
                ? AiCharacterElements
                : PlayerCharacterElements;
        }


        public List<BattleCharacterElement> GetAllOfOtherElements (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player ? AiCharacterElements : PlayerCharacterElements;
        }


        public List<BattleCharacterElement> GetAllOfEqualElements (CharacterSideType sideType)
        {
            return sideType == CharacterSideType.Player ? PlayerCharacterElements : AiCharacterElements;
        }


        /// <summary>
        /// 옆에 있는 캐릭터를 모두 리턴.
        /// </summary>
        public List<BattleCharacterElement> GetCharacterElementsAtNearby (CharacterSideType sideType,
            PositionModel positionModel, SkillTarget skillTarget, bool containSelf = false)
        {
            var nearPositions = PositionHelper.Instance.GetAroundPositionModel (_allLineModels, positionModel)
                .ToList ();
            var foundedCharacterElements = skillTarget == SkillTarget.Ally
                ? GetAllOfEqualElements (sideType)
                : GetAllOfOtherElements (sideType);

            if (containSelf)
                nearPositions.Add (positionModel);

            return foundedCharacterElements.Where (element =>
            {
                return nearPositions.Any (position => position.Equals (element.ElementData.PositionModel)) ||
                       nearPositions.Any (position => position.Equals (element.ElementData.PredicatedPositionModel));
            }).ToList ();
        }


        #region CheckBehaviour

        /// <summary>
        /// 행동 가능 여부 확인.
        /// </summary>
        public BehaviourResultModel CheckBehaviour (BattleCharacterElement element)
        {
            var resultModel = new BehaviourResultModel ();

            if (element.CanBehaviour)
            {
                if (element.IsFullSkillGage)
                {
                    if (TryFindSkillTarget (element, out var targetElements))
                    {
                        resultModel.SetResultState (BattleState.Behave);
                        resultModel.AddTargetCharacterElements (targetElements);
                        return resultModel;
                    }
                }

                if (element.ElementData.CharacterData.IsRangeAttack)
                {
                    if (TryFindNearestOtherMonster (element, out var targetElement))
                    {
                        resultModel.SetResultState (BattleState.Behave);
                        resultModel.AddTargetCharacterElements (new List<BattleCharacterElement> {targetElement});
                        return resultModel;
                    }
                }
                else
                {
                    if (TryFindNearbyOtherCharacterElement (element, out var targetElement))
                    {
                        resultModel.SetResultState (BattleState.Behave);
                        resultModel.AddTargetCharacterElements (new List<BattleCharacterElement> {targetElement});
                        return resultModel;
                    }
                }
            }

            // 이동.
            var movePositionResult = TryGetMovableAroundPosition (element, out var targetPosition);
            resultModel.SetResultState (movePositionResult ? BattleState.Moving : BattleState.Blocked);
            resultModel.SetTargetPosition (targetPosition);
            Debug.Log ($"{element.ElementData}'s CheckBehaviour Result : Move to {targetPosition}");
            return resultModel;
        }


        /// <summary>
        /// 목표까지 가장 빠르게 이동 가능한 주변 위치를 반환함 A star.
        /// </summary>
        private bool TryGetMovableAroundPosition (BattleCharacterElement battleCharacterElement,
            out PositionModel positionModel)
        {
            var characterModel = battleCharacterElement.ElementData;
            var myPosition = characterModel.PositionModel;

            var allOfCheckedPositions = new List<PositionModel> {myPosition};
            var findTargetModels = PositionHelper.Instance.GetAroundPositionModel (_allLineModels, myPosition)
                .Where (IsMovablePosition).Select ((x, i) => new BattleTargetResultModel (x)).ToList ();

            var checkedPosition = PositionHelper.Instance.EmptyPosition;
            var isCheckAround = CheckArounds ();

            // 적이 없거나 주변이 막힘.
            if (!isCheckAround)
            {
                positionModel = PositionHelper.Instance.EmptyPosition;
                return false;
            }

            positionModel = checkedPosition;
            return true;

            bool CheckArounds ()
            {
                if (findTargetModels.Count == 0)
                    return false;

                while (allOfCheckedPositions.Count < _fieldScale.Sum ())
                {
                    foreach (var findModel in findTargetModels)
                    {
                        var aroundPositions = findModel.AroundPositionModels.SelectMany (model =>
                            PositionHelper.Instance.GetAroundPositionModel (_allLineModels, model)).ToList ();

                        if (CheckPosition (aroundPositions))
                        {
                            checkedPosition = findModel.TargetResultPosition;
                            return true;
                        }

                        findModel.AroundPositionModels.Clear ();
                        allOfCheckedPositions.AddRange (aroundPositions);
                        allOfCheckedPositions = allOfCheckedPositions.Distinct ().ToList ();
                        findModel.AddRangeAroundPositions (aroundPositions);
                    }
                }

                return false;
            }

            bool CheckPosition (IEnumerable<PositionModel> checkPositions)
            {
                return checkPositions.Any (position =>
                    GetOtherCharacterAtPosition (characterModel.CharacterSideType, position));
            }

            bool GetOtherCharacterAtPosition (CharacterSideType characterSideType, PositionModel targetPositionModel)
            {
                var foundCharacter = GetAllOfOtherElements (characterSideType)
                    .Where (x => !x.ElementData.IsExcuted)
                    .SingleOrDefault (x => x.ElementData.PositionModel.Equals (targetPositionModel));

                return foundCharacter.NotNull ();
            }
        }

        #endregion


        #region SkillTarget

        #endregion


        #region Find

        /// <summary>
        /// 가장 가까운 대상 캐릭터를 찾음.
        /// </summary>
        public bool TryFindNearestMonster (BattleCharacterElement battleCharacterElement,
            out BattleCharacterElement targetElement)
        {
            var targetElements = GetAllCharacterElements (battleCharacterElement.ElementData.CharacterSideType,
                battleCharacterElement.ElementData.SkillData.SkillTarget);

            var foundModel = targetElements
                .MinSources (x => PositionHelper.Instance.Distance (_allLineModels,
                    battleCharacterElement.ElementData.PositionModel, x.ElementData.PositionModel)).First ();
            targetElement = foundModel;
            return foundModel.NotNull ();
        }


        /// <summary>
        /// 가장 가까운 대상 캐릭터를 찾음.
        /// </summary>
        public bool TryFindNearestOtherMonster (BattleCharacterElement battleCharacterElement,
            out BattleCharacterElement targetElement)
        {
            var targetElements =
                GetAllCharacterElements (battleCharacterElement.ElementData.CharacterSideType, SkillTarget.Enemy);

            var foundModel = targetElements
                .MinSources (x => PositionHelper.Instance.Distance (_allLineModels,
                    battleCharacterElement.ElementData.PositionModel, x.ElementData.PositionModel)).First ();
            targetElement = foundModel;
            return foundModel.NotNull ();
        }


        /// <summary>
        /// 옆에 있는 상대 캐릭터를 찾음. 
        /// </summary>
        public bool TryFindNearbyOtherCharacterElement (BattleCharacterElement battleCharacterElement,
            out BattleCharacterElement targetElement)
        {
            var allOfOtherElements =
                GetAllOfOtherElements (battleCharacterElement.ElementData.CharacterSideType);
            var aroundElements =
                GetCharacterElementsAtNearby (battleCharacterElement.ElementData.CharacterSideType,
                    battleCharacterElement.ElementData.PositionModel, SkillTarget.Enemy);
            var foundModel = aroundElements.Intersect (allOfOtherElements).FirstOrDefault ();
            targetElement = foundModel;
            return foundModel.NotNull ();
        }


        private bool TryFindSkillTarget (BattleCharacterElement characterElement,
            out List<BattleCharacterElement> targetElements)
        {
            var characterModel = characterElement.ElementData;
            var characterSide = characterModel.CharacterSideType;
            var characterPosition = characterModel.PositionModel;
            var skillData = characterElement.ElementData.SkillData;
            var results = new List<BattleCharacterElement> ();

            switch (skillData.SkillBound)
            {
                case SkillBound.Self:
                    results.Add (characterElement);
                    break;

                case SkillBound.Target:
                    if (TryFindNearestMonster (characterElement, out var element))
                    {
                        results.Add (element);
                    }

                    break;

                case SkillBound.SelfArea:
                    var elements = GetCharacterElementsAtNearby (
                        characterSide, characterPosition, skillData.SkillTarget, true);
                    results.AddRange (elements);
                    break;

                case SkillBound.TargetArea:
                    if (TryFindNearestMonster (characterElement, out element))
                    {
                        elements = GetCharacterElementsAtNearby (characterSide, element.ElementData.PositionModel,
                            skillData.SkillTarget, true);

                        results.AddRange (elements);
                    }

                    break;

                case SkillBound.SelfAreaOnly:
                    elements = GetCharacterElementsAtNearby (characterSide, characterPosition, skillData.SkillTarget);
                    results.AddRange (elements);

                    break;

                case SkillBound.TargetAreaOnly:
                    if (TryFindNearestMonster (characterElement, out element))
                    {
                        elements = GetCharacterElementsAtNearby (characterSide, element.ElementData.PositionModel,
                            skillData.SkillTarget);

                        results.AddRange (elements);
                    }

                    break;

                case SkillBound.All:
                    elements = GetAllCharacterElements (characterSide, skillData.SkillTarget);
                    results.AddRange (elements);
                    break;

                case SkillBound.FanShape:
                    break;

                default:
                    goto case SkillBound.Target;
            }

            targetElements = results;
            return targetElements.Any ();
        }


        public bool IsMovablePosition (PositionModel positionModel)
        {
            return _allLineModels.ContainsKey (positionModel.Column) &&
                   _allLineModels[positionModel.Column].ContainIndex (positionModel.Row) &&
                   !positionModel.Equals (PositionHelper.Instance.EmptyPosition) &&
                   AllOfCharacterModels.All (x => !x.PositionModel.Equals (positionModel)) &&
                   AllOfCharacterModels.All (x => !x.PredicatedPositionModel.Equals (positionModel));
        }


        public void CompleteMovement (CharacterModel characterModel, PositionModel positionModel)
        {
            characterModel.PositionModel.Set (positionModel.Column, positionModel.Row);
            characterModel.RemovePredicatePosition ();
        }

        #endregion

        #endregion


        #region EventMethods

        #endregion
    }
}