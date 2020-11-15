using UnityEngine.Events;

namespace AutoChess
{
    public struct SelectedSyntheticCharacterModel
    {
        public CharacterModel CharacterModel;

        
        public UnityAction<int> DeselectCharacter;

        
        public bool IsEmpty;
        

        public SelectedSyntheticCharacterModel (CharacterModel characterModel, UnityAction<int> deselectCharacter)
        {
            CharacterModel = characterModel;
            DeselectCharacter = deselectCharacter;
            IsEmpty = false;
        }

        public void Empty ()
        {
            IsEmpty = true;
            CharacterModel = default;
            DeselectCharacter = null;
        }
    }
}