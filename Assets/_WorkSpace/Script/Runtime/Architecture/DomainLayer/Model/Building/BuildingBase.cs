namespace AutoChess
{
    public abstract class BuildingBase : IConstructor
    {
        #region Fields & Property

        public Building BuildingTableData { get; set; }

        public int Level { get; set; }
        
        public abstract bool Extendable { get; set; }

        #endregion


        #region Methods

        #region Override
        
        public void Build()
        {
            // throw new System.NotImplementedException();
        }

        public void Extend()
        {
            // throw new System.NotImplementedException();
        }

        public void Destruct()
        {
            // throw new System.NotImplementedException();
        }

        #endregion


        #region This

        public abstract void OnExtendedBuilding();

        #endregion


        #region Event

        #endregion

        #endregion
    }
}