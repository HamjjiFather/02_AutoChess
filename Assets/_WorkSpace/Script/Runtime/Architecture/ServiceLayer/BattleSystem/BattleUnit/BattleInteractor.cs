using UnityEngine;

namespace AutoChess.Service
{
    public class HealthContainer
    {
        public HealthContainer(int maxHealth)
        {
            MaxHealth = maxHealth;
            NowHealth = MaxHealth;
        }

        public int MaxHealth { get; private set; }

        public int NowHealth { get; private set; }

        public float NowHealthRatio => Mathf.Clamp((float)NowHealth / MaxHealth, 0, 1);

        public float LoseHealthRatio => 1 - NowHealthRatio;

        public float MaxHealthPerValue(int percentValue) => MaxHealth * (float)percentValue / Constant.BasePercentValue;

        public int VariateHealth(int health)
        {
            NowHealth += health;
            return NowHealth;
        }
    }
    
    public class BattleInteractor
    {
        public BattleInteractor(CharacterUnit characterUnit)
        {
            CharacterUnit = characterUnit;
            HealthContainer = new HealthContainer(CharacterUnit.GetAbilityValue(SubAbilities.Health).FloatToInt());
        }

        #region Fields & Property

        public CharacterUnit CharacterUnit;

        public HealthContainer HealthContainer;
        
        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void OnHit()
        {
            
        }


        public void OnBeat()
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}