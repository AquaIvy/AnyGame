
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
    /// 服务器业务逻辑注册管理器
    /// </summary>
    public static class ServerLogicProtoclRegister
    {
        private static readonly List<IProtoclAutoCode> list = new List<IProtoclAutoCode>();

        /// <summary>
        /// 注册所有模块的网络消息到包管理器里
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="handlers"></param>
        public static void Register(ILogicModule[] modules, PacketHandlersBase handlers)
        {
            foreach (var m in modules)
            {
                if (m is AnyGame.Server.Interface.Server.IBag)
                {
                    IProtoclAutoCode pac = new IBagAccess1();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.IBag);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }                if (m is AnyGame.Server.Interface.Server.IGame)
                {
                    IProtoclAutoCode pac = new IGameAccess2();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.IGame);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }                if (m is AnyGame.Server.Interface.Server.IGameSystem)
                {
                    IProtoclAutoCode pac = new IGameSystemAccess3();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.IGameSystem);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }                if (m is AnyGame.Server.Interface.Server.ILogin)
                {
                    IProtoclAutoCode pac = new ILoginAccess4();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.ILogin);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }                if (m is AnyGame.Server.Interface.Server.IShop)
                {
                    IProtoclAutoCode pac = new IShopAccess5();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.IShop);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }
            }
        }
    }



    class IBagAccess1:IProtoclAutoCode
    {
        public PacketHandlersBase PacketHandlerManager {get;set;}

        AnyGame.Server.Interface.Server.IBag module;

        public void SetModule(ILogicModule m)
        {
            if (m == null)
                throw new ArgumentNullException("ILogicModule");
            module = (AnyGame.Server.Interface.Server.IBag)m;
            if (module == null)
            {
                throw new NullReferenceException(string.Format("{0} not AnyGame.Server.Interface.Server.IBag", m.GetType().FullName));
            }
        }


        public void Init()
        {
PacketHandlerManager.Register(1200, OnUseItem);
PacketHandlerManager.Register(1241, OnUpgradeBag);

        }

void OnUseItem(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
module.OnUseItem(netstate,p1,p2);
}
void OnUpgradeBag(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
module.OnUpgradeBag(netstate);
}



    }


    class IGameAccess2:IProtoclAutoCode
    {
        public PacketHandlersBase PacketHandlerManager {get;set;}

        AnyGame.Server.Interface.Server.IGame module;

        public void SetModule(ILogicModule m)
        {
            if (m == null)
                throw new ArgumentNullException("ILogicModule");
            module = (AnyGame.Server.Interface.Server.IGame)m;
            if (module == null)
            {
                throw new NullReferenceException(string.Format("{0} not AnyGame.Server.Interface.Server.IGame", m.GetType().FullName));
            }
        }


        public void Init()
        {
PacketHandlerManager.Register(1, TaskType.Low, Heartbeat);

        }

void Heartbeat(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadInt32();
module.Heartbeat(netstate,p1);
}



    }


    class IGameSystemAccess3:IProtoclAutoCode
    {
        public PacketHandlersBase PacketHandlerManager {get;set;}

        AnyGame.Server.Interface.Server.IGameSystem module;

        public void SetModule(ILogicModule m)
        {
            if (m == null)
                throw new ArgumentNullException("ILogicModule");
            module = (AnyGame.Server.Interface.Server.IGameSystem)m;
            if (module == null)
            {
                throw new NullReferenceException(string.Format("{0} not AnyGame.Server.Interface.Server.IGameSystem", m.GetType().FullName));
            }
        }


        public void Init()
        {
PacketHandlerManager.Register(2, RunGMCommand);
PacketHandlerManager.Register(3, TaskType.Low, GetSystemTime);
PacketHandlerManager.Register(7, TaskType.Low, ClientLog);
PacketHandlerManager.Register(8, TaskType.Low, PhoneInfo);
PacketHandlerManager.Register(9, TaskType.Low, ClientException);
PacketHandlerManager.Register(12, TaskType.Low, ClinetPauseStatus);
PacketHandlerManager.Register(13, TaskType.Low, Heart);

        }

void RunGMCommand(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadUTF8String();
module.RunGMCommand(netstate,p1);
}
void GetSystemTime(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
module.GetSystemTime(netstate);
}
void ClientLog(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadUTF8String();
var p2 = reader.ReadUTF8String();
module.ClientLog(netstate,p1,p2);
}
void PhoneInfo(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
module.PhoneInfo(netstate,p1);
}
void ClientException(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
module.ClientException(netstate,p1);
}
void ClinetPauseStatus(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadBoolean();
var p2 = new DateTime(reader.ReadLong64());
module.ClinetPauseStatus(netstate,p1,p2);
}
void Heart(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
module.Heart(netstate);
}



    }


    class ILoginAccess4:IProtoclAutoCode
    {
        public PacketHandlersBase PacketHandlerManager {get;set;}

        AnyGame.Server.Interface.Server.ILogin module;

        public void SetModule(ILogicModule m)
        {
            if (m == null)
                throw new ArgumentNullException("ILogicModule");
            module = (AnyGame.Server.Interface.Server.ILogin)m;
            if (module == null)
            {
                throw new NullReferenceException(string.Format("{0} not AnyGame.Server.Interface.Server.ILogin", m.GetType().FullName));
            }
        }


        public void Init()
        {
PacketHandlerManager.Register(1000, OnLoginServer);
PacketHandlerManager.Register(1003, OnCreatePlayer);

        }

void OnLoginServer(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
var p2 = reader.ReadUTF8String();
var p3 = reader.ReadInt32();
module.OnLoginServer(netstate,p1,p2,p3);
}
void OnCreatePlayer(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadUTF8String();
var p2 = (AnyGame.Server.Entity.Character.Sex)reader.ReadByte();
module.OnCreatePlayer(netstate,p1,p2);
}



    }


    class IShopAccess5:IProtoclAutoCode
    {
        public PacketHandlersBase PacketHandlerManager {get;set;}

        AnyGame.Server.Interface.Server.IShop module;

        public void SetModule(ILogicModule m)
        {
            if (m == null)
                throw new ArgumentNullException("ILogicModule");
            module = (AnyGame.Server.Interface.Server.IShop)m;
            if (module == null)
            {
                throw new NullReferenceException(string.Format("{0} not AnyGame.Server.Interface.Server.IShop", m.GetType().FullName));
            }
        }


        public void Init()
        {
PacketHandlerManager.Register(1000, OnBugCard);

        }

void OnBugCard(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
module.OnBugCard(netstate);
}



    }

}

