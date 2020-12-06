using BaseFrame;
using KKSFramework.DataBind;
using KKSFramework.Navigation;
using MasterData;
using UnityEngine.UI;

namespace AutoChess
{
    public class StarGradeArea : AreaBase<int>, IResolveTarget
    {
        #region Fields & Property

        public Context context;

#pragma warning disable CS0649

        [Resolver]
        private Image[] _onStarObj;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (int grade)
        {
            context.Resolve ();
            _onStarObj.ForEach ((obj, index) => { obj.enabled = index <= grade; });
        }
        
        
        public void SetArea (StarGrade grade)
        {
            SetArea ((int)grade);
        }
        
        
        public void SetArea (EquipmentGrade grade)
        {
            SetArea ((int)grade);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}