using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server
namespace AnyGame.Server.Entity.Character
#else
namespace AnyGame.Client.Entity.Character
#endif
{
    /// <summary>
    /// 同步物品类型
    /// </summary>
    public enum SyncType
    {
        /// <summary>
        /// 首次初始化
        /// </summary>
        First = 0,

        /// <summary>
        /// 添加
        /// </summary>
        Add = 1,

        /// <summary>
        /// 删除
        /// </summary>
        Remove = 2,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 3,
    }
}
