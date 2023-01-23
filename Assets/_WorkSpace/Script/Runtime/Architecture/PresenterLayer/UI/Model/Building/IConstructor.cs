using KKSFramework.GameSystem;

namespace AutoChess
{
    public interface IConstructor : ILevelBasedOnExp
    {
        public Building BuildingTableData { get; set; }

        void Build();
    }
}