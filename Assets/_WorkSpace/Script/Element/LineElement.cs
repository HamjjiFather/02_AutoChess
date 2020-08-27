using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AutoChess
{
    public class LineElement : MonoBehaviour
    {
        private readonly List<LandElement> _landElements = new List<LandElement> ();
        public IEnumerable<LandElement> LandElements => _landElements;

        public LandElement GetLandElement (int index)
        {
            return _landElements[index];
        }


        public void AddLandElement (LandElement landElement)
        {
            _landElements.Add (landElement);
        }


        public async UniTask Clear ()
        {
            _landElements.ForEach (element =>
            {
                Destroy (element.gameObject);
            });
            _landElements.Clear ();

            await UniTask.WaitWhile (() => _landElements.Any ());
        }
    }
}