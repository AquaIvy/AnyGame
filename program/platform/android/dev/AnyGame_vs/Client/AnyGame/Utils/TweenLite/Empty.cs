
namespace AnyGame.Utils.TweenLite
{
    /// <summary>
    /// 空特效
    /// </summary>
    public static class Empty
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static float EaseIn(float t, float b, float c, float d)
        {
            return b;
        }
    }
}
