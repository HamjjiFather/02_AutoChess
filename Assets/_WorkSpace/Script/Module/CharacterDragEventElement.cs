using System;
using BaseFrame;
using Helper;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoChess
{
    public class CharacterDragEventElement : EventTrigger
    {
        private int _myIndex;

        public int MyIndex => _myIndex;

        private Action<CharacterDragEventElement, PointerEventData> _dragEndAction;

        private Vector3 _originPosition;

        
        public void SetElement (int index, Action<CharacterDragEventElement, PointerEventData> dragEndAction)
        {
            _myIndex = index;
            _dragEndAction = dragEndAction;
        }


        public void ReturnToOriginPosition ()
        {
            transform.position = _originPosition;
        }

        
        public override void OnBeginDrag (PointerEventData eventData)
        {
            _originPosition = transform.position;
            base.OnBeginDrag (eventData);
        }


        public override void OnDrag (PointerEventData eventData)
        {
            base.OnDrag (eventData);
            var screenToWorldPoint = TreeNavigationHelper.GetContentCamera ().ScreenToWorldPoint (eventData.position);
            transform.position = new Vector3 (screenToWorldPoint.x, screenToWorldPoint.y, transform.position.z);
        }

        
        public override void OnEndDrag (PointerEventData eventData)
        {
            base.OnEndDrag (eventData);
            _dragEndAction.CallSafe (this, eventData);
        }
    }
}