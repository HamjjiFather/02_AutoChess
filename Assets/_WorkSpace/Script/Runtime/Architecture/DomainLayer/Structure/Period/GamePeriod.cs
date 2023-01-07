using System.Collections.Generic;

namespace AutoChess
{
    /// <summary>
    /// 게임 시간 클래스.
    /// </summary>
    public class GameTime : ITimeBase
    {
        #region Fields & Property

        public List<ITimeBase> TimeBases;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        public void RegistTimeBase(ITimeBase timeBase)
        {
            if (TimeBases.Contains(timeBase))
                return;
            
            TimeBases.Add(timeBase);
        }

        public void SpendTime() => TimeBases.ForEach(tb => tb.SpendTime());

        #endregion


        #region Event

        #endregion

        #endregion
    }
}