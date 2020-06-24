using System.Collections.Generic;
using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class SkillModel : ModelBase
    {
        #region Fields & Property

        public CharacterModel UseCharacterModel;

        public PositionModel TargetPosition;
        
        public Skill SkillData;

        public List<CharacterModel> TargetCharacters = new List<CharacterModel> ();

        public List<float> SkillValue = new List<float> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion
    }
}