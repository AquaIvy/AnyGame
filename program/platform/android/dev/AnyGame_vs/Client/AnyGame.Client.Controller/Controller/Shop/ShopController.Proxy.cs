
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core.Net;

namespace TradeAge.Client.Controller.Shop
{


    /// <summary>
    /// 
    /// </summary>
    partial class ShopController
    {
                /// <summary>
        /// 
        /// </summary>

public void BugCard()
{
var pw = PacketWriter.AcquireContent(1000);
NetState.Send(pw);PacketWriter.ReleaseContent(pw);
}




    }


}

