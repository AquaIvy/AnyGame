
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace AnyGame.Client.Controller.Player
{


    /// <summary>
    /// 
    /// </summary>
    partial class PlayerController
    {
                /// <summary>
        /// 解锁某个新手引导
        /// </summary>
/// <param name="type">新手引导类型</param>

public void UnlockGuideRecord(AnyGame.Client.Entity.Guide.GuideTypes type)
{
var pw = PacketWriter.AcquireContent(1300);
pw.Write((byte)type);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 解锁某个功能菜单
        /// </summary>
/// <param name="menu">菜单类型</param>

public void UnlockMenu(AnyGame.Client.Entity.Character.MenuTypes menu)
{
var pw = PacketWriter.AcquireContent(1303);
pw.Write((byte)menu);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 玩家改名
        /// </summary>
/// <param name="newName">新名字</param>

public void PlayerRename(string newName)
{
var pw = PacketWriter.AcquireContent(1306);
pw.WriteUTF8Null(newName);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

