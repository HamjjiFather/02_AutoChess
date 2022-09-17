using AutoChess.Domain;
using AutoChess.Repository;
using AutoChess.Service;
using JetBrains.Annotations;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    [UsedImplicitly]
    public class OfficeSkillInvestUseCase : IUseCaseBase
    {
        #region Fields & Property

        [Inject]
        private OfficeSkillSystemManager _officeSkillSystemManager;

        [Inject]
        private OfficeSkillRepository _officeSkillRepository;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        public void Execute(OfficeSkillBranchType branchType, int index, int amount)
        {
            _officeSkillSystemManager.InvestOfficeSkill(branchType, index, amount);
            _officeSkillRepository.Update(new OfficeSkillDao(index, 1));
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}