using System.Collections.Generic;
using UnityEngine;

namespace AutoChess
{
    public class BattleCharacterListArea : MonoBehaviour
    {
        #region Fields & Property

        public BattleCharacterInfoElement[] battleCharacterInfoElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetCharacterList (IEnumerable<CharacterModel> characterModels)
        {
            characterModels.ZipForEach (battleCharacterInfoElements, (model, element)  =>
            {
                element.SetElement (model);
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}