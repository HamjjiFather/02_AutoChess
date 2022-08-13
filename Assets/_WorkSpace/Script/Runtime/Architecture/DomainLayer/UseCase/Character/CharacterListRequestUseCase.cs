using System.Collections.Generic;
using System.Linq;
using AutoChess.Bundle;
using AutoChess.Presenter;
using KKSFramework.Domain;
using Zenject;

namespace AutoChess.UseCase
{
    /// <summary>
    /// 전체 보유한 캐릭터 리스트 요청.
    /// </summary>
    public class CharacterListRequestUseCase : IUseCaseBase
    {
        public void Initialize()
        {
        }


        public List<CharacterModelBase> Execute()
        {
            return null;
        }
    }
}