using KKSFramework.GameSystem;

namespace AutoChess
{
    public interface IConstructor : ILevelBasedOnExp
    {
        public Base BuildingTableData { get; set; }

        void Build();
    }
}