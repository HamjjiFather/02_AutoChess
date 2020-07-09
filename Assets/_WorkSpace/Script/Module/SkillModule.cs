using System;
using System.Collections.Generic;
using System.Linq;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class SkillModule : MonoBehaviour
    {
        #region Fields & Property

        public bool IsPassiveSkill => _passiveSkills.Count >= 1;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        private List<SkillModel> _passiveSkills = new List<SkillModel> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public SkillModel InvokeSkill (CharacterModel user, PositionModel positionModel, int skillIndex)
        {
            var skillModel = new SkillModel
            {
                UseCharacterModel = user,
                TargetPosition = positionModel,
                SkillData = TableDataManager.Instance.SkillDict[skillIndex],
            };

            CheckSkillStatus (skillModel);
            return skillModel;
        }


        public void InvokeAfterSkill (SkillModel nowSkillModel)
        {
            var skillModel = new SkillModel
            {
                UseCharacterModel = nowSkillModel.UseCharacterModel,
                TargetPosition = nowSkillModel.TargetPosition,
                SkillData = nowSkillModel.SkillData,
                SkillValueModels =
                    {new SkillValueModel (nowSkillModel.SkillValueModels.Select (x => x.AppliedValue).Sum ())}
            };

            CheckSkillStatus (skillModel);
        }


        public void CheckSkillStatus (SkillModel skillModel)
        {
            var isPassiveSkill = CheckPassiveSkill (skillModel);
            if (isPassiveSkill)
            {
                return;
            }

            var isInvokable = CheckCondition (skillModel);
            if (isInvokable)
            {
                ProgressSkill (skillModel);
            }
        }


        private bool CheckPassiveSkill (SkillModel skillModel)
        {
            var isPassiveSkill = skillModel.SkillData.SkillActiveCondition != SkillActiveCondition.OnActive;
            if (isPassiveSkill)
            {
                _passiveSkills.Add (skillModel);
            }

            return isPassiveSkill;
        }


        private bool CheckCondition (SkillModel skillModel)
        {
            switch (skillModel.SkillData.SkillActiveCondition)
            {
                // 스킬 게이지 충족시.
                case SkillActiveCondition.OnActive:
                    return true;

                default:
                    return false;
            }
        }


        private void ProgressSkill (SkillModel skillModel)
        {
            CheckSkillTarget (skillModel);
            CheckSkillValue (skillModel);
            ApplySkills (skillModel);
        }


        /// <summary>
        /// 스킬 대상을 체크함.
        /// </summary>
        private void CheckSkillTarget (SkillModel skillModel)
        {
            var skillTarget = skillModel.SkillData.SkillTarget;
            switch (skillModel.SkillData.SkillBound)
            {
                case SkillBound.Self:
                    skillModel.TargetCharacters.Add (skillModel.UseCharacterModel);
                    break;

                case SkillBound.Target:
                    var character = _battleViewmodel.FindClosestMonster (skillModel.UseCharacterModel.CharacterSideType,
                        skillModel.UseCharacterModel.PositionModel);
                    skillModel.TargetCharacters.Add (character);

                    break;

                case SkillBound.SelfArea:
                    var characterModels = _battleViewmodel.GetCharacterElementsAtNearby (
                        skillModel.UseCharacterModel.CharacterSideType, skillModel.UseCharacterModel.PositionModel,
                        skillTarget, true);
                    skillModel.TargetCharacters.AddRange (characterModels);

                    break;

                case SkillBound.TargetArea:
                    characterModels = _battleViewmodel.GetCharacterElementsAtNearby (
                        skillModel.UseCharacterModel.CharacterSideType, skillModel.TargetPosition, skillTarget, true);
                    skillModel.TargetCharacters.AddRange (characterModels);
                    break;

                case SkillBound.SelfAreaOnly:
                    characterModels = _battleViewmodel.GetCharacterElementsAtNearby (
                        skillModel.UseCharacterModel.CharacterSideType, skillModel.UseCharacterModel.PositionModel,
                        skillTarget);
                    skillModel.TargetCharacters.AddRange (characterModels);

                    break;

                case SkillBound.TargetAreaOnly:
                    characterModels = _battleViewmodel.GetCharacterElementsAtNearby (
                        skillModel.UseCharacterModel.CharacterSideType, skillModel.TargetPosition, skillTarget);
                    skillModel.TargetCharacters.AddRange (characterModels);
                    break;

                case SkillBound.All:
                    characterModels =
                        _battleViewmodel.GetCharacterModels (skillModel.UseCharacterModel.CharacterSideType,
                            skillTarget);
                    skillModel.TargetCharacters.AddRange (characterModels);
                    break;

                default:
                    throw new ArgumentOutOfRangeException ();
            }
        }


        /// <summary>
        /// 스킬 적용값.
        /// </summary>
        private void CheckSkillValue (SkillModel skillModel)
        {
            switch (skillModel.SkillData.RefSkillValueTarget)
            {
                case RefSkillValueTarget.Self:
                    var calcValue =
                        skillModel.UseCharacterModel.GetTotalStatusValue (skillModel.SkillData.RefSkillStatusType) *
                        skillModel.SkillData.RefSkillValueAmount;

                    skillModel.SkillValueModels.AddRange (Enumerable
                        .Repeat (calcValue, skillModel.TargetCharacters.Count)
                        .Select (value => new SkillValueModel (value)));
                    break;

                case RefSkillValueTarget.Target:
                    skillModel.SkillValueModels.AddRange (skillModel.TargetCharacters.Select (characterModel =>
                    {
                        calcValue = characterModel.GetTotalStatusValue (skillModel.SkillData.RefSkillStatusType) *
                                    skillModel.SkillData.RefSkillValueAmount;
                        return new SkillValueModel (calcValue);
                    }));
                    break;

                case RefSkillValueTarget.Damage:
                    break;
            }
        }


        private void ApplySkills (SkillModel skillModel)
        {
            for (var i = 0; i < skillModel.TargetCharacters.Count; i++)
            {
                var damageType = skillModel.SkillData.StatusChangeType == StatusChangeType.Increase
                    ? DamageType.Heal
                    : DamageType.Damage;
                skillModel.DamageType = damageType;
                skillModel.SkillValueModels[i].SetPositiveNegativeValue (damageType == DamageType.Heal);

                Debug.Log (
                    $"SkillIndex {skillModel.SkillData.Id}\nCount {skillModel.TargetCharacters.Count}/{i}\nSkill User {skillModel.UseCharacterModel}\nSkill Target {skillModel.TargetCharacters[i]}\nSkill Value {skillModel.SkillValueModels[i].PreApplyValue}");
                var element = _battleViewmodel.FindCharacterElement (skillModel.TargetCharacters[i]);
                element.ApplySkill (skillModel, skillModel.SkillValueModels[i]);
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}