using UnityEngine;

namespace KKSFramework.Navigation
{
    public abstract class AreaBase<T> : MonoBehaviour
    {
        public abstract void SetArea (T areaData);
    }
    
    public abstract class AreaBase<T1, T2> : MonoBehaviour
    {
        public abstract void SetArea (T1 areaData1, T2 areaData2);
    }
    
    public abstract class AreaBase<T1, T2, T3> : MonoBehaviour
    {
        public abstract void SetArea (T1 areaData1, T2 areaData2, T3 areaData3);
    }
    
    public abstract class AreaBase<T1, T2, T3, T4> : MonoBehaviour
    {
        public abstract void SetArea (T1 areaData1, T2 areaData2, T3 areaData3, T4 areaData4);
    }
    
    public abstract class AreaBase<T1, T2, T3, T4, T5> : MonoBehaviour
    {
        public abstract void SetArea (T1 areaData1, T2 areaData2, T3 areaData3, T4 areaData4, T5 areaData5);
    }
}