using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using AnyGame.Server.Entity.NetCode;
using AnyGame.Server.Entity.Guide;
using AnyGame.Server.Entity.Character;

namespace AnyGame.Server.Interface.Server
{
    /// <summary>
    /// 玩家模块
    /// </summary>
    public interface IPlayer : ILogicModule
    {
        /// <summary>
        /// 解锁某个新手引导
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="type">新手引导类型</param>
        [NetMethod((ushort)OpCode.UnlockGuideRecord, NetMethodType.SimpleMethod)]
        void UnlockGuideRecord(NetState netstate, GuideTypes type);

        /// <summary>
        /// 解锁某个功能菜单
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="menu">菜单类型</param>
        [NetMethod((ushort)OpCode.UnlockMenu, NetMethodType.SimpleMethod)]
        void UnlockMenu(NetState netstate, MenuTypes menu);

        /// <summary>
        /// 玩家改名
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="newName">新名字</param>
        [NetMethod((ushort)OpCode.PlayerRename, NetMethodType.SimpleMethod)]
        void PlayerRename(NetState netstate, string newName);
    }
}
