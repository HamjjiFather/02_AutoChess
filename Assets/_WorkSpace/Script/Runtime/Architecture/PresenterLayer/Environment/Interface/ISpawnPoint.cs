using UnityEngine;

namespace AutoChess.Presenter
{
    public interface ISpawnPoint
    {
        Transform SpawnPoint { get; set; }
    }
}