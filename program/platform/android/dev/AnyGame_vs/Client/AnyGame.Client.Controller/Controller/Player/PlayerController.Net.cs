

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;

namespace AnyGame.Client.Controller.Player
{


    partial class PlayerController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="net"></param>
        public PlayerController(NetController net)
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
            public ControllerPacketHandler(NetController net, BasePlayerController logic)
            {
                PacketHandlerManager = net.PacketHandlers;

                module = logic;
PacketHandlerManager.Register(1301, OnUnlockGuideRecordResult);
PacketHandlerManager.Register(1302, OnSyncGuideRecords);
PacketHandlerManager.Register(1304, OnUnlockMenuResult);
PacketHandlerManager.Register(1305, OnSyncUnlockMenus);
PacketHandlerManager.Register(1307, OnPlayerRenameResult);
PacketHandlerManager.Register(1308, OnSyncPlayerInfo);
PacketHandlerManager.Register(1309, OnSyncPlayerPropertyInfo);

            }

        public PacketHandlersBase PacketHandlerManager {get;set;}

        BasePlayerController module;


void OnUnlockGuideRecordResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Guide.GuideTypes)reader.ReadByte();
var p2 = reader.ReadBoolean();
module.OnUnlockGuideRecordResult(p1,p2);
}

void OnSyncGuideRecords(NetState netstate, PacketReader reader){
var len1 = reader.ReadInt32();
var p1 = new AnyGame.Client.Entity.Guide.GuideTypes[len1];
for(int i =0;i< len1;i++){
p1[i] = (AnyGame.Client.Entity.Guide.GuideTypes)reader.ReadByte();
}
module.OnSyncGuideRecords(p1);
}

void OnUnlockMenuResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Character.MenuTypes)reader.ReadByte();
var p2 = reader.ReadBoolean();
module.OnUnlockMenuResult(p1,p2);
}

void OnSyncUnlockMenus(NetState netstate, PacketReader reader){
var len1 = reader.ReadInt32();
var p1 = new AnyGame.Client.Entity.Character.MenuTypes[len1];
for(int i =0;i< len1;i++){
p1[i] = (AnyGame.Client.Entity.Character.MenuTypes)reader.ReadByte();
}
module.OnSyncUnlockMenus(p1);
}

void OnPlayerRenameResult(NetState netstate, PacketReader reader){
var p1 = (AnyGame.Client.Entity.Character.RenameResultType)reader.ReadByte();
var p2 = reader.ReadUTF8String();
module.OnPlayerRenameResult(p1,p2);
}

void OnSyncPlayerInfo(NetState netstate, PacketReader reader){
var p1 = reader.ReadUTF8String();
var p2 = reader.ReadInt32();
var p3 = new DateTime(reader.ReadLong64());
var p4 = new DateTime(reader.ReadLong64());
var p5 = new DateTime(reader.ReadLong64());
var p6 = reader.ReadInt32();
var p7 = reader.ReadInt32();
var p8 = reader.ReadLong64();
var p9 = reader.ReadInt32();
module.OnSyncPlayerInfo(p1,p2,p3,p4,p5,p6,p7,p8,p9);
}

void OnSyncPlayerPropertyInfo(NetState netstate, PacketReader reader){
 var p1 = PropertyReadProxy.Read(reader);
module.OnSyncPlayerPropertyInfo(p1);
}



    class PropertyReadProxy
    {
        public static AnyGame.Client.Entity.Character.Property Read(PacketReader reader)
        {
            AnyGame.Client.Entity.Character.Property ret = new AnyGame.Client.Entity.Character.Property();

ret.HP = reader.ReadInt32();
ret.MP = reader.ReadInt32();
ret.Physical = reader.ReadInt32();
ret.Mana = reader.ReadInt32();
ret.Strength = reader.ReadInt32();
ret.Endurance = reader.ReadInt32();
ret.Agility = reader.ReadInt32();


            return ret;
        }
    }

        }



    }


}


