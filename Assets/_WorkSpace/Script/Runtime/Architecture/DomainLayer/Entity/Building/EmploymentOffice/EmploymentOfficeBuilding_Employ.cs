using System.Collections.Generic;
using System.Linq;
using AutoChess.Presenter;
using KKSFramework;

namespace AutoChess
{
    public partial class EmploymentOfficeBuilding
    {
        public class EmployCharacterSlotModel
        {
            public CharacterModelBase EmployableCharacter;
        }


        #region Fields & Property

        /// <summary>
        /// 고용 가능한 캐릭터 수량.
        /// </summary>
        public int EmployableCharacterAmount
        {
            get
            {
                return Level switch
                {
                    1 and <= 3 => 4,
                    4 => 5,
                    <= BuildingDefine.MaxLevel => 6,
                    _ => 3
                };
            }
        }


        public (int, int) EmployableCharacterLevelBound
        {
            get
            {
                return Level switch
                {
                    1 and <= 3 => (0, 6),
                    4 => (6, 12),
                    <= BuildingDefine.MaxLevel => (10, 20),
                    _ => (0, 0)
                };
            }
        }

        /// <summary>
        /// 고용 캐릭터 모델.
        /// </summary>
        public Dictionary<int, EmployCharacterSlotModel> EmployableCharacterModels;

        #endregion


        #region Methods

        #region Override

        public void Initialize_Employ()
        {
            EmployableCharacterModels = new Dictionary<int, EmployCharacterSlotModel>(EmployableCharacterAmount);
            EmployableCharacterModels.AddRange(Enumerable.Range(0, EmployableCharacterAmount)
                .ToDictionary(i => i, _ => new EmployCharacterSlotModel()));
        }

        public void Build_Employ()
        {
        }

        public void SpendTime_Employ()
        {
            EmployableCharacterModels.Foreach(ecm =>
            {
                var characterPTd = new CharacterGenerateParameter(EmployableCharacterLevelBound);
                var character = CharacterGenerator.GenerateCharacter(characterPTd);
            });
        }

        protected void OnLevelUp_Employ(int level)
        {
            EmployableCharacterModels.EnsureCapacity(EmployableCharacterAmount);
            for (var i = 0; i < EmployableCharacterAmount - EmployableCharacterModels.Count; i++)
            {
                EmployableCharacterModels.Add(EmployableCharacterModels.Count - 1, new EmployCharacterSlotModel());
            }
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}