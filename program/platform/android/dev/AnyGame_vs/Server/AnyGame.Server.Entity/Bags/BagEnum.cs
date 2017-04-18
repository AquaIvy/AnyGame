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
        /// 
        /// </summary>
        Success = 0,

        /// <summary>
        /// 
        /// </summary>
        Fail = 1,
    }
}
