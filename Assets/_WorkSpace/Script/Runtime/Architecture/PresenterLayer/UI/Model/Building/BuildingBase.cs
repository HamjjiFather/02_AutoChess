namespace AutoChess
{
    public abstract class BuildingBase : IConstructor, ITimeBase
    {
        #region Fields & Property

        public Building BuildingTableData { get; set; }

        public int Level { get; set; }

        public double Exp { get; set; }

        private double RequireExp => RequireExps[Level];

        public double[] RequireExps { get; set; } = BuildingDefine.RequireExp;

        public int MaxLevel { get; set; } = BuildingDefine.MaxLevel;

        #endregion


        #region Methods

        #region Override

        public abstract void Build();

        public abstract void SpendTime();

        #endregion


        #region This

        protected abstract void OnLevelUp(int level);

        #endregion


        #region Event

        #endregion

        #endregion

        public bool VariExp(int expAmount)
        {
            Exp += expAmount;
            if (Exp < RequireExp)
                return false;

            Level += 1;
            OnLevelUp(Level);
            return true;
        }
    }
}