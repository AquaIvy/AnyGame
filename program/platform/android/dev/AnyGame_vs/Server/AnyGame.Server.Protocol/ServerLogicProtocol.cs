﻿
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
                }                if (m is AnyGame.Server.Interface.Server.ILogin)
                {
                    IProtoclAutoCode pac = new ILoginAccess3();
                    list.Add(pac);

                    pac.SetModule(m as AnyGame.Server.Interface.Server.ILogin);
                    pac.PacketHandlerManager = handlers;
                    pac.Init();
                }                if (m is AnyGame.Server.Interface.Server.IShop)
                {
                    IProtoclAutoCode pac = new IShopAccess4();
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

        }

void OnUseItem(NetState netstate, PacketReader reader){
if (!netstate.IsVerifyLogin) return;
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
module.OnUseItem(netstate,p1,p2);
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


    class ILoginAccess3:IProtoclAutoCode
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


    class IShopAccess4:IProtoclAutoCode
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

