using UniRx;

namespace AutoChess.Domain
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
    
    
    public class AdventureManager : ManagerBase, IGameSceneManager<GameSceneParameterBase>
    {
        #region Fields & Property

        public IntReactiveProperty ExploreGoodsAmount;
        
        #endregion


        #region Methods

        #region Override


        public void OnStart(GameSceneParameterBase parameter)
        {
            ExploreGoodsAmount = new IntReactiveProperty(100);
        }


        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        #endregion


        #region This

        public void Initialize(object parameter)
        {
            ExploreGoodsAmount = new IntReactiveProperty(100);
        }

        #endregion


        #region Event

        #endregion

        #endregion
    }
}