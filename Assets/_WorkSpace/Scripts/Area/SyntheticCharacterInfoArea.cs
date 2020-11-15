using System;
using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class SyntheticCharacterInfoArea : AreaBase<CharacterModel, UnityAction>, IResolveTarget
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

        private UnityAction _deselectCallback;

        public List<CharacterModel> SyntheticMaterialCharacters =>
            _selectedSyntheticCharacters.Select (x => x.ElementData.CharacterModel).ToList (); 
        
        #endregion


        #region UnityMethods

        private void Awake ()
        {
            var index = 0;
            _selectedSyntheticCharacters.ForEach (x => x.SetBaseIndex (index++));
        }

        #endregion


        #region Methods
        
        public override void SetArea (CharacterModel characterModel, UnityAction deselectCallback)
        {
            _syntheticTargetCharacter = characterModel;
            _deselectCallback = deselectCallback;
            
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
                SetArea (characterModel, _deselectCallback);
                return true;
            }
            
            var firstElement = _selectedSyntheticCharacters.FirstOrDefault (element => !element.HasElementData);
            if (firstElement == null)
                return false;

            firstElement.ElementData.CharacterModel = characterModel;
            firstElement.ElementData.DeselectCharacter = DeselectMaterialCharacter;
            
            firstElement.SetElement (firstElement.ElementData);
            return false;

            void DeselectMaterialCharacter (int id)
            {
                _selectedSyntheticCharacters[id].SetEmptyElement ();
                _deselectCallback.Invoke ();
            }
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}