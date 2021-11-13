using System;
using System.Linq;
using KKSFramework;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;

namespace AutoChess
{
    public class BattleCharacterListArea : AreaBase<ReactiveCollection<CharacterModel>>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private BattleCharacterInfoElement[] _battleCharacterInfoElements;
        
#pragma warning restore CS0649

        private IDisposable _clickDisposable;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (ReactiveCollection<CharacterModel> areaData)
        {
            areaData.ZipForEach (_battleCharacterInfoElements, (model, element) =>
            {
                element.SetElement (model);
            });
            
            areaData.ObserveReplace ().Subscribe (area =>
            {
                _battleCharacterInfoElements[area.Index].SetElement (area.NewValue);
            });
        }


        public BattleCharacterInfoElement GetElement (int index)
        {
            return _battleCharacterInfoElements[index];
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickActions"></param>
        public void SetElementClickActions (Action<CharacterModel> clickActions)
        {
            _battleCharacterInfoElements.Where (x => x.IsAssigned).Foreach (element =>
            {
                element.RegistActiveAction (clickActions);
            });

            _clickDisposable = Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonUp (0)).Subscribe (_ =>
            {
                _battleCharacterInfoElements.Where (x => x.IsAssigned).Foreach (x => x.RemoveAllEvents ());
                _clickDisposable.DisposeSafe ();
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}