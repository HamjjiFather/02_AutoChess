namespace AutoChess
{
    public enum BehaviourRoleType
    {
        None,
        MeleeAttack = 1,
        RangeAttack,
        Assassin,
        Magician,
        SupporterType,
    }
    
    
    public enum JobType
    {
        None = 0,
        
        Guard = 100,
        Initiator,
        Viking,
        Crusher,
        HolyGuardian,
        BloodSucker,
        Babarian,
        DeathKnight,
        Charger,
        
        Shadow = 200,
        Assassin,
        Silencer,
        GuardBreaker,
        HawkEye,
        
        Summoner = 300,
        Wizard,
        SpellBreaker,
        Cleric,
        Dreamer,
        Druid,
        Undertaker,
        Commander,
        Medic,
        MilitaryBand,
        
        Ranger = 400,
        Hunter,
        Sniper,
        ShotGunner,
        PierceArrow,
        Mortar,
    }


    public enum MovementPriority
    {
        FrontOfOther,
        BackWardOfOther,
    }
    
    
    /// <summary>
    /// 역할 / 직업
    /// </summary>
    public class RoleBase
    {
        #region Fields & Property

        public BehaviourRoleType BehaviourRole;
        
        public ICharacterSkill SkillBase;

        public IAbilityContainer AbilityContainer;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}