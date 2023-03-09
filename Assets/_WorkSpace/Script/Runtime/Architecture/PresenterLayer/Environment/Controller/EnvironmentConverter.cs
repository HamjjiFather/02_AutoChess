using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KKSFramework;
using UnityEngine;

namespace AutoChess.Presenter
{
    public class EnvironmentParameterBase
    {
        
    }


    public class BattleEnvironmentParameter : EnvironmentParameterBase
    {
        
    }
    
    [UsedImplicitly]
    public class EnvironmentConverter : MonoBehaviour
    {
        #region Fields & Property

        private Dictionary<GameSceneType, GameEnvironmentBase> _gameEnvironments = new ();

        public Dictionary<GameSceneType, GameEnvironmentBase> GameEnvironments
        {
            get
            {
                if (_gameEnvironments.Any()) return _gameEnvironments;
                _gameEnvironments.Add(GameSceneType.Base, baseEnvironment);
                _gameEnvironments.Add(GameSceneType.Adventure, adventureEnvironment);
                _gameEnvironments.Add(GameSceneType.Battle, battleEnvironment);
                _gameEnvironments.Foreach(ge => ge.Value.gameObject.SetActive(false));

                return _gameEnvironments;
            }
            set => _gameEnvironments = value;
        }

        public BaseEnvironmentController baseEnvironment;
        
        public AdventureEnvironmentController adventureEnvironment;

        public BattleEnvironmentController battleEnvironment;

        private GameSceneType _currentGameSceneType;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void ChangeEnvironment(GameSceneType GameSceneType, EnvironmentParameterBase parameter = default)
        {
            if (GameEnvironments.ContainsKey(_currentGameSceneType))
            {
                GameEnvironments[_currentGameSceneType].gameObject.SetActive(false);
                GameEnvironments[_currentGameSceneType].OnEnvironmentDisabled();
            }

            _currentGameSceneType = GameSceneType;
            
            if (GameEnvironments.ContainsKey(_currentGameSceneType))
            {
                GameEnvironments[_currentGameSceneType].OnEnvironmentEnabled(parameter);
                GameEnvironments[_currentGameSceneType].gameObject.SetActive(true);
            }
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}