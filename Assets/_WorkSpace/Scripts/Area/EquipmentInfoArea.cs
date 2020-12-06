using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
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
        private StarGradeArea _starGradeArea;

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
            _starGradeArea.SetArea (areaData.EquipmentGrade);
            _equipmentImage.Value = ResourcesLoadHelper.LoadResource<Sprite> (ResourceRoleType._Image,
                ResourcesType.Equipment, areaData.EquipmentData.SpriteResName);

            _baseStatusElements.ForEach (element => element.gameObject.SetActive (false));
            _baseStatusElementLineObjs.ForEach (obj => obj.SetActive (false));
            areaData.StatusList.ForEach ((status, index) =>
            {
                _baseStatusElements[index].gameObject.SetActive (true);
                _baseStatusElements[index].SetElement (areaData.GetBaseStatusModel (status.StatusData.StatusType));

                var objIndex = index - 1;

                if (objIndex >= 0 && objIndex <= _baseStatusElementLineObjs.Length - 1)
                    _baseStatusElementLineObjs[objIndex].SetActive (true);
            });

            // SetEquipState (true);
        }


        #endregion


        #region EventMethods

        #endregion
    }
}