﻿using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.Data;

namespace AutoChess.Bundle
{
    [Serializable]
    public class OfficeSkillBundleSet : IBundleSet
    {
        public int index;

        public int investmentPoint;
    }

    [Serializable]
    public class OfficeSkillBundle : BundleBase<OfficeSkillBundleSet>
    {
        public override Dictionary<int, OfficeSkillBundleSet> ToDictionaryLinq =>
            bundleSets.ToDictionary(x => x.index, x => x);
    }
}