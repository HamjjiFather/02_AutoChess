using System.Linq;
using System.Threading;
using KKSFramework.Navigation;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FieldCharacterElement : ElementBase<CharacterModel>
    {
        #region Fields & Property
        
        public override CharacterModel ElementData { get; set; }

        public MovingSystemModule movingSystem;

        public Animator characterAnimator;
        
        public Image characterImage;

#pragma warning disable CS0649

        [Inject]
        private AdventureViewmodel _adventureViewmodel;

#pragma warning restore CS0649

        private FieldViewLayout _fieldViewLayout;
        
        /// <summary>
        /// Task Cancellation TokenSource.
        /// </summary>
        private CancellationTokenSource _cancellationToken;
        
        
        #endregion


        #region UnityMethods

        #endregion


        #region Methods
        
        public override void SetElement (CharacterModel elementData)
        {
            characterImage.sprite = elementData.IconImageResources;
            characterAnimator.runtimeAnimatorController = elementData.CharacterAnimatorResources;
            _cancellationToken = new CancellationTokenSource();
            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();
        }


        public void Dispose ()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.DisposeSafe ();
        }

        
        public async UniTask MoveTo (FieldTargetResultModel resultModel)
        {
            var elements = resultModel.FoundPositions.Select (position => _fieldViewLayout.GetLandElement (position));
            await movingSystem.Moving (elements, _adventureViewmodel.SetSight, _cancellationToken.Token);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}