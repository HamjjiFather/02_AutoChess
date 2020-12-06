using UnityEngine.Events;

namespace AutoChess
{
    public class SelectedCombineModel : ICombinable
    {
        public ICombineMaterial CombineMaterialModel { get; set; }

        public UnityAction<int> DeselectMaterial;


        public SelectedCombineModel ()
        {
        }

        public SelectedCombineModel (ICombineMaterial combineMaterial, UnityAction<int> deselectMaterial)
        {
            CombineMaterialModel = combineMaterial;
            DeselectMaterial = deselectMaterial;
        }

        public void Empty ()
        {
            CombineMaterialModel = default;
            DeselectMaterial = null;
        }
    }
}