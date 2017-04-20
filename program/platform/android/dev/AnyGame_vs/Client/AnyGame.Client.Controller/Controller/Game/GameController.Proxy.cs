
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace AnyGame.Client.Controller.Game
{


    /// <summary>
    /// 
    /// </summary>
    partial class GameController
    {
                /// <summary>
        /// 客户端过来的心跳包
        /// </summary>
/// <param name="id">心跳包id，服务器确认的时候，把这个返回给客户端</param>

public void Heartbeat(int id)
{
var pw = PacketWriter.AcquireContent(1);
pw.Write(id);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

