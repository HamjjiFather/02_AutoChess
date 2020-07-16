using KKSFramework.GameSystem.GlobalText;
using KKSFramework.Navigation;
using UnityEngine.UI;
using Zenject;

namespace AutoChess
{
    public class SkillInfoArea : AreaBase<CharacterModel>
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

        public override void SetArea (CharacterModel areaData)
        {
            skillNameText.GetTranslatedString (areaData.SkillData.Name);
            skillDescText.text = SkillViewHelper.Instance.ToSkillDescriptionString (areaData);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}