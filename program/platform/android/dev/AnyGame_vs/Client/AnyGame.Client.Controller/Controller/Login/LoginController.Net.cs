﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;

namespace AnyGame.Client.Controller.Login
{


    partial class LoginController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="net"></param>
        public LoginController(NetController net)
        {
            nc = net;
            new ControllerPacketHandler(net, this);
        }

        private NetController nc;


        private NetState NetState
        {
            get
            {
                return nc.NetState;
            }
        }



        class ControllerPacketHandler
        {
            public ControllerPacketHandler(NetController net, BaseLoginController logic)
            {
                PacketHandlerManager = net.PacketHandlers;

                module = logic;
PacketHandlerManager.Register(1001, OnLoginServerResult);
PacketHandlerManager.Register(1003, OnCreatePlayerResult);
PacketHandlerManager.Register(1006, OnSyncInitDataFinish);
PacketHandlerManager.Register(1004, OnKickOfServer);
PacketHandlerManager.Register(1011, OnSyncPlayerBaseInfo);

            }

        public PacketHandlersBase PacketHandlerManager {get;set;}

        BaseLoginController module;


void OnLoginServerResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Login.LoginServerResult)reader.ReadByte();
var p2 = reader.ReadBoolean();
module.OnLoginServerResult(p1,p2);
}

void OnCreatePlayerResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Login.CraetePlayerResult)reader.ReadByte();
module.OnCreatePlayerResult(p1);
}

void OnSyncInitDataFinish(NetState netstate, PacketReader reader){
module.OnSyncInitDataFinish();
}

void OnKickOfServer(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Login.OfflineType)reader.ReadByte();
module.OnKickOfServer(p1);
}

void OnSyncPlayerBaseInfo(NetState netstate, PacketReader reader){
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
var p3 = reader.ReadBoolean();
var p4 = reader.ReadInt32();
module.OnSyncPlayerBaseInfo(p1,p2,p3,p4);
}



        }



    }


}


