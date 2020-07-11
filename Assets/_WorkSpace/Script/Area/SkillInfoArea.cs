using KKSFramework.Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace AutoChess
{
    public class SkillInfoArea : AreaBase<SkillModel>
    {
        #region Fields & Property
        
        public Image skillIconImage;
                                                  
        public Text skillLevelText;
                                                  
        public Text skillNameText;
                                                  
        public Text skillDescText;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (SkillModel areaData)
        {
            throw new System.NotImplementedException ();
        }

        #endregion


        #region EventMethods

        #endregion



    }
}