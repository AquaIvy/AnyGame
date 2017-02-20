#if Server 
using TradeAge.Server.Entity.Common;
namespace TradeAge.Server.Entity.Bags
#else
using TradeAge.Client.Entity.Common;
namespace TradeAge.Client.Entity.Bags
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
