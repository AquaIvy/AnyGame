using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using TradeAge.Server.Entity.NetCode;

namespace TradeAge.Server.Interface.Server
{
    /// <summary>
    /// 玩家背包模块
    /// </summary>
    public interface IBag : ILogicModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="itemId"></param>
        /// <param name="useCount"></param>
        [NetMethod((ushort)OpCode.UseItem, NetMethodType.SimpleMethod)]
        void OnUseItem(NetState netstate, int itemId, int useCount);
    }
}
