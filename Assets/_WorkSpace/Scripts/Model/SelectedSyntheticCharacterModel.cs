using UnityEngine.Events;

namespace AutoChess
{
    public class SelectedSyntheticCharacterModel
    {
        public CharacterModel CharacterModel;
        
        public UnityAction<int> DeselectCharacter;


        public SelectedSyntheticCharacterModel ()
        {
        }

        public SelectedSyntheticCharacterModel (CharacterModel characterModel, UnityAction<int> deselectCharacter)
        {
            CharacterModel = characterModel;
            DeselectCharacter = deselectCharacter;
        }

        public void Empty ()
        {
            CharacterModel = default;
            DeselectCharacter = null;
        }
    }
}