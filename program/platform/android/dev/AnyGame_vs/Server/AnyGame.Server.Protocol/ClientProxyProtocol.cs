
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Server.Core.Net;
using DogSE.Server.Core.Task;
using DogSE.Server.Core.LogicModule;

namespace DogSE.Server.Core.Protocol.AutoCode
{
    /// <summary>
    /// 代理客户端注册
    /// </summary>
    public static class ClientProxyRegister
    {
        /// <summary>
        /// 注册代理类
        /// </summary>
        public static void Register()
        {
AnyGame.Server.Interface.Client.ClientProxy.Login = new ILoginProxy1();
AnyGame.Server.Interface.Client.ClientProxy.Game = new IGameProxy1();
AnyGame.Server.Interface.Client.ClientProxy.Bag = new IBagProxy1();
AnyGame.Server.Interface.Client.ClientProxy.System = new IGameSystemProxy1();

        }
    }

    class IBagProxy1:AnyGame.Server.Interface.Client.IBag
    {
        public void UseItemResult(NetState netstate,AnyGame.Server.Entity.Bags.UseItemResult result,int itemId,int lessCount)
{
var pw = PacketWriter.AcquireContent(1201);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1201 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)result);
pw.Write(itemId);
pw.Write(lessCount);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncItems(NetState netstate,AnyGame.Server.Entity.Common.SyncType type,AnyGame.Server.Entity.Bags.GameItem[] items)
{
var pw = PacketWriter.AcquireContent(1202);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1202 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)type);
int itemslen = items == null ? 0:items.Length;pw.Write(itemslen);
for(int i = 0;i < itemslen ;i++){
GameItemWriteProxy.Write(items[i], pw);
}
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncBag(NetState netstate,int maxGridCount,AnyGame.Server.Entity.Bags.GameItem[] items)
{
var pw = PacketWriter.AcquireContent(1203);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1203 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(maxGridCount);
int itemslen = items == null ? 0:items.Length;pw.Write(itemslen);
for(int i = 0;i < itemslen ;i++){
GameItemWriteProxy.Write(items[i], pw);
}
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncAllResouce(NetState netstate,int money,int gem)
{
var pw = PacketWriter.AcquireContent(1204);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1204 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(money);
pw.Write(gem);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncResouce(NetState netstate,int resId,int num)
{
var pw = PacketWriter.AcquireContent(1205);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1205 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(resId);
pw.Write(num);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void UpgradeBagResult(NetState netstate,AnyGame.Server.Entity.Bags.UpgradeBagResult result)
{
var pw = PacketWriter.AcquireContent(1242);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1242 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)result);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    public class GameItemWriteProxy
    {
        public static void Write(AnyGame.Server.Entity.Bags.GameItem obj, PacketWriter pw)
        {

pw.Write(obj.Id);
pw.Write(obj.TemplateId);
pw.Write(obj.Num);

        }
    }

    }


    class IGameProxy1:AnyGame.Server.Interface.Client.IGame
    {
        public void SyncServerTime(NetState netstate,DateTime serverTime,int id)
{
var pw = PacketWriter.AcquireContent(2);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 2 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(serverTime.Ticks);
pw.Write(id);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    }


    class IGameSystemProxy1:AnyGame.Server.Interface.Client.IGameSystem
    {
        public void GetSystemTimeResult(NetState netstate,long time)
{
var pw = PacketWriter.AcquireContent(4);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 4 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(time);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void Notice(NetState netstate,string noticeContext)
{
var pw = PacketWriter.AcquireContent(10);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 10 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.WriteUTF8Null(noticeContext);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void ServerStatus(NetState netstate,string title,string context,bool isNoticeOnce,bool isMaintain)
{
var pw = PacketWriter.AcquireContent(11);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 11 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.WriteUTF8Null(title);
pw.WriteUTF8Null(context);
pw.Write(isNoticeOnce);
pw.Write(isMaintain);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    }


    class ILoginProxy1:AnyGame.Server.Interface.Client.ILogin
    {
        public void LoginServerResult(NetState netstate,AnyGame.Server.Entity.Login.LoginServerResult result,bool isCreatedPlayer)
{
var pw = PacketWriter.AcquireContent(1001);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1001 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)result);
pw.Write(isCreatedPlayer);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void CreatePlayerResult(NetState netstate,AnyGame.Server.Entity.Login.CraetePlayerResult result)
{
var pw = PacketWriter.AcquireContent(1003);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1003 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)result);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncInitDataFinish(NetState netstate)
{
var pw = PacketWriter.AcquireContent(1006);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1006 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void KickOfServer(NetState netstate,AnyGame.Server.Entity.Login.OfflineType type)
{
var pw = PacketWriter.AcquireContent(1004);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1004 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)type);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    }


}

