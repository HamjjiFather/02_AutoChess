using System.Collections.Generic;
using KKSFramework.Navigation;
using UnityEngine;

namespace AutoChess
{
    public class BattleCharacterListArea : AreaBase<IEnumerable<CharacterModel>>
    {
        #region Fields & Property

        public BattleCharacterInfoElement[] battleCharacterInfoElements;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (IEnumerable<CharacterModel> areaData)
        {
            areaData.ZipForEach (battleCharacterInfoElements, (model, element)  =>
            {
                element.SetElement (model);
            });
        }

        #endregion


        #region EventMethods

        #endregion



    }
}