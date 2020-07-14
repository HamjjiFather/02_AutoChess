using UniRx.Async;
using Zenject;

namespace AutoChess
{
    public class CharacterViewLayout : ViewLayoutBase
    {
        #region Fields & Property

        public CharacterInfoArea characterInfoArea;

        public CharacterListArea characterListArea;

#pragma warning disable CS0649

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override UniTask ActiveLayout ()
        {
            characterListArea.SetArea (ClickCharacterElement);
            return base.ActiveLayout ();
        }
        
        #endregion


        #region EventMethods

        private void ClickCharacterElement (CharacterModel characterModel)
        {
            characterInfoArea.SetArea (characterModel);
        }

        #endregion
    }
}