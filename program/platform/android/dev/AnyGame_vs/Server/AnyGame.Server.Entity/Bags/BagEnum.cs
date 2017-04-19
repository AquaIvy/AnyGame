using System;

#if Server 
using AnyGame.Server.Entity.Common;
namespace AnyGame.Server.Entity.Bags
#else
using AnyGame.Client.Entity.Common;
namespace AnyGame.Client.Entity.Bags
#endif
{
    public enum UseItemResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,

        /// <summary>
        /// 剩余数量不足
        /// </summary>
        NotEnough = 2,

        /// <summary>
        /// 未找到该物品
        /// </summary>
        NotFind = 3,

        /// <summary>
        /// 今日使用次数已经用完
        /// </summary>
        HasNoTimesToday = 4,
    }

    /// <summary>
    /// 升级背包返回
    /// </summary>
    public enum UpgradeBagResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,

        /// <summary>
        /// 缺少资源
        /// </summary>
        LessRes = 2,

        /// <summary>
        /// 已达等级上限
        /// </summary>
        HasOverTop = 3
    }
}
