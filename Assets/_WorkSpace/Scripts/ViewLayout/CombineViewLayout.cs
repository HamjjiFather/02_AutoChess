using System.Linq;
using KKSFramework;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class CombineViewLayout : ViewLayoutBase, IResolveTarget
    {
        public struct Model
        {
            public Model (bool isCharacterView, ICombineMaterial combineMaterial)
            {
                IsCharacterView = isCharacterView;
                CombineMaterial = combineMaterial;
            }

            public bool IsCharacterView;

            public bool HasMaterial => CombineMaterial != null;

            public ICombineMaterial CombineMaterial;
        }


        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private CombineInfoArea _combineInfo;

        [Resolver]
        private CharacterListArea _characterListArea;

        [Resolver]
        private EquipmentListArea _equipmentListArea;

        [Inject]
        private CharacterManager _characterViewmodel;

        [Inject]
        private EquipmentManager _equipmentManager;

#pragma warning restore CS0649

        private ICombineMaterial _selectedMaterial;

        private bool _isCharacterView;

        public const string MaterialParamKey = "SelecetedMaterial";

        public const string IsCharacterParamKey = "IsCharacter";

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        protected override UniTask OnActiveAsync (object parameters)
        {
            if (parameters == null)
                return UniTask.CompletedTask;

            var model = (Model)parameters;

            _isCharacterView = model.IsCharacterView;

            if (!model.HasMaterial)
            {
                _combineInfo.EmptyTargetMaterial ();

                if (_isCharacterView)
                    _characterListArea.SetArea (ClickMaterialList);
                else
                    _equipmentListArea.SetArea (ClickMaterialList);

                return base.OnActiveAsync (parameters);
            }

            TargetMaterialState (model.CombineMaterial);

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
                        _selectedMaterial.Index.Equals (x.CharacterTable.Id) &&
                        !_combineInfo.CombineMaterials.Contains (x) &&
                        x != _selectedMaterial && ((int)x.StarGrade).Equals (_selectedMaterial.Grade)).ToList ();

                _characterListArea.SetAreaForced (ClickMaterialList, characterList, false);
                return;
            }

            var equipmentList =
                _equipmentManager.EquipmentModels.Values.Where (x =>
                    _selectedMaterial.Index.Equals (x.EquipmentData.Id) &&
                    !_combineInfo.CombineMaterials.Contains (x) &&
                    x != _selectedMaterial && ((int)x.EquipmentGrade).Equals (_selectedMaterial.Grade)).ToList ();

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