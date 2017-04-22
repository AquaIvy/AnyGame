using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server
namespace AnyGame.Server.Entity.Guide
#else
namespace AnyGame.Client.Entity.Guide
#endif
{
    /// <summary>
    /// 新手引导类型
    /// </summary>
    public enum GuideTypes
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
    }

}
