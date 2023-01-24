using AutoChess.Repository;
using AutoChess;
using JetBrains.Annotations;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    [UsedImplicitly]
    public class OfficeSkillInvestUseCase : IUseCaseBase
    {
        #region Fields & Property

        // [Inject]
        // private OfficeSkillRepository _officeSkillRepository;

        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        // public void Execute(OfficeSkillBranchType branchType, int index, int amount)
        // {
        //     _officeSkillRepository.Update(new OfficeSkillDao(index, 1));
        // }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}