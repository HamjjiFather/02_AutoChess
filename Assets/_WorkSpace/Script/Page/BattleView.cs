using UnityEngine;
using Zenject;

namespace HexaPuzzle
{
    public class BattleView : MonoBehaviour
    {
        #region Fields & Property

        public BattleCharacterElement[] battleCharacterElements;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void OnEnable ()
        {
            battleCharacterElements.Foreach ((element, index) =>
            {
                element.SetElement (_characterViewmodel.GetBattleCharacterModel (index));
            });
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}