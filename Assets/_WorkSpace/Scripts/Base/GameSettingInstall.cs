using System;
using MasterData;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    [Serializable]
    public class CommonColorSetting
    {
        public Color playerHealthGageColor;

        public Color aiHealthGageColor;

        public Color enoughPriceColor;

        public Color notEnoughPriceColor;

        public Color[] statusColor;

        // public Color GetStatusColor (StatusType statusType)
        // {
        //     var arrayIndex = (int) statusType - 1;
        //     return arrayIndex <= statusColor.Length ? statusColor[(int) statusType - 1] : Color.white;
        // }

        public Color GetPriceColor (bool isEnough)
        {
            return isEnough ? enoughPriceColor : notEnoughPriceColor;
        }
    }

    [Serializable]
    public class GameSetting
    {
        public int baseCharacterUniqueId = 100000;

        public int baseEquipmentUniqueId = 200000;
    }

    [CreateAssetMenu (fileName = "GameSetting", menuName = "Installers/GameSetting")]
    public class GameSettingInstall : ScriptableObjectInstaller<GameSettingInstall>
    {
        public GameSetting gameSetting;

        public CommonColorSetting commonColorSetting;

        public override void InstallBindings ()
        {
            Container.BindInstance (gameSetting).AsSingle ();
            Container.BindInstance (commonColorSetting).AsSingle ();
        }
    }
}