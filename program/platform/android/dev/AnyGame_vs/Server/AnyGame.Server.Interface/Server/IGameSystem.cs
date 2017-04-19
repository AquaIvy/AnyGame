using AnyGame.Server.Entity.NetCode;
using DogSE.Server.Core.LogicModule;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Interface.Server
{
    /// <summary>
    /// GM 接口
    /// </summary>
    public interface IGameSystem : ILogicModule
    {
        /// <summary>
        /// 执行一个gm方法
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="command"></param>
        [NetMethod((ushort)OpCode.RunGMCommand, NetMethodType.SimpleMethod)]
        void RunGMCommand(NetState netstate, string command);

        /// <summary>
        /// 获取服务端时间请求
        /// </summary>
        /// <param name="netstate"></param>
        [NetMethod((ushort)OpCode.GetSystemTime, NetMethodType.SimpleMethod, TaskType.Low)]
        void GetSystemTime(NetState netstate);

        /// <summary>
        /// 客户端日志数据
        /// </summary>
        /// <param name="netstate"></param>
        /// <param name="type">日志类型</param>
        /// <param name="context">日志内容</param>
        [NetMethod((ushort)OpCode.ClientLog, NetMethodType.SimpleMethod, TaskType.Low)]
        void ClientLog(NetState netstate, string type, string context);

        /// <summary>
        /// 客户端通知手机的基本信息
        /// </summary>
        /// <param name="netstate">网络连接</param>
        /// <param name="context">内容</param>
        [NetMethod((ushort)OpCode.PhoneInfo, NetMethodType.SimpleMethod, TaskType.Low, false)]
        void PhoneInfo(NetState netstate, string context);

        /// <summary>
        /// 客户端的异常通知
        /// </summary>
        /// <param name="netstate">网络连接</param>
        /// <param name="context">内容</param>
        [NetMethod((ushort)OpCode.ClientException, NetMethodType.SimpleMethod, TaskType.Low, false)]
        void ClientException(NetState netstate, string context);

        /// <summary>
        /// 客户端的暂停状态
        /// </summary>
        /// <param name="netsate"></param>
        /// <param name="isPause"></param>
        /// <param name="time">暂停和恢复的时间</param>
        [NetMethod((ushort)OpCode.ClinetPauseStatus, NetMethodType.SimpleMethod, TaskType.Low)]
        void ClinetPauseStatus(NetState netsate, bool isPause, DateTime time);

        /// <summary>
        /// 心跳包
        /// </summary>
        /// <param name="netstate"></param>

        [NetMethod((ushort)OpCode.Heart2, NetMethodType.SimpleMethod, TaskType.Low)]
        void Heart(NetState netstate);

    }
}
