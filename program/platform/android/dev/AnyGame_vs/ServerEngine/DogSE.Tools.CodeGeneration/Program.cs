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

            Console.ReadKey();
        }

        /// <summary>
        /// 生成服务端的代码
        /// </summary>
        static void CreateServerCode()
        {
            //服务端部分的 Client -> Server    收到客户端的请求
            ServerLogicProtocolGeneration.CreateCode(@"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                        @"..\..\..\..\Server\AnyGame.Server.Protocol\ServerLogicProtocol.cs");

            //服务端部分的 Server -> Client    将结果下发给客户端
            ClientProxyProtocolGeneration.CreateCode(@"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                        @"..\..\..\..\Server\AnyGame.Server.Protocol\ClientProxyProtocol.cs");
        }

        static void CreateClientCode()
        {
            //客户端部分的 Server -> Client   操作返回
            ClientLogicProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                @"..\..\..\..\Client\AnyGame.Client.Controller\",
                "AnyGame.Client");

            //客户端部分的 Client -> Server   客户端操作
            ServerProxyProtocolGeneration.CreateCode(
                @"..\..\..\..\Server\AnyGame.Server.Interface\bin\Debug\AnyGame.Server.Interface.dll",
                @"..\..\..\..\Client\AnyGame.Client.Controller\",
                "AnyGame.Client");
        }
    }
}
