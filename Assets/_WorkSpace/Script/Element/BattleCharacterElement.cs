using System;
using System.Threading;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class BattleCharacterElement : PooledObjectComponent, IElementBase<CharacterModel>
    {
        #region Fields & Property

        public BattleSystem battleSystem;

        public GageElement hpGageElement;

        public GageElement skillGageElement;
        
        public Image characterImage;

        public Animator characterAnimator;

        public BattleCharacterInfoElement battleCharacterInfoElement;

#pragma warning disable CS0649

        [Inject]
        private BattleViewmodel _battleViewmodel;

#pragma warning restore CS0649

        public CharacterModel ElementData { get; set; }

        public BattleState BattleState => battleSystem.BattleState;

        private int _maxHealth;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetElement (CharacterModel characterModel)
        {
            ElementData = characterModel;

            characterImage.sprite = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Monster, ElementData.CharacterData.SpriteResName);

            characterAnimator.runtimeAnimatorController =
                ResourcesLoadHelper.GetResources<RuntimeAnimatorController> (ResourceRoleType._Animation,
                    characterModel.CharacterData.AnimatorResName);

            _maxHealth = (int) ElementData.GetTotalStatusValue (StatusType.Health);
            hpGageElement.SetValueOnlyGageValue (_maxHealth, _maxHealth);
        }

        public void SetInfoElement (BattleCharacterInfoElement infoElement)
        {
            battleCharacterInfoElement = infoElement;
        }


        public void StartBattle ()
        {
            battleSystem.SetCallbacks (SetHealth, WaitAnimation);
            battleSystem.SetCharacterData (ElementData);
            battleSystem.StartBattle (SkillGageCallback);
            
            // 체력 증감 처리.
            void SetHealth (int hp)
            {
                if (hp == 0)
                {
                    EndBattle ();
                }
                hpGageElement.SetValueOnlyGageValue (hp, _maxHealth);
                
                if(ElementData.CharacterSideType == CharacterSideType.Player)
                    battleCharacterInfoElement.hpGageElement.SetValue  (hp, _maxHealth);
            }
            
            // 애니메이션 실행 대기.
            async UniTask WaitAnimation (BattleState state, CancellationToken token)
            {
                var animationName = AnimationNameByState ();
                characterAnimator.Play (animationName);
                await UniTask.Delay (TimeSpan.FromSeconds (0.5f), cancellationToken: token);

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


        private void EndBattle ()
        {
            ElementData.EndBattle ();
            gameObject.SetActive (false);
        }


        private void SkillGageCallback (float skillValue)
        {
            skillGageElement.SetSliderValue (skillValue);
            if(ElementData.CharacterSideType == CharacterSideType.Player)
                battleCharacterInfoElement.skillGageElement.SetSliderValue  (skillValue);
        }
        
        
        public void ApplySkill (SkillModel skillModel, float skillValue)
        {
            battleSystem.ApplySkill (skillModel, skillValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}