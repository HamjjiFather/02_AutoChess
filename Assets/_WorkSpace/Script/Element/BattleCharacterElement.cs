using KKSFramework.Navigation;
using KKSFramework.Object;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class BattleCharacterElement : PooledObjectComponent, IElementBase<CharacterModel>
    {
        #region Fields & Property

        public GageElement hpGageElement;
        
        public Image characterImage;

        public Animator characterAnimator;


#pragma warning disable CS0649

#pragma warning restore CS0649
        
        public CharacterModel ElementData { get; set; }

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

            _maxHealth = (int)ElementData.GetTotalStatus (StatusType.Health);
            hpGageElement.SetValueOnlyGageValue (_maxHealth, _maxHealth);
        }

        
        public void StartBattle ()
        {
            ElementData.AddHealthEvent (SetHealth);
        }


        private void SetHealth (int hp)
        {
            hpGageElement.SetValueOnlyGageValue (hp, _maxHealth);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}