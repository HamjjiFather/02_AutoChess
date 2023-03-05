using AutoChess.Bundle;

namespace AutoChess.Repository
{
    public readonly struct OutpostBuildingDao
    {
        public OutpostBuildingDao(OutpostBuildingBundleSet bundleSet, Outpost tableData)
        {
            Index = bundleSet.Index != null ? int.Parse(bundleSet.Index) : tableData.Id;
            HasBuilt = bundleSet.hasBuilt;
            ExtendBuildings = bundleSet.extendBuildings;
            OutpostTableData = tableData;
        }

        public readonly int Index;

        public readonly bool HasBuilt;

        public readonly int[] ExtendBuildings;

        public readonly Outpost OutpostTableData;
    }
}