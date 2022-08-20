namespace AutoChess
{
    public interface IConstructor
    {
        public Building BuildingTableData { get; set; }

        public int Level { get; set; }

        public bool Extendable { get; set; }

        void Build();

        void Extend();

        void Destruct();
    }
}