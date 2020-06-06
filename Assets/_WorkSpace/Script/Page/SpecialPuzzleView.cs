using System;
using System.Collections.Generic;
using BaseFrame;
using KKSFramework.ResourcesLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HexaPuzzle
{
    public class SpecialPuzzleView : MonoBehaviour
    {
        #region Fields & Property

        public ScrollRect scrollRect;
        
        public Transform elementParents;

#pragma warning disable CS0649

        [Inject]
        private PuzzleViewmodel _puzzleViewmodel;

#pragma warning restore CS0649

        private readonly List<SpecialPuzzleElement> specialPuzzleElements = new List<SpecialPuzzleElement> ();

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            LoadResource ();
            
            void LoadResource ()
            {
                var resObj = ResourcesLoadHelper.GetResources<SpecialPuzzleElement> (ResourceRoleType._Prefab,
                    ResourcesType.Element, "SpecialPuzzleElement");
                
                using (var puzzleModelEnum = _puzzleViewmodel.AllSpecialPuzzles.Values.GetEnumerator ())
                {
                    while (puzzleModelEnum.MoveNext ())
                    {
                        var model = puzzleModelEnum.Current;
                        var element = resObj.InstantiateObject<SpecialPuzzleElement> (elementParents);
                        element.SetData (model);
                        specialPuzzleElements.Add (element);
                    }
                }
            }
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}