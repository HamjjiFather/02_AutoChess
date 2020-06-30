using KKSFramework.DesignPattern;

namespace AutoChess
{
    public enum DamageType
    {
        Heal = 0,
        Damage = 1,
        CriticalHeal = 10,
        CriticalDamage = 11
    }
    
    public class BattleDamageModel : ModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        public DamageType DamageType;

        public int Amount;

        #endregion


        #region Methods

        public void SetModel (DamageType damageType, int amount)
        {
            DamageType = damageType;
            Amount = amount;
        }

        #endregion
    }
}