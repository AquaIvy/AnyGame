
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace AnyGame.Client.Controller.GameSystem
{


    /// <summary>
    /// 
    /// </summary>
    partial class GameSystemController
    {
                /// <summary>
        /// 执行一个gm方法
        /// </summary>
/// <param name="command"></param>

public void RunGMCommand(string command)
{
var pw = PacketWriter.AcquireContent(2);
pw.WriteUTF8Null(command);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 获取服务端时间请求
        /// </summary>

public void GetSystemTime()
{
var pw = PacketWriter.AcquireContent(3);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 客户端日志数据
        /// </summary>
/// <param name="type">日志类型</param>
/// <param name="context">日志内容</param>

public void ClientLog(string type,string context)
{
var pw = PacketWriter.AcquireContent(7);
pw.WriteUTF8Null(type);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 客户端通知手机的基本信息
        /// </summary>
/// <param name="context">内容</param>

public void PhoneInfo(string context)
{
var pw = PacketWriter.AcquireContent(8);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 客户端的异常通知
        /// </summary>
/// <param name="context">内容</param>

public void ClientException(string context)
{
var pw = PacketWriter.AcquireContent(9);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 客户端的暂停状态
        /// </summary>
/// <param name="isPause"></param>
/// <param name="time">暂停和恢复的时间</param>

public void ClinetPauseStatus(bool isPause,DateTime time)
{
var pw = PacketWriter.AcquireContent(12);
pw.Write(isPause);
pw.Write(time.Ticks);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 心跳包
        /// </summary>

public void Heart()
{
var pw = PacketWriter.AcquireContent(13);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

