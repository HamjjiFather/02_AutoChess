using KKSFramework.Object;
using UniRx;
using UnityEngine.UI;
using Zenject;

namespace HexaPuzzle
{
    public class SpecialPuzzleElement : PrefabComponent
    {
        #region Fields & Property

        public Text puzzleNameText;
        
        public Text puzzleExpText;
        
        public Text puzzleValueText;

#pragma warning disable CS0649

        [Inject]
        private PuzzleViewmodel _puzzleViewmodel;

#pragma warning restore CS0649

        private SpecialPuzzleModel _specialPuzzleModel;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetData (SpecialPuzzleModel specialPuzzleModel)
        {
            _specialPuzzleModel = specialPuzzleModel;

            puzzleValueText.text = _specialPuzzleModel.SpecialPuzzleData.CheckValue.ToString (); 

            _specialPuzzleModel.Exp.Subscribe (x =>
            {
                var levelData = _puzzleViewmodel.GetLevelData (x);

                puzzleNameText.text = $"{_specialPuzzleModel.SpecialPuzzleData.PuzzleMatchingType} Lv.{levelData.LevelString}";
                puzzleExpText.text = $"{x - levelData.CoExp}/{levelData.ReqExp}";
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}