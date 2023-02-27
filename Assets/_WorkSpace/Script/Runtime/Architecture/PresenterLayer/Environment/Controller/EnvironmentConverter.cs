using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using KKSFramework;
using UnityEngine;

namespace AutoChess.Presenter
{
    public enum EnvironmentType
    {
        None,
        Base,
        Adventure,
        Battle
    }

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

        private Dictionary<EnvironmentType, GameEnvironmentBase> _gameEnvironments = new ();

        public BaseEnvironmentController baseEnvironment;
        
        public AdventureEnvironmentController adventureEnvironment;

        public BattleEnvironmentController battleEnvironment;

        private EnvironmentType _currentEnvironmentType;

        #endregion


        #region Methods

        #region Override

        private void Awake()
        {
            _gameEnvironments.Add(EnvironmentType.Base, baseEnvironment);
            _gameEnvironments.Add(EnvironmentType.Adventure, adventureEnvironment);
            _gameEnvironments.Add(EnvironmentType.Battle, battleEnvironment);
            
            _gameEnvironments.Foreach(ge => ge.Value.gameObject.SetActive(false));
        }


        private void Start()
        {
            ChangeEnvironment(EnvironmentType.Base, default);
        }

        #endregion


        #region This

        public void ChangeEnvironment(EnvironmentType environmentType, EnvironmentParameterBase parameter = default)
        {
            if (_gameEnvironments.ContainsKey(_currentEnvironmentType))
            {
                _gameEnvironments[_currentEnvironmentType].gameObject.SetActive(false);
                _gameEnvironments[_currentEnvironmentType].OnEnvironmentDisabled();
            }

            _currentEnvironmentType = environmentType;
            
            if (_gameEnvironments.ContainsKey(_currentEnvironmentType))
            {
                _gameEnvironments[_currentEnvironmentType].OnEnvironmentEnabled(parameter);
                _gameEnvironments[_currentEnvironmentType].gameObject.SetActive(true);
            }
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}