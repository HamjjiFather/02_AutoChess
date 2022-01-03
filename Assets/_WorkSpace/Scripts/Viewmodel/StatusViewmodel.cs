using KKSFramework.DesignPattern;
using MasterData;
using UnityEditor;

namespace AutoChess
{
    public class StatusViewmodel : ViewModelBase
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        public override void Initialize ()
        {
        }


        #region Methods

        public string DisplayValue ()
        {
            return string.Empty;
        }


        public string ValueString (AbilityType abilityType, float value)
        {
            var statusData = TableDataHelper.Instance.GetAbility (abilityType);
            return value.ToString (statusData.Format);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}