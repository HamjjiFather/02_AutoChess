using System.Linq;
using BaseFrame;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class CombineViewLayout : ViewLayoutBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private CombineInfoArea _combineInfo;

        [Resolver]
        private CharacterListArea _characterListArea;

        [Resolver]
        private EquipmentListArea _equipmentListArea;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

#pragma warning restore CS0649

        private ICombineMaterial _selectedMaterial;

        private bool _isCharacterView;

        public const string MaterialParamKey = "SelecetedMaterial";

        public const string IsCharacterParamKey = "IsCharacter";

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnActiveAsync (Parameters parameters)
        {
            if (parameters == null)
                return UniTask.CompletedTask;

            _isCharacterView = parameters.GetValueOrDefault (IsCharacterParamKey, false, false);

            if (!parameters.ContainsKey (MaterialParamKey))
            {
                _combineInfo.EmptyTargetMaterial ();

                if (_isCharacterView)
                    _characterListArea.SetArea (ClickMaterialList);
                else
                    _equipmentListArea.SetArea (ClickMaterialList);
                
                return base.OnActiveAsync (parameters);
            }

            var targetMaterial =
                parameters.GetValueOrDefault (MaterialParamKey, default (ICombineMaterial), false);

            TargetMaterialState (targetMaterial);

            return base.OnActiveAsync (parameters);
        }


        /// <summary>
        /// 합성 목표 캐릭터를 선택함.
        /// </summary>
        private void TargetMaterialState (ICombineMaterial combineMaterial)
        {
            _selectedMaterial = combineMaterial;
            _combineInfo.SetArea (combineMaterial, UpdateMaterial);

            UpdateMaterial ();
        }


        private void UpdateMaterial ()
        {
            if (_isCharacterView)
            {
                var characterList =
                    _characterViewmodel.AllCharacterModels.Where (x =>
                        _selectedMaterial.Index.Equals (x.CharacterData.Index) &&
                        !_combineInfo.CombineMaterials.Contains (x) &&
                        x != _selectedMaterial && ((int) x.StarGrade).Equals (_selectedMaterial.Grade)).ToList ();

                _characterListArea.SetAreaForced (ClickMaterialList, characterList, false);
                return;
            }

            var equipmentList =
                _equipmentViewmodel.EquipmentModels.Values.Where (x =>
                    _selectedMaterial.Index.Equals (x.EquipmentData.Index) &&
                    !_combineInfo.CombineMaterials.Contains (x) &&
                    x != _selectedMaterial && ((int) x.EquipmentGrade).Equals (_selectedMaterial.Grade)).ToList ();

            _equipmentListArea.SetAreaForced (ClickMaterialList, equipmentList, false);
        }

        #endregion


        #region EventMethods

        private void ClickMaterialList (ICombineMaterial combineMaterial)
        {
            var isTargetMaterial = _combineInfo.SelectMaterial (combineMaterial);
            if (isTargetMaterial)
            {
                TargetMaterialState (combineMaterial);
            }

            UpdateMaterial ();
        }

        #endregion
    }
}