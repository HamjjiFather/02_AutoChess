using System;
using JetBrains.Annotations;

namespace AutoChess
{
    /// <summary>
    /// 인게임 파이팅 공식 관리. 
    /// </summary>
    [UsedImplicitly]
    public static class FormulaHelper
    {
        #region Fields & Property

        /// <summary>
        /// 공격 속도 기준 값.
        /// </summary>
        public const float AttackSpeedBasePercent = 10000f;

        /// <summary>
        /// 최소 프레임 단위.
        /// </summary>
        public const int MinimunFrameRate = 30;

        /// <summary>
        /// 최소 프레임 시간.
        /// </summary>
        public static float MinimunFrameSeconds => 1 / (float) MinimunFrameRate;

        #endregion


        #region Methods

        #region Override

        #endregion


        #region This

        /// <summary>
        /// 비율에 대한 값을 기준 값으로 나누어 0 ~ n리턴(상한선 x).
        /// 파라미터 값과 기준 값이 일치할 경우 1.
        /// <example>
        /// value가 5이고 기준 값이 100일 경우 0.05를 리턴.
        /// </example>
        /// </summary>
        public static double PercentLerp01Unclamped (double value)
        {
            var r = value / Constant.BasePercentValue;
            return r;
        }

        public static double PercentLerp01Unclamped (float value) =>
            PercentLerp01Unclamped ((double) value);

        public static double PercentLerp01Unclamped (long value) =>
            PercentLerp01Unclamped ((double) value);

        public static double PercentLerp01Unclamped (int value) =>
            PercentLerp01Unclamped ((double) value);


        #region Exp

        /// <summary>
        /// 계정 현재 레벨에 맞는 요구 경험치를 리턴.
        /// 공식은 1을 시작기준으로 잡았기 때문에 실제 레벨에 1을 더해준 값으로 요구 경험치를 구한다.
        /// </summary>
        public static double RequireExp (double level)
        {
            var result = 100 * Math.Pow (level + 1, 2) - 100 * (level + 1) + 300;
            return result;
        }

        #endregion
        
        // #비용.


        #region Price

        #endregion


        // #능력.


        #region Ability

        #endregion

        #endregion


        #region Event

        #endregion

        #endregion
    }
}