using AnyGame.Server.Entity.NetCode;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Interface.Client
{
    /// <summary>
    /// GM
    /// </summary>
    [ClientInterface]
    public interface IGameSystem
    {
        /// <summary>
        /// 获取服务端时间结果
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="time">服务端时间</param>
        [NetMethod((ushort)OpCode.GetSystemTimeResult, NetMethodType.SimpleMethod)]
        void GetSystemTimeResult(NetState netstate, long time);

        /// <summary>
        /// 公告
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="noticeContext"></param>
        [NetMethod((ushort)OpCode.Notice, NetMethodType.SimpleMethod)]
        void Notice(NetState netstate, string noticeContext);

        /// <summary>
        /// 服务器状态
        /// </summary>
        /// <param name="netsate"></param>
        /// <param name="title">标题</param>
        /// <param name="context">内容</param>
        /// <param name="isNoticeOnce">公告是否只播放一次</param>
        /// <param name="isMaintain">是否维护中</param>
        [NetMethod((ushort)OpCode.ServerStatus, NetMethodType.SimpleMethod)]
        void ServerStatus(NetState netsate, string title, string context, bool isNoticeOnce, bool isMaintain);
    }
}
