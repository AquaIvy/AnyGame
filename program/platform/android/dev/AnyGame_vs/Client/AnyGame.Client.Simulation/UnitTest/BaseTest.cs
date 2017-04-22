using AnyGame.Client.Template;
using DogSE.Client.Core;
using DogSE.Library.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AnyGame.Client.Controller;
using AnyGame.Client.Controller.Login;
using AnyGame.Client.Entity.Character;
using AnyGame.Client.Entity.Login;

namespace AnyGame.Client.Simulation.UnitTest
{
    /// <summary>
    /// 所有测试用例的基类
    /// </summary>
    public class BaseTest
    {
        public string Host = "192.168.2.84";
        public int Port = 4601;

        public readonly GameController controller = new GameController();

        const string ConfigFile = "Test.config";

        protected bool IsLoginSuccess;
        protected string AccountName;



        public BaseTest()
        {
            if (File.Exists(ConfigFile))
            {
                var lines = File.ReadAllLines(ConfigFile);
                if (lines.Length > 0)
                    Host = lines[0];
                if (lines.Length > 1)
                    Port = int.Parse(lines[1]);
            }

            Logs.AddConsoleAppender();

            AccountName = Guid.NewGuid().ToString().Substring(0, 8);
            //var files = Directory.GetFiles(@"..\..\..\..\Server\AnyGame.Server.Game\bin\ConfigData\");
            Templates.LoadTemplate(@"..\..\..\..\Server\AnyGame.Server.Game\bin\ConfigData\");
        }

        /// <summary>
        /// 测试结束
        /// </summary>
        [TestCleanup]
        public void FinishTest()
        {
            Logs.Info("test finish.");

            controller.Net.StopWorld();
            controller.Net.NetState.Dispose();
        }

        /// <summary>
        /// 使用随机名字登陆游戏
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoginServerUseRandomName()
        {
            //WebClient wc = new WebClient();
            //var bytes = wc.DownloadData("http://192.168.2.83:81/Login/Api/Fishluv.aspx?uid=" + UserName);
            //var xml = Encoding.UTF8.GetString(bytes);

            //string loginToken = string.Empty;
            //try
            //{
            //    XElement root = XElement.Parse(xml);

            //    var notice = root;
            //    loginToken = notice.Element("LoginToken").Value;
            //}
            //catch (Exception ex)
            //{
            //    Logs.Error("init GameServerStatus xml fail." + ex.ToString());
            //}


            controller.Net.StartWorld();
            controller.Net.NetStateConnect += Net_NetStateConnect;
            controller.Net.NetStateDisconnect += Net_NetStateDisconnect;
            controller.Net.ConnectServer(Host, Port);

            Console.WriteLine("Host:" + Host);
            Console.WriteLine("Port:" + Port);

            return await WaitIsTrue(() => IsLoginSuccess, 10);
        }


        private void Net_NetStateConnect(object sender, NetStateConnectEventArgs e)
        {
            Assert.IsTrue(e.IsConnected, "服务器连接失败。");

            controller.Login.LoginServerResultEvent += Login_LoginServerResultEvent;
            controller.Login.LoginServer(AccountName, "123456", 0);
        }

        private void Net_NetStateDisconnect(object sender, NetStateDisconnectEventArgs e)
        {

        }

        private void Login_LoginServerResultEvent(object sender, LoginServerResultEventArgs e)
        {
            Assert.AreEqual(LoginServerResult.Success, e.Result, "用账号 {0} 登陆游戏失败。");
            Assert.IsFalse(e.IsCreatedPlayer, "新建的账号角色怎么会有？");

            controller.Login.SyncInitDataFinishEvent += Login_SyncInitDataFinishEvent;
            controller.Login.CreatePlayerResultEvent += Login_CreatePlayerResultEvent;
            controller.Login.CreatePlayer(AccountName, Sex.Male);
        }

        private void Login_CreatePlayerResultEvent(object sender, CreatePlayerResultEventArgs e)
        {
            Assert.AreEqual(CraetePlayerResult.Success, e, "创建角色失败。这个你懂的");
        }

        private void Login_SyncInitDataFinishEvent(object sender, EventArgs e)
        {
            //  登陆成功除了创建成功外，还需要有这个方法才说明玩家数据都初始化好
            //  同步到客户端了
            IsLoginSuccess = true;
        }



        /// <summary>
        /// 等待一个方法返回true
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="timeOutSec"></param>
        /// <returns></returns>
        public async Task<bool> WaitIsTrue(Func<bool> fun, int timeOutSec = 2)
        {
            int waitMS = timeOutSec * 1000;
            bool ret = fun();
            while (ret == false)
            {
                ret = fun();

                if (ret == false)
                {
                    NetController.TaskManager.RunAllTask();
                    Thread.Sleep(100);
                    waitMS -= 100;
                }

                if (waitMS < 0)
                {
                    ret = fun();
                    break;
                }
            }

            return ret;
        }
    }
}
