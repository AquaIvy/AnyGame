using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DogSE.Library.Log;
using DogSE.Tools.CodeGeneration.Client.Unity3d;
using DogSE.Tools.CodeGeneration.Server;

namespace DogSE.Tools.CodeGeneration
{
    /// <summary>
    /// 代码生成
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CreateServerCode();
            CreateClientCode();
        }

        static void CreateServerCode()
        {
            ServerLogicProtocolGeneration.CreateCode(@"..\..\..\..\Server\TradeAge.Server.Interface\bin\Debug\TradeAge.Server.Interface.dll",
                        @"..\..\..\..\Server\TradeAge.Server.Protocol\ServerLogicProtocol.cs");

            ClientProxyProtocolGeneration.CreateCode(@"..\..\..\..\Server\TradeAge.Server.Interface\bin\Debug\TradeAge.Server.Interface.dll",
                        @"..\..\..\..\Server\TradeAge.Server.Protocol\ClientProxyProtocol.cs");
        }

        static void CreateClientCode()
        {
            ClientLogicProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\TradeAge.Server.Interface\bin\Debug\TradeAge.Server.Interface.dll",
                @"..\..\..\..\Client\TradeAge.Client.Controller\",
                "TradeAge.Client");


            ServerProxyProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\TradeAge.Server.Interface\bin\Debug\TradeAge.Server.Interface.dll",
                @"..\..\..\..\Client\TradeAge.Client.Controller\",
                "TradeAge.Client");
        }
    }
}
