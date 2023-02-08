namespace AutoChess
{
    public abstract class BuildingModelBase : IConstructor, ITimeBase
    {
        public delegate void OnLevelUpDelegate(int level);
        
        #region Fields & Property

        public Building BuildingTableData { get; set; }

        public int Level { get; set; } = BuildingDefine.StartLevel;

        public double Exp { get; set; }

        private double RequireExp => RequireExps[Level];

        public double[] RequireExps { get; set; } = BuildingDefine.RequireExp;

        public int MaxLevel { get; set; } = BuildingDefine.MaxLevel;

        /// <summary>
        /// 레벨업시 호출되는 이벤트.
        /// 레벨업 처리 후 호출된다.
        /// </summary>
        public event OnLevelUpDelegate OnLevelUpEvent;


        #endregion


        #region Methods

        #region Override

        public abstract void Build();

        public abstract void SpendTime();

        #endregion


        #region This

        protected virtual void OnLevelUp(int level)
        {
            OnLevelUpEvent?.Invoke(level);
        }


        /// <summary>
        /// 최대 레벨에 도달했는지?
        /// </summary>
        public bool IsMaxLevel => MaxLevel <= Level;
        
        
        /// <summary>
        /// 경험치 변동.
        /// </summary>
        public bool VariExp(int expAmount)
        {
            if (IsMaxLevel)
                return false;
            
            Exp += expAmount;
            if (Exp < RequireExp)
                return false;

            Level += 1;
            OnLevelUp(Level);
            return true;
        }

        #endregion


        #region Event

        #endregion

        #endregion

        
    }
}