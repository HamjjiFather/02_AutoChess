using AutoChess.Repository;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    /// <summary>
    /// 탐험에서 성공적으로 탈출함.
    /// </summary>
    public class OutOfAdventureUseCase : IUseCaseBase
    {
        [Inject]
        private AdventureInventoryRepository _adventureInventoryRepository;
        
        public void Initialize()
        {
        }
        

        public void Execute()
        {
            
        }
    }
}