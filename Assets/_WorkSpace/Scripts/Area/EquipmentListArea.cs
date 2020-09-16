using System.Collections.Generic;
using System.Linq;
using BaseFrame;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using KKSFramework.ResourcesLoad;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class EquipmentListArea : AreaBase<UnityAction<EquipmentModel>>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Transform _contents;

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

            _listElements.ForEach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            _equipmentViewmodel.EquipmentModels.Values.ForEach (equipmentModel =>
            {
                var element = ObjectPoolingHelper.Spawn<EquipmentInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (EquipmentInfoListElement), _contents);

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