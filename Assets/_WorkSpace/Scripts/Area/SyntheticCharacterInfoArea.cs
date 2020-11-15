using System.Collections.Generic;
using System.Linq;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class SyntheticCharacterInfoArea : AreaBase<CharacterModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private GameObject _targetCharacterObj;
        
        [Resolver]
        private Image _characterTypeIcon;

        [Resolver]
        private Property<string> _characterNameText;

        [Resolver]
        private GameObject _emptyTargetCharacterObj;
        
        [Resolver]
        private StarGradeArea _starGradeArea;
        
        [Resolver]
        private Property<Sprite> _characterImage;

        [Resolver]
        private Animator _characterAnimator;
        
        [Resolver]
        private SelectedSyntheticCharacterElement[] _selectedSyntheticCharacters;

#pragma warning restore CS0649

        private CharacterModel _syntheticTargetCharacter;

        public List<CharacterModel> SyntheticMaterialCharacters =>
            _selectedSyntheticCharacters.Select (x => x.ElementData.CharacterModel).ToList (); 
        
        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetArea (CharacterModel characterModel)
        {
            _syntheticTargetCharacter = characterModel;
            
            _targetCharacterObj.SetActive (true);
            _emptyTargetCharacterObj.SetActive (false);
            
            _characterNameText.Value = LocalizeHelper.FromName (characterModel.CharacterData.Name);
            _starGradeArea.SetArea (characterModel.StarGrade);
            _characterImage.Value = characterModel.IconImageResources;
            _characterAnimator.runtimeAnimatorController = characterModel.CharacterAnimatorResources;
        }
        

        /// <summary>
        /// 조합 대상 캐릭터를 비움.
        /// </summary>
        public void EmptyTargetCharacter ()
        {
            _targetCharacterObj.SetActive (false);
            _emptyTargetCharacterObj.SetActive (true);
        }


        /// <summary>
        /// 조합 재료 캐릭터를 선택함.
        /// </summary>
        public bool SelectCharacter (CharacterModel characterModel)
        {
            if (_syntheticTargetCharacter is default (CharacterModel))
            {
                SetArea (characterModel);
                return true;
            }
            
            var firstElement = _selectedSyntheticCharacters.FirstOrDefault (element => !element.HasElementData);
            if (firstElement == null)
                return false;
            
            firstElement.SetElement (new SelectedSyntheticCharacterModel (characterModel, DeselectMaterialCharacter));
            return false;

            void DeselectMaterialCharacter (int id)
            {
                _selectedSyntheticCharacters[id].SetEmptyElement ();
            }
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}