using System;
using System.Linq;
using System.Threading;
using KKSFramework;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using Zenject;

namespace AutoChess
{
    public class FieldCharacterElement : ElementBase<CharacterData>, IResolveTarget
    {
        #region Fields & Property
        
        public override CharacterData ElementData { get; set; }

#pragma warning disable CS0649
        
        [Resolver]
        private MovingSystemModule _movingSystem;

        [Resolver]
        private Action<CharacterData> _setAppearance;

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
        
        public override void SetElement (CharacterData elementData)
        {
            _setAppearance (elementData);
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