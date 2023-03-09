using System;

namespace AutoChess
{
    public interface IGameSceneManager : IDisposable
    {
    }
    
    public interface IGameSceneManager<in T> : IGameSceneManager where T : GameSceneParameterBase
    {
        void OnStart(T parameter);
    }
}