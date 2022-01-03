using Helper;
using KKSFramework.Navigation;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class SkillInfoArea : AreaBase<CharacterData>
    {
        #region Fields & Property
        
        public Text skillNameText;
                                                  
        public Text skillDescText;

#pragma warning disable CS0649

        [Inject]
        private SkillViewmodel _skillViewmodel;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public override void SetArea (CharacterData areaData)
        {
            skillNameText.text = LocalizeHelper.FromName (areaData.SkillData.Name);
            skillDescText.text = SkillViewHelper.Instance.ToSkillDescriptionString (areaData);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}