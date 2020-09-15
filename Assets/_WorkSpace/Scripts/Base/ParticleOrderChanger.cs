using BaseFrame;
using UnityEngine;

namespace AutoChess
{
    [ExecuteInEditMode]
    public class ParticleOrderChanger : MonoBehaviour
    {
        #region Fields & Property

        public string sortingLayer;

        public int orderInLayer;

        public bool change;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        private void Update ()
        {
            if (change)
            {
                change = false;
                GetComponentsInChildren<ParticleSystem>().ForEach (x =>
                {
                    var renderer = x.GetComponent<Renderer> ();
                    renderer.sortingLayerName = sortingLayer;
                    renderer.sortingOrder = orderInLayer;
                });
                DestroyImmediate (this);
            }
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}