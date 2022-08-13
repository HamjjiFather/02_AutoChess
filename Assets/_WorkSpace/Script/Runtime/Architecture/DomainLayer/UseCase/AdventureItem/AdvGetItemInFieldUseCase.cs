using AutoChess.Domain;
using JetBrains.Annotations;
using KKSFramework.Domain;

namespace AutoChess.UseCase
{
    /// <summary>
    /// 필드에 있는 아이템을 획득함.
    /// </summary>
    [UsedImplicitly]
    public class AdvGetItemInFieldUseCase : IUseCaseBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override
        
        public void Initialize()
        {
            
        }

        #endregion


        #region This

        /// <summary>
        /// 획득.
        /// </summary>
        /// <param name="areaModel"></param>
        /// <returns> 성공여부 </returns>
        public bool Execute(AreaModel areaModel)
        {
            return true;
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}