using System.Collections.Generic;
using System.Linq;
using AutoChess.Domain;
using KKSFramework;
using Zenject;

namespace AutoChess
{
    public enum BehaviourState
    {
        FindTarget,
        CheckInteractionArea,
        FindInteractableArea,
        
        DoInteract,
        DoMove,
        WaitForNextBehaviour,
    }
    
    /// <summary>
    /// 전투 관리.
    /// 캐릭터 추합.
    /// 전투 시작.
    /// </summary>
    public class BattleManager : ManagerBase, IBattleStateReceiver, IGameSceneManager<GameSceneParameterBase>
    {
        public List<BattleInteractableUnit> PlayerUnits, EnemyUnits;

        /// <summary>
        /// 공용 데이터.
        /// </summary>
        public BattleShareData BattleShareData;

        public void EntryCharacters(IEnumerable<CharacterUnit> players, IEnumerable<CharacterUnit> enemies)
        {
            PlayerUnits.AddRange(players.Select(p => new BattleInteractableUnit(BattleSideType.Player, p)));
            EnemyUnits.AddRange(enemies.Select(p => new BattleInteractableUnit(BattleSideType.Enemy, p)));
        }

        public void OnStart(GameSceneParameterBase parameter)
        {
            
        }

        public void Dispose()
        {
            
        }


        #region Core

        public void StartBattle()
        {
            PlayerUnits.Union(EnemyUnits).Foreach(units => units.StartBattle());
        }


        public void EndBattle()
        {
            PlayerUnits.Union(EnemyUnits).Foreach(units => units.EndBattle());
        }

        #endregion
    }
}