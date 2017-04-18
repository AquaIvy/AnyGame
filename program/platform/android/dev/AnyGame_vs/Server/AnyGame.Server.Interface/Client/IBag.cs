using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using AnyGame.Server.Entity.Bags;
using AnyGame.Server.Entity.NetCode;

namespace AnyGame.Server.Interface.Client
{
    /// <summary>
    /// 使用背包物品结果
    /// </summary>
    [ClientInterface]
    public interface IBag
    {
        /// <summary>
        /// 使用背包物品结果
        /// </summary>
        [NetMethod((ushort)OpCode.UseItemResult, NetMethodType.SimpleMethod)]
        void UseItemResult(NetState netstate, UseItemResult result, int itemId, int lessCount);

        /// <summary>
        /// 同步背包信息
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="MaxCount"></param>
        /// <param name="CurCount"></param>
        [NetMethod((ushort)OpCode.SyncBag, NetMethodType.SimpleMethod)]
        void SyncBag(NetState netstate, int MaxCount, int CurCount);
    }
}
