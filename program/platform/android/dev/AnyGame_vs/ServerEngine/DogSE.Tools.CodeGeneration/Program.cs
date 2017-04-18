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
            ServerLogicProtocolGeneration.CreateCode(@"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                        @"..\..\..\..\Server\AnyGame.Server.Protocol\ServerLogicProtocol.cs");

            ClientProxyProtocolGeneration.CreateCode(@"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                        @"..\..\..\..\Server\AnyGame.Server.Protocol\ClientProxyProtocol.cs");
        }

        static void CreateClientCode()
        {
            ClientLogicProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                @"..\..\..\..\Client\AnyGame.Client.Controller\",
                "AnyGame.Client");


            ServerProxyProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                @"..\..\..\..\Client\AnyGame.Client.Controller\",
                "AnyGame.Client");
        }
    }
}
