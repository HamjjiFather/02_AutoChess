using System.Collections.Generic;

namespace AutoChess.Repository
{
    public readonly struct OutpostBuildingDto
    {
        public OutpostBuildingDto(int outpostIndex, IEnumerable<int> outpostExtendIndexes)
        {
            OutpostIndex = outpostIndex.ToString();
            OutpostExtendIndexes = outpostExtendIndexes;
        }

        public readonly string OutpostIndex;

        public readonly IEnumerable<int> OutpostExtendIndexes;
    }
}