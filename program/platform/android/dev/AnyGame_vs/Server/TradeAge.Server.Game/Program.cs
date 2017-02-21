using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DogSE.Library.Log;
using DogSE.Server.Core;
using DogSE.Server.Core.Config;
using DogSE.Server.Core.Protocol.AutoCode;
using TradeAge.Server.Logic;
using TradeAge.Server.Database;
using AnyGame.Server.Template;
using System.Diagnostics;

namespace TradeAge.Server.Game
{
    /// <summary>
    /// 游戏的启动项目，同时也是一个服务器状态的监视窗口
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //添加一个文件日志适配器
            Logs.ConfigLogFile("tradeage.log");
            //添加一个控制台日志适配器
            Logs.AddAppender(new ConsoleAppender());

            //加载服务器配置文件 Server.config
            StaticConfigFileManager.LoadData(true);

            //加载模版数据
            Templates.LoadTemplate();

            //var itemtmp = Templates.GetItemTemplate(9);
            //Logs.Error("Templates.GetItemTemplate  {0},{1},{2}", itemtmp.Name, itemtmp.IconId, itemtmp.Description);
            //Logs.Error("Templates.GetItemTemplate  {0}", Templates.ItemTemplate.Length);

            //foreach (var item in Templates.CardTemplate)
            //{
            //    Logs.Warn("{0} {1} {2} {3}",
            //        item.Id,
            //        item.Name,
            //        item.Picture,
            //        item.Speed);
            //}

            Logs.Notice("ServerConfig.ServerId  " + ServerConfig.ServerId);
            foreach (var item in ServerConfig.Tcp)
            {
                Logs.Notice("ServerConfig.Tcp  " + item.Host + "  " + item.Port);
            }

            GameServerService.IsConsoleRun = true;
            var world = new WorldBase();
            world.IsAutoRegisterMessage = false;
            world.NetStateDisconnect += world_NetStateDisconnect;
            DB.Init();

            GameServerService.AfterModuleInit = () =>
            {
                ServerLogicProtoclRegister.Register(world.GetModules(), world.PacketHandlers);
                ClientProxyRegister.Register();
                return true;
            };
            LogicModule.Prints();
            GameServerService.StartGame(world);

        }

        static void world_NetStateDisconnect(object sender, NetStateDisconnectEventArgs e)
        {
            //  网络连接断开
            if (e.NetState != null)
            {
                //Logs.Info("{0} close socket.", e.NetState.Serial);
                //Logs.Info("{0} close socket.", e.NetState.NetAddress);
            }
        }
    }
}
