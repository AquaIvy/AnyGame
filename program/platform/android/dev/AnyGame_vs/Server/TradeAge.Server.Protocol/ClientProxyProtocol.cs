
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

        }
    }

    class IBagProxy1 : AnyGame.Server.Interface.Client.IBag
    {
        public void UseItemResult(NetState netstate, AnyGame.Server.Entity.Bags.UseItemResult result, int itemId, int lessCount)
        {
            var pw = PacketWriter.AcquireContent(1201);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(1201);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            pw.Write((byte)result);
            pw.Write(itemId);
            pw.Write(lessCount);
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }

        public void SyncBag(NetState netstate, int MaxCount, int CurCount)
        {
            var pw = PacketWriter.AcquireContent(1202);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(1202);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            pw.Write(MaxCount);
            pw.Write(CurCount);
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }




    }


    class IGameProxy1 : AnyGame.Server.Interface.Client.IGame
    {
        public void SyncServerTime(NetState netstate, DateTime serverTime, int id)
        {
            var pw = PacketWriter.AcquireContent(2);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(2);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            pw.Write(serverTime.Ticks);
            pw.Write(id);
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }




    }


    class ILoginProxy1 : AnyGame.Server.Interface.Client.ILogin
    {
        public void LoginServerResult(NetState netstate, AnyGame.Server.Entity.Login.LoginServerResult result, bool isCreatePlayer)
        {
            var pw = PacketWriter.AcquireContent(1001);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(1001);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            pw.Write((byte)result);
            pw.Write(isCreatePlayer);
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }

        public void CreatePlayerResult(NetState netstate, AnyGame.Server.Entity.Login.CraetePlayerResult result)
        {
            var pw = PacketWriter.AcquireContent(1003);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(1003);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            pw.Write((byte)result);
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }

        public void SyncInitDataFinish(NetState netstate)
        {
            var pw = PacketWriter.AcquireContent(1004);
            PacketProfile packetProfile = PacketProfile.GetOutgoingProfile(1004);
            if (packetProfile != null)
                packetProfile.RegConstruct();
            netstate.Send(pw);
            if (packetProfile != null) packetProfile.Record(pw.Length);
            PacketWriter.ReleaseContent(pw);
        }




    }


}

