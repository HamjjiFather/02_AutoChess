using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class EquipmentListArea : AreaBase<UnityAction<EquipmentModel>>
    {
        #region Fields & Property
        
        public Transform contents;

#pragma warning disable CS0649
        
        [Inject]
        private EquipmentViewmodel _equipmentViewmodel;

#pragma warning restore CS0649
        
        private readonly List<EquipmentInfoListElement> _listElements = new List<EquipmentInfoListElement> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetArea (UnityAction<EquipmentModel> areaData)
        {
            if (!_equipmentViewmodel.IsDataChanged) return;
            _equipmentViewmodel.IsDataChanged = false;
            
            _listElements.Foreach (element => element.PoolingObject ());
            _listElements.Clear ();

            _equipmentViewmodel.EquipmentModels.Values.Foreach (equipmentModel =>
            {
                var element = ObjectPoolingHelper.GetResources<EquipmentInfoListElement> (ResourceRoleType._Prefab,
                    ResourcesType.Element, nameof(EquipmentInfoListElement), contents);

                element.SetElement (new EquipmentInfoListElementModel
                {
                    EquipmentModel = equipmentModel,
                    ElementClick = areaData
                });
                _listElements.Add (element);
            });

            _listElements.First ().elementButton.onClick.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion


    }
}