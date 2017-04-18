

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;

namespace AnyGame.Client.Controller.Bag
{


    partial class BagController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="net"></param>
        public BagController(NetController net)
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
            public ControllerPacketHandler(NetController net, BaseBagController logic)
            {
                PacketHandlerManager = net.PacketHandlers;

                module = logic;
PacketHandlerManager.Register(1201, OnUseItemResult);
PacketHandlerManager.Register(1202, OnSyncBag);

            }

        public PacketHandlersBase PacketHandlerManager {get;set;}

        BaseBagController module;

void OnUseItemResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Bags.UseItemResult)reader.ReadByte();
var p2 = reader.ReadInt32();
var p3 = reader.ReadInt32();
module.OnUseItemResult(p1,p2,p3);
}
void OnSyncBag(NetState netstate, PacketReader reader){
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
module.OnSyncBag(p1,p2);
}



        }



    }


}


