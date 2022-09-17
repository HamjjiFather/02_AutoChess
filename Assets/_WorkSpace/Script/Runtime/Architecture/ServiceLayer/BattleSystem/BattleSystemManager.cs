using System.Collections.Generic;
using System.Linq;

namespace AutoChess.Service
{
    /// <summary>
    /// 전투 관리.
    /// 캐릭터 추합.
    /// 전투 시작.
    /// </summary>
    public class BattleSystemManager : ManagerBase
    {
        public List<BattleInteractor> PlayerUnits, EnemyUnits;

        public void EntryCharacters(IEnumerable<CharacterUnit> players, IEnumerable<CharacterUnit> enemies)
        {
            PlayerUnits.AddRange(players.Select(p => new BattleInteractor(p)));
            EnemyUnits.AddRange(enemies.Select(p => new BattleInteractor(p)));
        }


        public void StartBattle()
        {
            
        }
        

        public void EndBattle()
        {
            
        }
    }
}