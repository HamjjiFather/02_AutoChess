using AutoChess.Domain;
using AutoChess.Repository;
using AutoChess.Service;
using JetBrains.Annotations;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    [UsedImplicitly]
    public class OfficeSkillResetUseCase : IUseCaseBase
    {
        #region Fields & Property
        
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

        public void Execute(OfficeSkillBranchType branchType)
        {
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}