namespace AnyGame.Utils.TweenLite
{
    /// <summary>
    /// 加速移动特效
    /// </summary>
    public static class Back
    {
        public static float EaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
        }

        public static float EaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
        }

        /// <summary>
        /// 带回弹功能
        /// </summary>
        /// <param name="t"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float EaseInOut(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1)
            {
                return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
            }
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }
    }
}
