
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace AnyGame.Client.Controller.Login
{


    /// <summary>
    /// 
    /// </summary>
    partial class LoginController
    {
                /// <summary>
        /// 登陆服务器
        /// </summary>
/// <param name="accountName"></param>
/// <param name="password"></param>
/// <param name="serverId">服务器id</param>

public void LoginServer(string accountName,string password,int serverId)
{
var pw = PacketWriter.AcquireContent(1000);
pw.WriteUTF8Null(accountName);
pw.WriteUTF8Null(password);
pw.Write(serverId);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 创建角色
        /// </summary>
/// <param name="playerName"></param>
/// <param name="sex">性别</param>

public void CreatePlayer(string playerName,AnyGame.Client.Entity.Character.Sex sex)
{
var pw = PacketWriter.AcquireContent(1003);
pw.WriteUTF8Null(playerName);
pw.Write((byte)sex);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

