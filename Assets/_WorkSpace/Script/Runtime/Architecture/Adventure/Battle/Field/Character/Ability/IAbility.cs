namespace AutoChess
{
    public interface IAbility
    {
        /// <summary>
        /// 값.
        /// </summary>
        public float Value { get; set; }
        
        /// <summary>
        /// 값 추가.
        /// </summary>
        public void AddValue(float value);

        /// <summary>
        /// 값 받아오기.
        /// </summary>
        public float GetValue();
    }
}