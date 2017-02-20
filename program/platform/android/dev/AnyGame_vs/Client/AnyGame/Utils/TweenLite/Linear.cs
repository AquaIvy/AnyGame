using System;

namespace AnyGame.Utils.TweenLite
{
    /// <summary>
    /// 线性
    /// </summary>
    public static class Linear
    {
        public static float EaseNone(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        public static float EaseIn(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        public static float EaseOut(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        public static float EaseInOut(float t, float b, float c, float d)
        {
            return c * t / d + b;
	    }

        /// <summary>
        /// 先缩小再放大
        /// </summary>
        /// <param name="t"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float FirstMinScaleNextMaxScale(float t, float b, float c, float d)
        {
            var e= (float)(Math.Sin(t / d * (Math.PI * 2) + Math.PI) * (c / 2f)) + 1;
            //Logs.Info("t={0} b={1} c={2} d={3} e={4}", t, b, c, d, e);
            return e;
        }
    }
}
