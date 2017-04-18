using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using AnyGame.Server.Entity.Character;
using AnyGame.Server.Entity.NetCode;

namespace AnyGame.Server.Interface.Server
{
    /// <summary>
    /// 商店模块
    /// </summary>
    public interface IShop : ILogicModule
    {
        /// <summary>
        /// 购买卡牌
        /// </summary>
        /// <param name="netstate"></param>
        [NetMethod((ushort)OpCode.LoginServer, NetMethodType.SimpleMethod)]
        void OnBugCard(NetState netstate);
    }
}
