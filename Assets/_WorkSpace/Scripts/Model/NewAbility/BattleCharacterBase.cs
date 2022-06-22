using System.Collections;
using UnityEngine;

namespace AutoChess
{
    public enum BehaviourType
    {
        Idle,
        Attack,
        MoveTo
    }

    public class BattleCharacterBase : MonoBehaviour
    {
        #region Fields & Property
        
        public CharacterPrimeAbilityContainer abilityContainer;

        #endregion


        #region Methods

        #region Override
        
        public void StartBattle() 
        {
        
        }

        #endregion


        #region This
        
        public IEnumerator WaitForBehaviour() 
        {
            var behaviourAbilityValue = abilityContainer.GetAbilityValue(SubAbilities.Speed.ToString());
            var behaviourSeconds = AbilityFormula.BehaviourSecondsFromSpeedAbilityValue(behaviourAbilityValue);
            yield return new WaitForSeconds(behaviourSeconds);
            DoBehavior ();
        }
        
        
        public void DoBehavior() 
        {
        
        }
        

        #endregion


        #region Event

        #endregion

        #endregion
    }
}