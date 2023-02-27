using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class BuildingBundleSet : IBundleSet
    {
        public string index;

        public string Index
        {
            get => index;
            set => index = value;
        }

        public int level;
    }


    [Serializable]
    public class BaseBuildingBundle : BundleBase<BuildingBundleSet>
    {
        public BaseBuildingBundle()
        {
        }

        public BuildingBundleSet[] buildingSets;


        public override Dictionary<string, BuildingBundleSet> ToDictionaryLinq => bundleSets.ToDictionary(x => x.index);

        public override void Initialize()
        {
        }
    }
}