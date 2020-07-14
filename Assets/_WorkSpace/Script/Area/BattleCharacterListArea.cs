using System;
using KKSFramework.Navigation;
using UniRx;
using UnityEngine;

namespace AutoChess
{
    public class BattleCharacterListArea : AreaBase<ReactiveCollection<CharacterModel>>
    {
        #region Fields & Property

        public BattleCharacterInfoElement[] battleCharacterInfoElements;
        
#pragma warning disable CS0649
        
#pragma warning restore CS0649

        private IDisposable _clickDisposable;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (ReactiveCollection<CharacterModel> areaData)
        {
            areaData.ZipForEach (battleCharacterInfoElements, (model, element) =>
            {
                element.SetElement (model);
            });
            
            areaData.ObserveReplace ().Subscribe (area =>
            {
                battleCharacterInfoElements[area.Index].SetElement (area.NewValue);
            });
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickActions"></param>
        public void SetElementClickActions (Action<CharacterModel> clickActions)
        {
            battleCharacterInfoElements.Foreach (element =>
            {
                element.RegistActiveAction (clickActions);
            });

            _clickDisposable = Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonUp (0)).Subscribe (_ =>
            {
                battleCharacterInfoElements.Foreach (x => x.RemoveAllEvents ());
                _clickDisposable.DisposeSafe ();
            });
        }

        #endregion


        #region EventMethods

        #endregion
    }
}