using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using Zenject;

namespace AutoChess.Service
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
    public class BattleSystemManager : ManagerBase, IBattleStateReceiver
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
        
        
        /// <summary>
        /// 사망한 적 캐릭터를 갈무리함.
        /// </summary>
        public InvestigateResult InvestigateEnemy(BattleInteractableUnit battleInteractableUnit)
        {
            if (!battleInteractableUnit.Death)
                return default;

            if (battleInteractableUnit.CharacterUnit.CharacterTableData is not Enemy enemyTableData) return default;
            
            var probabilities = enemyTableData.DropItemProbabilities;
            var pickedItems = ProbabilityHelper.GetItemsForAll(probabilities).ToList();
            if (!pickedItems.Any())
                return default;

            var globalIndexes = pickedItems
                .Select(pi =>
                    new ItemBase(enemyTableData.DropItemGlobalIndexes[pi], enemyTableData.DropItemAmounts[pi]));
            var result = new InvestigateResult(globalIndexes.ToList());
            return result;
        }
    }

    public struct ItemBase
    {
        public ItemBase(string globalIndex, int itemAmount)
        {
            GlobalIndex = globalIndex;
            ItemAmount = itemAmount;
        }

        public string GlobalIndex;

        public int ItemAmount;
    }

    public class InvestigateResult
    {
        public InvestigateResult(List<ItemBase> resultItems)
        {
            ResultItems = resultItems;
        }

        public List<ItemBase> ResultItems;
    }
}