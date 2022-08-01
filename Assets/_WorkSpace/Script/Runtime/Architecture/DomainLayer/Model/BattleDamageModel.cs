namespace AutoChess
{
    public enum DamageType
    {
        Heal = 0,
        Damage = 1,
        CriticalHeal = 10,
        CriticalDamage = 11
    }
    
    public class BattleDamageModel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        public DamageType DamageType;

        public int Amount;

        #endregion


        #region Methods

        #endregion
    }
}