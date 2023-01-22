using KKSFramework.GameSystem;
using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UnityEngine;

namespace AutoChess.Presenter
{
    public struct DeathInfo
    {
        public bool Death;

        public string DeathDate;
    }


    public class CharacterModelBase : LevelBase, ICharacterAbility, ICharacterSkill, IGetSprite
    {
        public CharacterModelBase(int uniqueIndex, global::Character characterTable)
        {
            UniqueIndex = uniqueIndex;
            CharacterTableData = characterTable;
        }

        #region Fields & Property

        /// <summary>
        /// 유니크 ID.
        /// </summary>
        public int UniqueIndex;

        public Character CharacterTableData { get; set; }
        
        /// <summary>
        /// 캐릭터 등급.
        /// </summary>
        public PrimeAbilityBase PrimeAbilityBase { get; set; }
        
        /// <summary>
        /// 스킬 인덱스.
        /// </summary>
        public int[] SkillDataIndexes { get; set; }
        
        /// <summary>
        /// 사망 정보.
        /// </summary>
        public DeathInfo CharacterDeathInfo;

        public RuntimeAnimatorController CharacterAnimatorResources =>
            ResourcesLoadHelper.GetResources<RuntimeAnimatorController>(ResourceRoleType._Animation,
                CharacterTableData.AnimatorResName);


        public Sprite GetSprite => ((IGetSprite) this).GetSprite(CharacterTableData.SpriteResName);


#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #endregion


        #region Level

        #endregion


        #region Equipment

        #endregion


        #region Skill

        #endregion

        #region Util

        #endregion
    }
}