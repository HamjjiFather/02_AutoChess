using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;

namespace HexaPuzzle
{
    /// <summary>
    /// Column model data.
    /// </summary>
    public class PuzzleLineModel : ModelBase
    {
        /// <summary>
        /// land data. 
        /// </summary>
        public List<PuzzleLandModel> LandDatas = new List<PuzzleLandModel> ();


        public bool IsCreateLine;

        public int Count => LandDatas.Count;


        /// <summary>
        /// The row in which the puzzle is created.
        /// </summary>
        public int CreatePuzzleRow ()
        {
            var lastRow = LandDatas.LastOrDefault (x => x.landTypes == LandTypes.Show);
            return lastRow?.row ?? -1;
        }

        /// <summary>
        /// 해당 땅이 활성화 상태인지.
        /// </summary>
        public bool ContainRow (int row)
        {
            return Enumerable.Range (0, LandDatas.Count).Contains (row) && LandDatas[row].landTypes == LandTypes.Show;
        }

        /// <summary>
        /// 해당 땅이 있는지.
        /// </summary>
        public bool ExistRow (int row)
        {
            return Enumerable.Range (0, LandDatas.Count).Contains (row);
        }

        public IEnumerable<int> GetAvobeRows (int row)
        {
            var list = new List<int> ();

            row++;
            while (Enumerable.Range (0, LandDatas.Count).Contains (row))
            {
                if (LandDatas[row].landTypes == LandTypes.Show)
                {
                    list.Add (row);
                    row++;
                    continue;
                }

                break;
            }

            return list;
        }

        /// <summary>
        /// 오름차순으로 정렬하여 리턴.
        /// </summary>
        public IEnumerable<int> GetBelowRows (int row)
        {
            var list = new List<int> ();

            row--;
            while (Enumerable.Range (0, LandDatas.Count).Contains (row))
            {
                if (LandDatas[row].landTypes == LandTypes.Show)
                {
                    list.Add (row);
                    row--;
                    continue;
                }

                break;
            }

            return list.OrderBy (x => x);
        }
    }
}