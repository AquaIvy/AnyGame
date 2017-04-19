using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using AnyGame.Server.Entity.NetCode;

namespace AnyGame.Server.Interface.Server
{
    /// <summary>
    /// 玩家背包模块
    /// </summary>
    public interface IBag : ILogicModule
    {
        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="itemId">该物品的唯一id，非物品模版id</param>
        /// <param name="useCount"></param>
        [NetMethod((ushort)OpCode.UseItem, NetMethodType.SimpleMethod)]
        void OnUseItem(NetState netstate, int itemId, int useCount);

        /// <summary>
        /// 升级背包
        /// </summary>
        [NetMethod((ushort)OpCode.UpgradeBag, NetMethodType.SimpleMethod)]
        void OnUpgradeBag(NetState netstate);
    }
}
