using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server 
namespace AnyGame.Server.Entity.Bags
#else
namespace AnyGame.Client.Entity.Bags
#endif
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male = 0,

        /// <summary>
        /// 女性
        /// </summary>
        Female = 1,
    }
}
