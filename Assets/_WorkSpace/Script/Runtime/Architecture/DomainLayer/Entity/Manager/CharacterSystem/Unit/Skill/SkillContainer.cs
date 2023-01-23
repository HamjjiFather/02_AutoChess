using System.Linq;

namespace AutoChess
{
    public class SkillContainer : IGetAbilities
    {
        public const int BaseSkillSlotAmount = 3;
        
        #region Fields & Property

        public readonly SkillUnit[] SkillUnits = new SkillUnit[BaseSkillSlotAmount];

        #endregion


        #region Methods

        #region Override

        public float GetAbilityValue(SubAbilityType abilityTypeType) =>
            default;

        #endregion


        #region This

        /// <summary>
        /// 스킬을 교체함.
        /// </summary>
        public void ChangeSkill(int index, SkillUnit skillUnit)
        {
            if (index >= BaseSkillSlotAmount)
                return;

            SkillUnits[index] = skillUnit;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}