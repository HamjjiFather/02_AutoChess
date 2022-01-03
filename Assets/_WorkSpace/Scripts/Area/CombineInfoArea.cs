using System.Collections.Generic;
using System.Linq;
using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutoChess
{
    public class CombineInfoArea : AreaBase<ICombineMaterial, UnityAction>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private GameObject _targetMaterialObj;
        
        [Resolver]
        private Image _characterTypeIcon;

        [Resolver]
        private Property<string> _materialNameText;

        [Resolver]
        private GameObject _emptyTargetMaterialObj;
        
        [Resolver]
        private StarGradeArea _starGradeArea;
        
        [Resolver]
        private Property<Sprite> _materialImage;

        [Resolver]
        private SelectedCombineMaterialElement[] _selectedCombineMaterials;

#pragma warning restore CS0649

        private ICombineMaterial _CombineTarget;

        private UnityAction _deselectCallback;

        public List<ICombineMaterial> CombineMaterials =>
            _selectedCombineMaterials.Select (x => x.ElementData.CombineMaterialModel).ToList (); 
        
        #endregion


        #region UnityMethods

        private void Awake ()
        {
            var index = 0;
            _selectedCombineMaterials.Foreach (x => x.SetBaseIndex (index++));
        }

        #endregion


        #region Methods
        
        public override void SetArea (ICombineMaterial material, UnityAction deselectCallback)
        {
            _CombineTarget = material;
            _deselectCallback = deselectCallback;
            
            _targetMaterialObj.SetActive (true);
            _emptyTargetMaterialObj.SetActive (false);
            
            _materialNameText.Value = LocalizeHelper.FromName (material.NameString);
            _starGradeArea.SetArea (material.Grade);
            _materialImage.Value = material.ImageSprite;
            // _characterAnimator.runtimeAnimatorController = material.CharacterAnimatorResources;
        }


        /// <summary>
        /// 조합 대상 캐릭터를 비움.
        /// </summary>
        public void EmptyTargetMaterial ()
        {
            _targetMaterialObj.SetActive (false);
            _emptyTargetMaterialObj.SetActive (true);
        }


        /// <summary>
        /// 조합 재료 캐릭터를 선택함.
        /// </summary>
        public bool SelectMaterial (ICombineMaterial material)
        {
            if (_CombineTarget is default (CharacterData))
            {
                SetArea (material, _deselectCallback);
                return true;
            }
            
            var firstElement = _selectedCombineMaterials.FirstOrDefault (element => !element.HasElementData);
            if (firstElement == null)
                return false;

            firstElement.ElementData.CombineMaterialModel = material;
            firstElement.ElementData.DeselectMaterial = DeselectMaterialCharacter;
            
            firstElement.SetElement (firstElement.ElementData);
            return false;

            void DeselectMaterialCharacter (int id)
            {
                _selectedCombineMaterials[id].SetEmptyElement ();
                _deselectCallback.Invoke ();
            }
        }
        

        #endregion


        #region EventMethods

        #endregion
    }
}