using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#if Server
namespace AnyGame.Server.Entity.Character
#else
namespace AnyGame.Client.Entity.Character
#endif
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum PlatformTypes
    {
        /// <summary>
        /// 空
        /// </summary>
        [Description("无视我")]
        None = 0,

        /// <summary>
        /// 鱼乐
        /// </summary>
        [Description("鱼乐")]
        Fishluv = 1,

        /// <summary>
        /// AquaIvy
        /// </summary>
        [Description("AquaIvy")]
        AquaIvy = 2,
    }

    /// <summary>
    /// 手机平台类型
    /// </summary>
    public enum PhonePlatformTypes
    {
        /// <summary>
        /// 安卓平台
        /// </summary>
        [Description("安卓")]
        Andriod = 0,

        /// <summary>
        /// 苹果
        /// </summary>
        [Description("苹果")]
        IOS = 1,
    }
}
