
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
AnyGame.Server.Interface.Client.ClientProxy.System = new IGameSystemProxy1();
AnyGame.Server.Interface.Client.ClientProxy.Player = new IPlayerProxy1();
AnyGame.Server.Interface.Client.ClientProxy.Bag = new IBagProxy1();

        }
    }

    class IPlayerProxy1:AnyGame.Server.Interface.Client.IPlayer
    {
        public void UnlockGuideRecordResult(NetState netstate,AnyGame.Server.Entity.Guide.GuideTypes type,bool isPass)
{
var pw = PacketWriter.AcquireContent(1301);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1301 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)type);
pw.Write(isPass);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncGuideRecords(NetState netstate,AnyGame.Server.Entity.Guide.GuideTypes[] records)
{
var pw = PacketWriter.AcquireContent(1302);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1302 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                int recordslen = records == null ? 0:records.Length;pw.Write(recordslen);
for(int i = 0;i < recordslen ;i++){
pw.Write((byte)records[i]);
}
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void UnlockMenuResult(NetState netstate,AnyGame.Server.Entity.Character.MenuTypes menu,bool isUnlock)
{
var pw = PacketWriter.AcquireContent(1304);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1304 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)menu);
pw.Write(isUnlock);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncUnlockMenus(NetState netstate,AnyGame.Server.Entity.Character.MenuTypes[] menus)
{
var pw = PacketWriter.AcquireContent(1305);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1305 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                int menuslen = menus == null ? 0:menus.Length;pw.Write(menuslen);
for(int i = 0;i < menuslen ;i++){
pw.Write((byte)menus[i]);
}
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void PlayerRenameResult(NetState netstate,AnyGame.Server.Entity.Character.RenameResultType result,string newName)
{
var pw = PacketWriter.AcquireContent(1307);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1307 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write((byte)result);
pw.WriteUTF8Null(newName);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncPlayerInfo(NetState netstate,string name,int sex,DateTime createTime,DateTime lastLoginTime,DateTime lastLogoffTime,int level,int exp,long expSum,int vipLevel)
{
var pw = PacketWriter.AcquireContent(1308);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1308 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.WriteUTF8Null(name);
pw.Write(sex);
pw.Write(createTime.Ticks);
pw.Write(lastLoginTime.Ticks);
pw.Write(lastLogoffTime.Ticks);
pw.Write(level);
pw.Write(exp);
pw.Write(expSum);
pw.Write(vipLevel);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}

public void SyncPlayerPropertyInfo(NetState netstate,AnyGame.Server.Entity.Character.Property property)
{
var pw = PacketWriter.AcquireContent(1309);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1309 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                PropertyWriteProxy.Write(property, pw);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    public class PropertyWriteProxy
    {
        public static void Write(AnyGame.Server.Entity.Character.Property obj, PacketWriter pw)
        {

pw.Write(obj.HP);
pw.Write(obj.MP);
pw.Write(obj.Physical);
pw.Write(obj.Mana);
pw.Write(obj.Strength);
pw.Write(obj.Endurance);
pw.Write(obj.Agility);

        }
    }

    }


    class IBagProxy1:AnyGame.Server.Interface.Client.IBag
    {
        public void UseItemResult(NetState netstate,AnyGame.Server.Entity.Character.UseItemResult result,int itemId,int lessCount)
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

public void SyncItems(NetState netstate,AnyGame.Server.Entity.Character.SyncType type,AnyGame.Server.Entity.Character.GameItem[] items)
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

public void SyncBag(NetState netstate,int maxGridCount,AnyGame.Server.Entity.Character.GameItem[] items)
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

public void UpgradeBagResult(NetState netstate,AnyGame.Server.Entity.Character.UpgradeBagResult result)
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
        public static void Write(AnyGame.Server.Entity.Character.GameItem obj, PacketWriter pw)
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

public void SyncPlayerBaseInfo(NetState netstate,int playerId,int gameZoonId,bool isSupperMan,int platformType)
{
var pw = PacketWriter.AcquireContent(1011);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile( 1011 );
            if ( packetProfile != null )
                packetProfile.RegConstruct();
                pw.Write(playerId);
pw.Write(gameZoonId);
pw.Write(isSupperMan);
pw.Write(platformType);
netstate.Send(pw);
 if ( packetProfile != null ) packetProfile.Record(pw.Length);
PacketWriter.ReleaseContent(pw);
}




    }


}

