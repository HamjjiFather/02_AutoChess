using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using KKSFramework.DesignPattern;
using KKSFramework.LocalData;

namespace HexaPuzzle
{
    public class CharacterViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649


#pragma warning restore CS0649

        private int _lastUniqueId = 0;
        
        private readonly List<CharacterModel> _allCharacterModels = new List<CharacterModel> ();
        
        private readonly List<CharacterModel> _battleCharacterModels = new List<CharacterModel> ();

        private readonly int[] StartCharacterIndexes = 
        {
            6000,
            6001,
            6002,
            6003,
            6004
        };

        #endregion


        public override void Initialize ()
        {
        }

        public override void InitTableData ()
        {
            base.InitTableData ();
        }

        public override void InitLocalData ()
        {
            _lastUniqueId = LocalDataHelper.GetGameBundle ().LastCharacterUniqueId;
            
            var characterBundle = LocalDataHelper.GetCharacterBundle ();

            if (!characterBundle.CharacterUniqueIds.Any ())
            {
                StartCharacterIndexes.Foreach ((characterIndex, index) =>
                {
                    var characterModel = new CharacterModel ();
                    var characterData = TableDataManager.Instance.CharacterDict[characterIndex];
                    characterModel.SetUniqueData (NewUniqueId(), 0);
                    characterModel.SetCharacterData (characterData);
                    characterModel.SetStatusModel (GetStatusModel (characterData));

                    _battleCharacterModels.Add (characterModel);
                });
            }

            characterBundle.CharacterUniqueIds.Foreach ((uid, index) =>
            {
                var characterModel = new CharacterModel ();
                var characterData = TableDataManager.Instance.CharacterDict[characterBundle.CharacterIds[index]];
                characterModel.SetUniqueData (CombineUniqueId(uid), characterBundle.CharacterExps[index]);
                characterModel.SetCharacterData (characterData);
                characterModel.SetStatusModel (GetStatusModel (characterData));

                _battleCharacterModels.Add (characterModel);
            });
        }


        #region Methods

        public CharacterModel GetBattleCharacterModel (int index)
        {
            return _battleCharacterModels.Count > index ? _battleCharacterModels[index] : default;
        }

        public StatusModel GetStatusModel (Character character)
        {
            var status = new StatusModel (character.Hp, 0, character.At, character.Df);
            return status;
        }


        public string CombineUniqueId (int uniqueIndex)
        {
            return $"{GameConstants.BaseUniqueId}{uniqueIndex}";
        }
        
        public string NewUniqueId ()
        {
            _lastUniqueId++;
            LocalDataHelper.SaveGameUniqueIdData (_lastUniqueId);
            return $"{GameConstants.BaseUniqueId}{_lastUniqueId}";
        }

        #endregion


        #region EventMethods

        #endregion
    }
}