using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DesignPattern;
using UniRx;

namespace HexaPuzzle
{
    public class SpecialPuzzleModel : ModelBase
    {
        #region Fields & Property

        public bool IsUnlock;
        
        public SpecialPuzzle SpecialPuzzleData;

        public readonly IntReactiveProperty Exp = new IntReactiveProperty ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion


        public SpecialPuzzleModel (bool isUnlock, SpecialPuzzle specialPuzzleData)
        {
            IsUnlock = isUnlock;
            SpecialPuzzleData = specialPuzzleData;
        }

        public void AddPuzzleExp (int amount)
        {
            Exp.Value += amount;
        }
    }
}