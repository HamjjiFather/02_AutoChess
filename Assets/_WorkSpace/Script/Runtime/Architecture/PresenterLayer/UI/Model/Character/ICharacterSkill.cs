using System.Linq;

namespace AutoChess.Presenter
{
    public interface ICharacterSkill
    {
        public Character CharacterTableData { get; set; }

        /// <summary>
        /// 기본 공격을 포함한 스킬 인덱스.
        /// </summary>
        public int[] SkillDataIndexes { get; set; }

        // /// <summary>
        // /// 스킬 데이터.
        // /// </summary>
        // public CharacterSkill[] SkillDatas =>
        //     SkillDataIndexes.Select(sdi => TableDataManager.Instance.CharacterSkillDict[sdi]).ToArray();
    }
}