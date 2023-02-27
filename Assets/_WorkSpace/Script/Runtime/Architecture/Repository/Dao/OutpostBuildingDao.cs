using AutoChess.Bundle;

namespace AutoChess.Repository
{
    public readonly struct OutpostBuildingDao
    {
        public OutpostBuildingDao(OutpostBuildingBundleSet bundleSet, Outpost tableData)
        {
            Index = bundleSet.Index != null ? int.Parse(bundleSet.Index) : tableData.Id;
            AddOnTypes = bundleSet.addOnTypes;
            OutpostTableData = tableData;
        }

        public readonly int Index;

        public readonly OutpostAddOnTypes AddOnTypes;

        public readonly Outpost OutpostTableData;
    }
}