using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using AnyGame.Server.Entity.Bags;
using AnyGame.Server.Entity.NetCode;
using AnyGame.Server.Entity.Common;

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
        /// 同步物品
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="type">同步类型，可参见该枚举定义</param>
        /// <param name="items">物品列表</param>
        [NetMethod((ushort)OpCode.SyncItem, NetMethodType.SimpleMethod)]
        void SyncItems(NetState netstate, SyncType type, params GameItem[] items);

        /// <summary>
        /// 同步背包信息
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="maxGridCount"></param>
        /// <param name="items"></param>
        [NetMethod((ushort)OpCode.SyncBag, NetMethodType.SimpleMethod)]
        void SyncBag(NetState netstate, int maxGridCount, GameItem[] items);

        /// <summary>
        /// 同步所有的资源
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="money">金币</param>
        /// <param name="gem">钻石</param>
        [NetMethod((ushort)OpCode.SyncAllResouce, NetMethodType.SimpleMethod)]
        void SyncAllResouce(NetState netstate, int money, int gem);

        /// <summary>
        /// 同步资源
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="resId">资源id</param>
        /// <param name="num">数量</param>
        [NetMethod((ushort)OpCode.SyncResouce, NetMethodType.SimpleMethod)]
        void SyncResouce(NetState netstate, int resId, int num);

        /// <summary>
        /// 升级背包的结果
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="result"></param>
        [NetMethod((ushort)OpCode.UpgradeBagResult, NetMethodType.SimpleMethod)]
        void UpgradeBagResult(NetState netstate, UpgradeBagResult result);
    }
}
