using System;
using System.Threading;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    [Serializable]
    public class BattleCharacterPackage
    {
        public BattleCharacterElement battleCharacterElement;
        
        public CharacterAppearanceModule characterAppearanceModule;

        public CharacterParticleModule characterParticleModule;
        
        public BattleSystemModule battleSystemModule;

        public void InitPackage ()
        {
            battleSystemModule.InitModule (this);
        }
    }
    
    public class BattleCharacterElement : PooledObjectComponent, IElementBase<CharacterModel>
    {
        #region Fields & Property

        public BattleCharacterPackage battleCharacterPackage;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        /// <summary>
        /// 캐릭터 데이터.
        /// </summary>
        public CharacterModel ElementData { get; set; }

        /// <summary>
        /// 전투 상태.
        /// </summary>
        public BattleState BattleState => battleCharacterPackage.battleSystemModule.BattleState;

        /// <summary>
        /// 최대 체력.
        /// </summary>
        private int _maxHealth;

        /// <summary>
        /// 캐릭터 정보 엘리먼트.
        /// </summary>
        private BattleCharacterInfoElement _battleCharacterInfoElement;
        
        public bool CanBehaviour => battleCharacterPackage.battleSystemModule.behaviourSystemModule.CanBehaviour;

        public bool IsFullSkillGage => battleCharacterPackage.battleSystemModule.behaviourSystemModule.IsFullSkillGage;
        

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterModel characterModel)
        {
            ElementData = characterModel;

            battleCharacterPackage.characterAppearanceModule.SetActive (true);

            var sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, ElementData.CharacterData.SpriteResName);
            var aniamtorController = ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (
                ResourceRoleType._Animation, ElementData.CharacterData.AnimatorResName);
            battleCharacterPackage.characterAppearanceModule.SetSprite (sprite);
            battleCharacterPackage.characterAppearanceModule.SetRuntimeAnimatorContoller (aniamtorController);

            _maxHealth = ElementData.GetTotalStatusValue (StatusType.Health).FloatToInt ();
            battleCharacterPackage.characterAppearanceModule.SetValueOnlyHealthGageValue (_maxHealth, _maxHealth);
            battleCharacterPackage.characterAppearanceModule.SetHealthGageColor (ElementData.CharacterSideType);
            SkillGageCallback (0);
        }

        public void SetInfoElement (BattleCharacterInfoElement infoElement)
        {
            _battleCharacterInfoElement = infoElement;
        }


        public void StartBattle ()
        {
            battleCharacterPackage.battleSystemModule.SetCallbacks (SetHealth, WaitAnimation);
            battleCharacterPackage.battleSystemModule.StartBattle (SkillGageCallback);

            // 체력 증감 처리.
            void SetHealth (int hp)
            {
                if (hp == 0)
                {
                    EndBattle ();
                    _battleViewmodel.CheckCharacters (ElementData.CharacterSideType);
                    battleCharacterPackage.characterParticleModule.PlayParticle (CharacterBuiltInParticleType.Death);
                }

                battleCharacterPackage.characterAppearanceModule.SetValueOnlyHealthGageValue(hp, _maxHealth);

                if (ElementData.CharacterSideType == CharacterSideType.Player)
                    _battleCharacterInfoElement.hpGageElement.SetValueAsync (hp, _maxHealth);
            }

            // 애니메이션 실행 대기.
            async UniTask WaitAnimation (BattleState state, CancellationToken token)
            {
                var animationName = AnimationNameByState ();
                await battleCharacterPackage.characterAppearanceModule.PlayAnimation (animationName, token);

                string AnimationNameByState ()
                {
                    switch (state)
                    {
                        case BattleState.Moving:
                            return "Move";

                        case BattleState.Behave:
                            return "Attack";

                        default:
                            return "Idle";
                    }
                }
            }
        }


        /// <summary>
        /// 전투 종료.
        /// </summary>
        public void EndBattle ()
        {
            ElementData.EndBattle ();
            battleCharacterPackage.battleSystemModule.EndBattle ();
            battleCharacterPackage.characterAppearanceModule.SetActive (false);
        }


        /// <summary>
        /// 스킬 게이지.
        /// </summary>
        private void SkillGageCallback (float skillValue)
        {
            var sliderValue = skillValue / Constant.MaxSkillGageValue;
            battleCharacterPackage.characterAppearanceModule.SetSkillSliderValue (sliderValue);
            
            if (ElementData.CharacterSideType == CharacterSideType.Player)
                _battleCharacterInfoElement.skillGageElement.SetSliderValue (sliderValue);
        }


        /// <summary>
        /// 스킬 적용.
        /// </summary>
        public void ApplySkill (SkillModel skillModel, SkillValueModel skillValueModel)
        {
            battleCharacterPackage.battleSystemModule.ApplySkill (skillModel, skillValueModel);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}