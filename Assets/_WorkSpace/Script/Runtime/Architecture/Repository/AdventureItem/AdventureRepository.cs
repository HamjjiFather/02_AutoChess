using AutoChess.Bundle;
using JetBrains.Annotations;
using KKSFramework.Data;
using KKSFramework.Repository;
using Zenject;

namespace AutoChess.Repository
{
    [UsedImplicitly]
    public class AdventureRepository : IRepository
    {
        #region Fields & Property

        [Inject]
        private AdventureBundle _adventureBundle;

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
        }

        #endregion


        #region This

        public void Update(int issuancedUniqueIndex)
        {
            _adventureBundle.Bind(issuancedUniqueIndex);
        }

        public int GetIssuanceUniqueIndex => _adventureBundle.issuancedUniqueIndex;

        #endregion


        #region Event

        #endregion

        #endregion


    }
}