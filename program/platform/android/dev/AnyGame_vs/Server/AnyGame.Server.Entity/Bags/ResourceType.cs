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
    public static class ResourceType
    {
        /// <summary>
        /// 钱id
        /// </summary>
        public const int Gold = 1;

        /// <summary>
        /// 钻石
        /// </summary>
        public const int Gem = 2;
    }
}
