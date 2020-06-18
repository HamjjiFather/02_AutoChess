using KKSFramework.DesignPattern;

namespace AutoChess
{
    public class BaseStatusModel : ModelBase
    {
        #region Fields & Property

        public float StatusValue;

        public float GradeValue;

        public Status StatusData;

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        public string DisplayValue => StatusValue.ToString (StatusData.Format);

        #endregion


        #region Methods

        public BaseStatusModel ()
        {
        }

        
        public BaseStatusModel (Status statusData)
        {
            StatusData = statusData;
        }


        public void SetStatusValue (float statusValue)
        {
            StatusValue = statusValue;
        }

        public void SetGradeValue (float gradeValue)
        {
            GradeValue = gradeValue;
        }

        #endregion
    }
}