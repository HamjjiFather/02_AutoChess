using System;
using System.Collections.Generic;

namespace AutoChess
{
    /// <summary>
    /// 개별 능력치의 발동자-수치 컨테이너. 
    /// </summary>
    public class AbilityAllocatorContainer : Dictionary<string, double>, IDisposable
    {
        #region Fields & Property

        #endregion


        public void Dispose ()
        {
            Clear ();
        }
    }
}