using System;
using KKSFramework;
using Helper;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoChess
{
    public class FormationCharacterElement : ElementBase<CharacterData>, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private EventTrigger _eventTrigger;
        
        [Resolver]
        private Animator _characterAniamtor;

        [Resolver]
        private Property<Sprite> _characterImage;

#pragma warning restore CS0649

        public override CharacterData ElementData { get; set; }
        
        public int MyIndex { get; private set; }

        private Action<FormationCharacterElement, PointerEventData> _dragEndAction;

        private Vector3 _originPosition;

        #endregion


        private void Awake ()
        {
            var dragEntry = new EventTrigger.Entry();
            dragEntry.callback.AddListener (eventData =>
            {
                var screenToWorldPoint =
                    NavigationProjectManager.Instance.MainCamera.ScreenToWorldPoint (eventData.currentInputModule.input.mousePosition);
                transform.position = new Vector3 (screenToWorldPoint.x, screenToWorldPoint.y, transform.position.z);
            });
            dragEntry.eventID = EventTriggerType.Drag;
            _eventTrigger.triggers.Add (dragEntry);
            
            var beginDragEntry = new EventTrigger.Entry ();
            beginDragEntry.callback.AddListener (eventData =>
            {
                _originPosition = transform.position;
            });
            beginDragEntry.eventID = EventTriggerType.BeginDrag;
            _eventTrigger.triggers.Add (beginDragEntry);
            
            var endDragEntry = new EventTrigger.Entry ();
            endDragEntry.callback.AddListener (eventData =>
            {
                _dragEndAction.CallSafe (this, (PointerEventData)eventData);
            });
            endDragEntry.eventID = EventTriggerType.EndDrag;
            _eventTrigger.triggers.Add (endDragEntry);
        }


        public override void SetElement (CharacterData elementData)
        {
            _characterImage.Value = elementData.IconImageResources;
            _characterAniamtor.runtimeAnimatorController = elementData.CharacterAnimatorResources;
        }
        
        
        public void SetElement (int index, Action<FormationCharacterElement, PointerEventData> dragEndAction)
        {
            MyIndex = index;
            _dragEndAction = dragEndAction;
        }


        public void ReturnToOriginPosition ()
        {
            transform.position = _originPosition;
        }
    }
}