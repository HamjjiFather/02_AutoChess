using System.Collections.Generic;
using KKSFramework.DesignPattern;
using UniRx;

namespace AutoChess
{
    public class AdventureModel : ModelBase
    {
        #region Fields & Property

        /// <summary>
        /// 모든 필드 모델.
        /// </summary>
        public Dictionary<int, List<FieldModel>> AllFieldModel;
        
        /// <summary>
        /// 모든 숲 필드 모델.
        /// </summary>
        public Dictionary<int, List<FieldModel>> AllForestModels = new Dictionary<int, List<FieldModel>> ();
        
        /// <summary>
        /// 시작 필드.
        /// </summary>
        public FieldModel StartField;
        
        public IntReactiveProperty AdventureCount;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion
        
        
        public AdventureModel (int adventureCount)
        {
            AdventureCount = new IntReactiveProperty (adventureCount);
        }


        #region Methods


        public void SetBaseField (Dictionary<int, List<FieldModel>> allFieldModel)
        {
            AllFieldModel = allFieldModel;
        }
        

        public void SetField (Dictionary<int, List<FieldModel>> forestFieldModel, FieldModel startField)
        {
            AllForestModels = forestFieldModel;
            StartField = startField;
        }

        public void DecreaseAdventureCount ()
        {
            AdventureCount.Value--;
        }
        
        #endregion
    }
}