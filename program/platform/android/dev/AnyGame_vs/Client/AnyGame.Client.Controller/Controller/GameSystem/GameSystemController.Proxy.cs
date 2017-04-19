
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
        /// 
        /// </summary>
/// <param name="command"></param>

public void RunGMCommand(string command)
{
var pw = PacketWriter.AcquireContent(2);
pw.WriteUTF8Null(command);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>

public void GetSystemTime()
{
var pw = PacketWriter.AcquireContent(3);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>
/// <param name="type"></param>
/// <param name="context"></param>

public void ClientLog(string type,string context)
{
var pw = PacketWriter.AcquireContent(7);
pw.WriteUTF8Null(type);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>
/// <param name="context"></param>

public void PhoneInfo(string context)
{
var pw = PacketWriter.AcquireContent(8);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>
/// <param name="context"></param>

public void ClientException(string context)
{
var pw = PacketWriter.AcquireContent(9);
pw.WriteUTF8Null(context);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>
/// <param name="isPause"></param>
/// <param name="time"></param>

public void ClinetPauseStatus(bool isPause,DateTime time)
{
var pw = PacketWriter.AcquireContent(12);
pw.Write(isPause);
pw.Write(time.Ticks);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 
        /// </summary>

public void Heart()
{
var pw = PacketWriter.AcquireContent(13);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

