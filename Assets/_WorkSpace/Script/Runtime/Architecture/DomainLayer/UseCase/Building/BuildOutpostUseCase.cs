using System.Linq;
using AutoChess.Repository;
using KKSFramework.Domain;

namespace AutoChess.UseCase
{
    public class BuildOutpostUseCase : IUseCaseBase
    {
        public BuildOutpostUseCase(OutpostRepository repository)
        {
            _outpostRepository = repository;
        }

        #region Fields & Property

        private OutpostRepository _outpostRepository;
        
        #endregion


        #region Methods

        #region Override

        public void Initialize()
        {
        }

        #endregion


        #region This

        public void Execute(OutpostBuildingEntity entity)
        {
            var dto = new OutpostBuildingDto(entity.OutpostIndex, entity.ExtendBuildingList.Cast<int>());
            _outpostRepository.Update(dto);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}