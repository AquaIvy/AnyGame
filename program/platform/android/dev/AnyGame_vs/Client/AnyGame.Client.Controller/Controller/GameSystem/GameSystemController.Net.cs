

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;

namespace AnyGame.Client.Controller.GameSystem
{


    partial class GameSystemController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="net"></param>
        public GameSystemController(NetController net)
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
            public ControllerPacketHandler(NetController net, BaseGameSystemController logic)
            {
                PacketHandlerManager = net.PacketHandlers;

                module = logic;
PacketHandlerManager.Register(4, OnGetSystemTimeResult);
PacketHandlerManager.Register(10, OnNotice);
PacketHandlerManager.Register(11, OnServerStatus);

            }

        public PacketHandlersBase PacketHandlerManager {get;set;}

        BaseGameSystemController module;

void OnGetSystemTimeResult(NetState netstate, PacketReader reader){
var p1 = reader.ReadLong64();
module.OnGetSystemTimeResult(p1);
}
void OnNotice(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
module.OnNotice(p1);
}
void OnServerStatus(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
var p2 = reader.ReadUTF8String();
var p3 = reader.ReadBoolean();
var p4 = reader.ReadBoolean();
module.OnServerStatus(p1,p2,p3,p4);
}



        }



    }


}


