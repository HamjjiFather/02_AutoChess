using System.Collections.Generic;
using System.Linq;
using Helper;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using ResourcesLoad;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace AutoChess
{
    public class InAdventureEquipmentArea : AreaBase<UnityAction<EquipmentModel>>, IResolveTarget
    {
        #region Fields & Property
        
#pragma warning disable CS0649
        
        [Resolver]
        private Transform _contents;

        [Inject]
        private EquipmentViewModel _equipmentViewModel;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649
        
        private readonly List<InAdventureEquipmentInfoListElement> _listElements = new List<InAdventureEquipmentInfoListElement> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetArea (UnityAction<EquipmentModel> areaData)
        {
            _listElements.ForEach (element => ObjectPoolingHelper.Despawn (element.transform));
            _listElements.Clear ();

            _adventureViewmodel.AdventureRewardModel.InAdventureEquipmentModels.Values.Foreach (equipmentModel =>
            {
                var element = ObjectPoolingHelper.Spawn<InAdventureEquipmentInfoListElement> (
                    ResourceRoleType.Bundles.ToString (),
                    ResourcesType.Element.ToString (), nameof (InAdventureEquipmentInfoListElement), _contents);

                element.SetElement (new InAdventureEquipmentInfoListElementModel
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