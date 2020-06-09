using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DesignPattern;
using UniRx;

namespace HexaPuzzle
{
    public class CharacterModel : ModelBase
    {
        #region Fields & Property

        public string UniqueCharacterId;
        
        public IntReactiveProperty Exp = new IntReactiveProperty ();
        
        public Character CharacterData;

        public StatusModel StatusModel;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        public void SetUniqueData (string uid, int exp)
        {
            UniqueCharacterId = uid;
            Exp.Value = exp;
        }

        public void SetCharacterData (Character character)
        {
            CharacterData = character;
        }


        public void SetStatusModel (StatusModel statusModel)
        {
            StatusModel = statusModel;
        }
        

        #endregion
    }
}