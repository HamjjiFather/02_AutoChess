using AutoChess;

namespace AutoChess
{
    public enum FieldActionType
    {
        None = -1,
        
        /// <summary>
        /// 기습.
        /// </summary>
        Ambush,
        
        /// <summary>
        /// 정찰.
        /// </summary>
        Scout,
        
        /// <summary>
        /// 전투.
        /// </summary>
        Battle,
        
        /// <summary>
        /// 전초기지 메뉴.
        /// </summary>
        ShowOutpostMenu,
        
        /// <summary>
        /// 탈출하기.
        /// </summary>
        Exit,
    }
    
    public class AdventureManager : ManagerBase
    {
        #region Fields & Property

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void StartAdventure(OutpostBuildingEntity startOutpost)
        {
            
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}