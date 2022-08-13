using AutoChess.Repository;
using JetBrains.Annotations;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    /// <summary>
    /// 탐험에서 패배함.
    /// </summary>
    [UsedImplicitly]
    public class DefeatAdventureUseCase : IUseCaseBase
    {
        #region Fields & Property
        
        [Inject]
        private AdventureInventoryRepository _adventureInventoryRepository;

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
            
        }

        #endregion


        #region This

        public void Execute()
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion

       
    }
}