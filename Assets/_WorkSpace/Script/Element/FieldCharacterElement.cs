using System.Linq;
using System.Threading;
using KKSFramework.Navigation;
using Cysharp.Threading.Tasks;
using KKSFramework;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class FieldCharacterElement : ElementBase<CharacterModel>, IResolveTarget
    {
        #region Fields & Property
        
        public override CharacterModel ElementData { get; set; }

#pragma warning disable CS0649
        
        [Resolver]
        private MovingSystemModule _movingSystem;

        [Resolver]
        private Animator _characterAnimator;
        
        [Resolver]
        private SpriteRenderer _characterSpriteRenderer;

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
            _characterSpriteRenderer.sprite = elementData.IconImageResources;
            _characterAnimator.runtimeAnimatorController = elementData.CharacterAnimatorResources;
            _cancellationToken = new CancellationTokenSource();
            _fieldViewLayout = ProjectContext.Instance.Container.Resolve<FieldViewLayout> ();
            _movingSystem.SetMovingTarget(transform);
        }


        public void Dispose ()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.DisposeSafe ();
        }

        
        public async UniTask MoveTo (FieldTargetResultModel resultModel)
        {
            var elements = resultModel.FoundPositions.Select (position => _fieldViewLayout.GetLandElement (position));
            await _movingSystem.Moving (elements, _adventureViewmodel.CompleteMove, _cancellationToken.Token);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}