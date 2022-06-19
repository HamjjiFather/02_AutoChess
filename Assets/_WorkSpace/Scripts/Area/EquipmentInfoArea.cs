using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class EquipmentInfoArea : AreaBase<EquipmentModel>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Property<string> _equipmentName;

        [Resolver]
        private Property<Sprite> _equipmentImage;

        [Resolver]
        private StatusElement[] _baseStatusElements;

        [Resolver]
        private GameObject[] _baseStatusElementLineObjs;

        [Inject]
        private CharacterViewmodel _characterViewmodel;

#pragma warning restore CS0649

        public EquipmentModel AreaData;

        private BattleCharacterListArea _battleCharacterListArea;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
        }

        #endregion


        #region Methods

        public void SetBattleCharacterListComponent (BattleCharacterListArea battleCharacterListArea)
        {
            _battleCharacterListArea = battleCharacterListArea;
        }

        public override void SetArea (EquipmentModel areaData)
        {
            AreaData = areaData;
            _equipmentName.Value = LocalizeHelper.FromName (areaData.EquipmentData.Name);
            _equipmentImage.Value = ResourcesLoadHelper.GetResources<Sprite> (ResourceRoleType._Image,
                ResourcesType.Equipment, areaData.EquipmentData.SpriteResName);

            // _baseStatusElements.Foreach (element => element.gameObject.SetActive (false));
            _baseStatusElementLineObjs.Foreach (obj => obj.SetActive (false));
            // SetEquipState (true);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}