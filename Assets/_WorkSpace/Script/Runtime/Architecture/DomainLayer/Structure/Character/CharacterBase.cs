using System.Collections.Generic;
using System.Linq;
using KKSFramework.GameSystem;

namespace AutoChess
{
    /// <summary>
    /// 캐릭터.
    /// </summary>
    public class CharacterBase : ExpLevelModel, IGetSubAbility
    {
        public CharacterBase(PlayableCharacter tableData, IEnumerable<int> baseValues, IEnumerable<int> investedValues,
            int level, double exp)
        {
            _characterTableData = tableData;
            _primeAbilityContainer = new PrimeAbilityContainer(baseValues, investedValues);
            _subAbilityContainer = new SubAbilityContainer();

            Level = level;
            MaxLevel = CharacterDefine.MaxCharacterLevel;
            Exp = exp;
            RequireExps = TableDataManager.Instance.PlayerLevelDict.Values.Select(x => (double) x.ReqExp).ToArray();
        }

        #region Fields & Property

        /// <summary>
        /// 캐릭터 테이블 데이터.
        /// </summary>
        private readonly PlayableCharacter _characterTableData;

        /// <summary>
        /// 캐릭터 롤.
        /// </summary>
        public CharacterRoleType CharacterRoleType => _characterTableData.CharacterRoleType;

        /// <summary>
        /// 캐릭터의 주요 능력치 컨테이너.
        /// </summary>
        private readonly PrimeAbilityContainer _primeAbilityContainer;

        /// <summary>
        /// 캐릭터의 기본 보조 능력치.
        /// </summary>
        private readonly SubAbilityContainer _subAbilityContainer;

        #endregion


        #region Methods

        #region Override

        public int GetSubAbilityValue(SubAbilityType subAbilityType)
        {
            var fromPrimeAbility = _primeAbilityContainer.GetSubAbilityValue(subAbilityType);
            var fromSubAbility = _subAbilityContainer.GetSubAbilityValue(subAbilityType);
            return fromPrimeAbility + fromSubAbility;
        }

        #endregion


        #region This

        #endregion


        #region Event

        #endregion

        #endregion
    }
}