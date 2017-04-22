using AnyGame.Server.Entity.Character;
using AnyGame.Server.Entity.Guide;
using AnyGame.Server.Entity.NetCode;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using System;

namespace AnyGame.Server.Interface.Client
{
    /// <summary>
    /// 使用背包物品结果
    /// </summary>
    [ClientInterface]
    public interface IPlayer
    {
        /// <summary>
        /// 解锁某个新手引导
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="type">新手引导类型</param>
        /// <param name="isPass">是否通过</param>
        [NetMethod((ushort)OpCode.UnlockGuidRecordResult, NetMethodType.SimpleMethod)]
        void UnlockGuideRecordResult(NetState netstate, GuideTypes type, bool isPass);

        /// <summary>
        /// 同步新手引导记录
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="records">记录数据</param>
        [NetMethod((ushort)OpCode.SyncGuideRecords, NetMethodType.SimpleMethod)]
        void SyncGuideRecords(NetState netstate, GuideTypes[] records);

        /// <summary>
        /// 解锁菜单
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="menu">菜单</param>
        /// <param name="isUnlock">是否解锁</param>
        [NetMethod((ushort)OpCode.UnlockMenuResult, NetMethodType.SimpleMethod)]
        void UnlockMenuResult(NetState netstate, MenuTypes menu, bool isUnlock);

        /// <summary>
        /// 同步已解锁菜单列表
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="menus">菜单项</param>
        [NetMethod((ushort)OpCode.SyncUnlockMenus, NetMethodType.SimpleMethod)]
        void SyncUnlockMenus(NetState netstate, MenuTypes[] menus);

        /// <summary>
        /// 玩家改名结果
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="result">结果</param>
        /// <param name="newName">新名字</param>
        [NetMethod((ushort)OpCode.PlayerRenameResult, NetMethodType.SimpleMethod)]
        void PlayerRenameResult(NetState netstate, RenameResultType result, string newName);

        /// <summary>
        /// 同步玩家信息
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="lastLoginTime">上一次登录时间</param>
        /// <param name="lastLogoffTime">上一次登出时间</param>
        /// <param name="level">等级</param>
        /// <param name="exp">当前等级的经验</param>
        /// <param name="expSum">累计经验</param>
        /// <param name="vipLevel">Vip等级</param>
        [NetMethod((ushort)OpCode.SyncPlayerInfo, NetMethodType.SimpleMethod)]
        void SyncPlayerInfo(NetState netstate, string name, int sex, DateTime createTime, DateTime lastLoginTime, DateTime lastLogoffTime, int level, int exp, long expSum, int vipLevel);

        /// <summary>
        /// 同步玩家属性信息
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="property">属性</param>
        [NetMethod((ushort)OpCode.SyncPlayerPropertyInfo, NetMethodType.SimpleMethod)]
        void SyncPlayerPropertyInfo(NetState netstate, Property property);
    }
}
