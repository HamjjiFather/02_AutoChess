using BaseFrame;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HexaPuzzle
{
    public class BattleCharacterView : MonoBehaviour
    {
        #region Fields & Property
        
        public BattleCharacterElement[] battleCharacterElements;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;
        
#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        private void Awake ()
        {
            battleCharacterElements.Foreach ((element, index) =>
            {
                element.SetElement(_characterViewmodel.GetBattleCharacterModel (index));
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}