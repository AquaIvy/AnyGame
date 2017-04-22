

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
PacketHandlerManager.Register(1202, OnSyncItems);
PacketHandlerManager.Register(1203, OnSyncBag);
PacketHandlerManager.Register(1204, OnSyncAllResouce);
PacketHandlerManager.Register(1205, OnSyncResouce);
PacketHandlerManager.Register(1242, OnUpgradeBagResult);

            }

        public PacketHandlersBase PacketHandlerManager {get;set;}

        BaseBagController module;

void OnUseItemResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Character.UseItemResult)reader.ReadByte();
var p2 = reader.ReadInt32();
var p3 = reader.ReadInt32();
module.OnUseItemResult(p1,p2,p3);
}
void OnSyncItems(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Character.SyncType)reader.ReadByte();
var len2 = reader.ReadInt32();
var p2 = new AnyGame.Client.Entity.Character.GameItem[len2];for(int i =0;i< len2;i++){
p2[i] = GameItemReadProxy.Read(reader);
}
module.OnSyncItems(p1,p2);
}
void OnSyncBag(NetState netstate, PacketReader reader){
var p1 = reader.ReadInt32();
var len2 = reader.ReadInt32();
var p2 = new AnyGame.Client.Entity.Character.GameItem[len2];for(int i =0;i< len2;i++){
p2[i] = GameItemReadProxy.Read(reader);
}
module.OnSyncBag(p1,p2);
}
void OnSyncAllResouce(NetState netstate, PacketReader reader){
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
module.OnSyncAllResouce(p1,p2);
}
void OnSyncResouce(NetState netstate, PacketReader reader){
var p1 = reader.ReadInt32();
var p2 = reader.ReadInt32();
module.OnSyncResouce(p1,p2);
}
void OnUpgradeBagResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Character.UpgradeBagResult)reader.ReadByte();
module.OnUpgradeBagResult(p1);
}



    class GameItemReadProxy
    {
        public static AnyGame.Client.Entity.Character.GameItem Read(PacketReader reader)
        {
            AnyGame.Client.Entity.Character.GameItem ret = new AnyGame.Client.Entity.Character.GameItem();

ret.Id = reader.ReadInt32();
ret.TemplateId = reader.ReadInt32();
ret.Num = reader.ReadInt32();


            return ret;
        }
    }

        }



    }


}


