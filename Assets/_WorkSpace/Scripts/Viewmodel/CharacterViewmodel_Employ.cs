using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using MasterData;
using UniRx;

namespace AutoChess
{
    public partial class CharacterViewmodel
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 고용 가능한 캐릭터.
        /// </summary>
        public List<CharacterModel> AllEmployableCharacterModels { get; private set; } = new List<CharacterModel> ();

        /// <summary>
        /// 고용 가능한 캐릭터가 출현하는 수량.
        /// </summary>
        public IntReactiveProperty EmployableCharacterCount;

        /// <summary>
        /// 새로 고용 가능한지 여부.
        /// </summary>
        public bool IsNewEmployment { get; set; } = true;

        #endregion


        public void Initialize_Employ ()
        {
            EmployableCharacterCount = new IntReactiveProperty (3);
        }


        #region Methods

        public void SetNewEmployment ()
        {
            IsNewEmployment = true;
        }


        /// <summary>
        /// 고용 가능한 캐릭터를 새로 생성.
        /// </summary>
        public void NewEmployCharacterModels ()
        {
            if (!IsNewEmployment)
                return;

            AllEmployableCharacterModels.Clear ();

            EmployableCharacterCount.Value.ForWhile (() =>
            {
                var characterIndex = Character.Manager.Values.Choice ().Index;
                NewCharacter (characterIndex, AllEmployableCharacterModels);
            });
        }


        /// <summary>
        /// 캐릭터를 고용함.
        /// </summary>
        public void EmployCharacter (CharacterModel characterModel)
        {
            AllEmployableCharacterModels.Remove (characterModel);
            AllCharacterModels.Add (characterModel);
            EmployableCharacterCount.Value--;
            SaveCharacterData ();
            SaveCharacterStatusGradeData ();
            IsDataChanged = true;
        }


        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public int GetEmployPrice (CharacterModel characterModel)
        {
            var gradeValue = ((int) characterModel.StarGrade + 1) * 10;
            var statusValue = characterModel.StatusModel.Status.Values.Sum (x => x.GradeValue) / 10f;

            return gradeValue + (int)statusValue;
        }
        
        
        /// <summary>
        /// 등용 가격 책정.
        /// </summary>
        public string GetEmployPriceText (CharacterModel characterModel)
        {
            return $"{GetEmployPrice (characterModel)}G";
        }

        #endregion


        #region EventMethods

        #endregion
    }
}