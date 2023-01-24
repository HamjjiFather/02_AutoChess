using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class OfficeSkillBundleSet : IBundleSet
    {
        public string index = string.Empty;
        
        public string Index { get => index; set => index = value; }

        public int investmentPoint;
    }

    [Serializable]
    public class OfficeSkillBundle : BundleBase<OfficeSkillBundleSet>
    {
        public override Dictionary<string, OfficeSkillBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.index, x => x);
    }
}