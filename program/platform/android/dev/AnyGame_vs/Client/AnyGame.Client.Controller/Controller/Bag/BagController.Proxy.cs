
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace AnyGame.Client.Controller.Bag
{


    /// <summary>
    /// 
    /// </summary>
    partial class BagController
    {
                /// <summary>
        /// 使用物品
        /// </summary>
/// <param name="itemId">该物品的唯一id，非物品模版id</param>
/// <param name="useCount"></param>

public void UseItem(int itemId,int useCount)
{
var pw = PacketWriter.AcquireContent(1200);
pw.Write(itemId);
pw.Write(useCount);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}

        /// <summary>
        /// 升级背包
        /// </summary>

public void UpgradeBag()
{
var pw = PacketWriter.AcquireContent(1241);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

