using KKSFramework.DataBind;
using KKSFramework.ResourcesLoad;
using UnityEngine;

namespace AutoChess
{
    public class LandElement : PrefabComponent, IResolveTarget
    {
        public Context context;
        
        public PositionModel PositionModel;
        
        [Resolver]
        private GameObject _highlightedField;

        public GameObject HighlightedField => _highlightedField;

        public void ManualResolve ()
        {
            context.Resolve ();
        }

        public void SetPosition (int column, int row)
        {
            PositionModel = new PositionModel (column, row);
        }
    }
}