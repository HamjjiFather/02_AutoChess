using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class CharacterSyntheticViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private SyntheticCharacterInfoArea _syntheticCharacterInfo;

        [Resolver]
        private CharacterListArea _characterListArea;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        private CharacterModel _selectedCharacterModel;

        public const string SyntheticCharacterViewLayoutParamKey = "character";

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnActiveAsync (Parameters parameters)
        {
            if (parameters == null || !parameters.ContainsKey (SyntheticCharacterViewLayoutParamKey))
            {
                _syntheticCharacterInfo.EmptyTargetCharacter ();
                _characterListArea.SetArea (ClickCharacterList);
                return base.OnActiveAsync (parameters);
            }

            var characterModel =
                parameters.GetValueOrDefault (SyntheticCharacterViewLayoutParamKey, default (CharacterModel), false);

            TargetCharacterState (characterModel);

            return base.OnActiveAsync (parameters);
        }


        /// <summary>
        /// 합성 목표 캐릭터를 선택함.
        /// </summary>
        private void TargetCharacterState (CharacterModel characterModel)
        {
            _selectedCharacterModel = characterModel;
            _syntheticCharacterInfo.SetArea (characterModel);

            UpdateMaterialCharacters ();
        }


        private void UpdateMaterialCharacters ()
        {
            var characterList =
                _characterViewmodel.AllCharacterModels.Where (x =>
                    _selectedCharacterModel.CharacterData.Index.Equals (x.CharacterData.Index) &&
                    !_syntheticCharacterInfo.SyntheticMaterialCharacters.Contains (x) &&
                    x != _selectedCharacterModel && x.StarGrade.Equals (_selectedCharacterModel.StarGrade)).ToList ();

            _characterListArea.SetAreaForced (ClickCharacterList, characterList, false);
        }

        #endregion


        #region EventMethods

        private void ClickCharacterList (CharacterModel characterModel)
        {
            var isTargetCharacter = _syntheticCharacterInfo.SelectCharacter (characterModel);
            if (isTargetCharacter)
            {
                TargetCharacterState (characterModel);
            }

            UpdateMaterialCharacters ();
        }

        #endregion
    }
}