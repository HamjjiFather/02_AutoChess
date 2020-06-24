using System;
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


        public void StartBattle ()
        {
            battleSystem.PlayAnimationCallback (WaitAnimation);
            battleSystem.SetCharacterData (ElementData);
            battleSystem.StartBattle (SkillGageCallback);
            ElementData.AddHealthEvent (SetHealth);
        }


        private void SetHealth (int hp)
        {
            hpGageElement.SetValueOnlyGageValue (hp, _maxHealth);
        }


        private async UniTask WaitAnimation (BattleState state)
        {
            var animationName = AnimationNameByState ();
            // var c = characterAnimator.lay
            // var clipInfo = c.First (x => x.clip.name.Equals (animationName));
            characterAnimator.Play (animationName);
            await UniTask.Delay (TimeSpan.FromSeconds (0.5f));

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


        private void SkillGageCallback (float skillValue)
        {
            skillGageElement.SetSliderValue (skillValue);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}