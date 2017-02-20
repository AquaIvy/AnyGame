
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace TradeAge.Client.Controller.Bag
{


    /// <summary>
    /// 
    /// </summary>
    partial class BagController
    {
                /// <summary>
        /// 
        /// </summary>
/// <param name="itemId"></param>
/// <param name="useCount"></param>

public void UseItem(int itemId,int useCount)
{
var pw = PacketWriter.AcquireContent(1200);
pw.Write(itemId);
pw.Write(useCount);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

