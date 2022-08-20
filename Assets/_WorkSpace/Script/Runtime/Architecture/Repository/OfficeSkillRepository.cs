using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using JetBrains.Annotations;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess.Repository
{
    public readonly struct OfficeSkillResetDto
    {
        public readonly int[] indexes;

        public readonly GameSystemType[] GameSystemTypes;

        public OfficeSkillResetDto(int[] indexes, GameSystemType[] gameSystemTypes)
        {
            this.indexes = indexes;
            GameSystemTypes = gameSystemTypes;
        }
    }


    public readonly struct OfficeSkillDao : IDAO
    {
        public readonly int Index;

        public readonly int SpentPointAmount;

        public OfficeSkillDao(int index, int spentPointAmount)
        {
            Index = index;
            SpentPointAmount = spentPointAmount;
        }
    }


    [UsedImplicitly]
    public class OfficeSkillRepository : IRepository<OfficeSkillDao>
    {
        #region Fields & Property

        [Inject]
        private OfficeSkillBundle _officeSkillBundle;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        public List<OfficeSkillDao> ReadAll(IEnumerable<int> indexes) =>
            ((IRepository<OfficeSkillDao>) this).ReadAll(indexes);

        public OfficeSkillDao Read(int index)
            => Translate(_officeSkillBundle.Resolve(index));

        public void Update(OfficeSkillDao entity)
        {
            var bundleSet = _officeSkillBundle.Resolve(entity.Index);
            bundleSet.investmentPoint = entity.SpentPointAmount;
        }

        public void Delete(OfficeSkillDao entity)
        {
            throw new System.NotImplementedException();
        }

        public OfficeSkillDao Translate(OfficeSkillBundleSet bundleSet) =>
            new(bundleSet.index, bundleSet.investmentPoint);

        #endregion


        #region Event

        #endregion

        #endregion
    }
}