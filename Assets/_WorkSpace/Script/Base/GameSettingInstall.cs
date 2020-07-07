using System;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    [Serializable]
    public class GameSetting
    {
        public Color playerHealthGageColor;

        public Color aiHealthGageColor;

        public int baseCharacterUniqueId = 100000;

        public readonly string[] PlayerCharacterPosition =
        {
            "1,1",
            "2,1",
            "3,2",
            "4,1",
            "5,1"
        };
    }
    
    [CreateAssetMenu (fileName = "GameSetting", menuName = "Installers/GameSetting")]
    public class GameSettingInstall : ScriptableObjectInstaller<GameSettingInstall>
    {
        public GameSetting gameSetting;

        public override void InstallBindings ()
        {
            Container.BindInstance (gameSetting).AsSingle ();
        }
    }
}