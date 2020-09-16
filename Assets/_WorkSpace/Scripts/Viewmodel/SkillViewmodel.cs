using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using MasterData;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class SkillViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Inject]
        private StatusViewmodel _statusViewmodel;

        [Inject]
        private BattleViewmodel _battleViewmodel;

        [Inject]
        private CommonColorSetting _commonColorSetting;

#pragma warning restore CS0649

        private Dictionary<int, List<SkillModel>> _passiveSkills = new Dictionary<int, List<SkillModel>> ();

        #endregion


        public override void Initialize ()
        {
        }


        #region Methods

        public SkillModel InvokeSkill (CharacterModel user, BehaviourResultModel behaviourResultModel, int skillIndex, bool applyBullet)
        {
            var skillModel = new SkillModel
            {
                UseCharacterModel = user,
                SkillData = Skill.Manager.GetItemByIndex (skillIndex),
                ApplyBullet = applyBullet
            };

            skillModel.TargetCharacters.AddRange (
                behaviourResultModel.TargetBattleCharacterElements.Select (x => x.ElementData));

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
                var key = skillModel.UseCharacterModel.UniqueCharacterId;
                if (!_passiveSkills.ContainsKey (key))
                    _passiveSkills.Add (key, new List<SkillModel> ());

                _passiveSkills[key].Add (skillModel);
            }

            return isPassiveSkill;
        }


        /// <summary>
        /// 스킬 사용 가능 여부 확인.
        /// </summary>
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


        /// <summary>
        /// 스킬 처리.
        /// 직접 공격의 경우 바로 적용하고, 발사체의 경우 발사체를 생성한다.
        /// </summary>
        private void ProgressSkill (SkillModel skillModel)
        {
            CheckSkillValue (skillModel);

            // 발사체에서 스킬 적용.
            if (skillModel.ApplyBullet)
                return;
            
            ApplySkills (skillModel);
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


        /// <summary>
        /// 대상에게 스킬을 적용.
        /// </summary>
        public void ApplySkills (SkillModel skillModel)
        {
            for (var i = 0; i < skillModel.TargetCharacters.Count; i++)
            {
                var damageType = skillModel.SkillData.StatusChangeType == StatusChangeType.Increase
                    ? DamageType.Heal
                    : DamageType.Damage;
                skillModel.DamageType = damageType;
                skillModel.SkillValueModels[i].SetPositiveNegativeValue (damageType == DamageType.Heal);

                Debug.Log (
                    $"SkillIndex {skillModel.SkillData.Index}\nCount {skillModel.TargetCharacters.Count}/{i}\nSkill User {skillModel.UseCharacterModel}\nSkill Target {skillModel.TargetCharacters[i]}\nSkill Value {skillModel.SkillValueModels[i].PreApplyValue}");
                var element = _battleViewmodel.FindCharacterElement (skillModel.TargetCharacters[i]);
                element.ApplySkill (skillModel, skillModel.SkillValueModels[i]);
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}