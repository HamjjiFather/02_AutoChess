using BaseFrame;
using KKSFramework.DataBind;
using UnityEngine;
using Zenject;

namespace AutoChess
{
    public class EndAdventurePopup : PopupController, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private InAdventureEquipmentArea _inAdventureEquipmentArea;
        
        [Resolver]
        private StatusElement[] _baseStatusElements;

        [Resolver]
        private GameObject[] _baseStatusElementLineObjs;

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            
        }

        #endregion


        #region Methods

        protected override void OnPush (Parameters parameters)
        {
            base.OnPush (parameters);
            
            _inAdventureEquipmentArea.SetArea (SetEquipmentInfo);
        }


        private void SetEquipmentInfo (EquipmentModel equipmentModel)
        {
            _baseStatusElements.ForEach (element => element.gameObject.SetActive (false));
            _baseStatusElementLineObjs.ForEach (obj => obj.SetActive (false));
            equipmentModel.StatusList.ForEach ((status, index) =>
            {
                _baseStatusElements[index].gameObject.SetActive (true);
                _baseStatusElements[index].SetElement (equipmentModel.GetBaseStatusModel (status.StatusData.StatusType));

                var objIndex = index - 1;

                if (objIndex >= 0 && objIndex <= _baseStatusElementLineObjs.Length - 1)
                    _baseStatusElementLineObjs[objIndex].SetActive (true);
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}