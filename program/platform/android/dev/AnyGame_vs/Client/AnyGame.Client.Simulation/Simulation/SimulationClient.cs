using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using AnyGame.Client.Controller;
using AnyGame.Client.Controller.Login;
using AnyGame.Client.Entity.Character;
using AnyGame.Client.Entity.Login;

namespace AnyGame.Client.Simulation.Simulation
{
    /// <summary>
    /// 模拟客户端
    /// </summary>
    public class SimulationClient
    {
        public string Host = "192.168.2.83";
        public int Port = 4530;
        public string LoginBaseUrl = "http://192.168.2.83:81/Login/Api/Fishluv.aspx?uid=";
        string loginToken = string.Empty;

        GameController controller = new GameController();



        /// <summary>
        /// 开始进行登录
        /// </summary>
        public void StartLogin()
        {
            if (string.IsNullOrEmpty(AccountName))
            {
                AccountName = Guid.NewGuid().ToString().Substring(0, 8) + (accountIndex++).ToString();
            }

            controller.Net.StartWorld();
            controller.Net.NetStateConnect += OnNetStateConnect;
            controller.Net.NetStateDisconnect += Net_NetStateDisconnect;

            controller.Net.ConnectServer(Host, Port);

            if (loginToken == string.Empty)
                GetToken();

            //controller.Login.LoginServer(AccountName, "123456", 0);

            //  已15s为一个周期进行服务器验证
            WaitIsTrue(() => IsLoginSuccess | IsError, 15);
        }

        public void GetToken()
        {
            //WebClient wc = new WebClient();
            //wc.Encoding = Encoding.UTF8;
            //var xml = wc.DownloadString(LoginBaseUrl + AccountName);

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
        }

        void Net_NetStateDisconnect(object sender, NetStateDisconnectEventArgs e)
        {
            Logs.Error("net close {0}", AccountName);
            IsLoginSuccess = false;
        }

        public bool IsError { get; set; }

        public bool IsLoginSuccess;

        /// <summary>
        /// 账号名
        /// </summary>
        public string AccountName { get; set; }


        private static int accountIndex = 0;

        void OnNetStateConnect(object sender, NetStateConnectEventArgs e)
        {
            if (e.IsConnected)
            {
                controller.Login.LoginServerResultEvent += Login_LoginServerResultEvent;

                controller.Login.LoginServer(AccountName, "123456", 0);
            }
            else
            {
                Logs.Error("登陆失败。服务器无法连接");
                IsError = true;
            }
        }

        private void Login_LoginServerResultEvent(object sender, LoginServerResultEventArgs e)
        {
            if (e.Result == LoginServerResult.Success)
            {
                if (!e.IsCreatedPlayer)
                {
                    controller.Login.CreatePlayerResultEvent += Login_CreatePlayerResultEvent;
                    controller.Login.CreatePlayer(AccountName, Sex.Male);
                }
                else
                {
                    Console.WriteLine("{0} 玩家登陆已有账号成功", AccountName);
                    Logs.Info("{0} 玩家登陆已有账号成功", AccountName);
                    IsLoginSuccess = true;
                    RandomReward();
                }
            }
            else
            {
                Logs.Error("登陆失败，登陆接口返回 {0}", e.Result.ToString());
                IsError = true;
            }
        }

        private void Login_CreatePlayerResultEvent(object sender, CreatePlayerResultEventArgs e)
        {
            if (e.Result == CraetePlayerResult.Success)
            {
                Logs.Info("{0} 登陆并创建角色完成", AccountName);
                IsLoginSuccess = true;
                InitControllerEvent();

                RunGM("showmethemoney 3000");
                RandomReward();
            }
            else
            {
                Logs.Error("登陆失败，创建角色返回 {0}", e.ToString());
                IsError = true;
            }
        }

        void RunGM(string command)
        {

            //controller.System.RunGMCommand(command);
        }

        #region 一些事件的响应和日志输出

        /// <summary>
        /// 一个简单的通用写日志类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="evenetName"></param>
        void WriteResultLog<T>(T type, string evenetName)
        {
            错误计数 += Convert.ToInt32(type) == 0 ? 0 : 1;
            Logs.Info("{0} {1} result: {2}", controller.Model.Player.Name, evenetName, type.ToString());
        }

        /// <summary>
        /// 初始化一些相关事件
        /// </summary>
        void InitControllerEvent()
        {
            //controller.Adventure.OpenGameLevelEvent += OpenGameLevelEvent;
            //controller.Adventure.AdventureEventResultEvent += AdventureEventResultEvent;
            //controller.Adventure.OpenGameBoxEvent += Adventure_OpenGameBoxEvent;
            //controller.Hero.WorkResultEvent += Hero_WorkResultEvent;
            //controller.Hero.RestResultEvent += Hero_RestResultEvent;
            //controller.Hero.UnEmployResultEvent += Hero_UnEmployResultEvent;
            //controller.Rewards.RewardResultsEvent += Rewards_RewardResultsEvent;
            //controller.Bag.UseItemReturn += Bag_UseItemReturn;

            //controller.Player.EatFoodResultEvent += Player_EatFoodResultEvent;
            //controller.Player.BuyFoodResultEvent += Player_BuyFoodResultEvent;
            //controller.Hero.UpgradeWeaponReturn += Hero_UpgradeWeaponReturn;
            //controller.Hero.AwakeReturn += Hero_AwakeReturn;
            //controller.BarrageChat.SyncBarrageMessage += BarrageChat_SyncBarrageMessage;
        }

        #endregion

        /// <summary>
        /// 等待一个方法返回true
        /// </summary>
        /// <param name="fun"></param>
        /// <param name="timeOutSec"></param>
        /// <returns></returns>
        public bool WaitIsTrue(Func<bool> fun, int timeOutSec = 1)
        {
            int waitMS = timeOutSec * 1000;
            bool ret = fun();
            while (ret == false)
            {
                ret = fun();

                if (ret == false)
                {
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

        public void Stop()
        {
            try
            {
                controller.Net.StopWorld();
            }
            catch (Exception ex)
            {
                Logs.Error(ex.ToString());
            }

        }

        private DateTime nextUpdateTime;


        public DateTime Now = DateTime.Now;

        #region 随机操作

        private static string[] gms = new[]
        {
            "showmethemoney 3000",
            "showmethemoney 30000",
            "showmethemoney 300000",
            "addstatue 1",
            "addstatue 3",
            "addstatue 5",
            "addresource 100,20",
            "addresource 101,20",
            "addresource 102,20",
            "addresource 103,20",
            "addresource 104,20",
            "addresource 105,20",
            "addresource 106,20",
            "addfregments 50",
            "addfregments 150",
            "addfregments 250",
            "addexp 5000",
            "addexp 50000",
            "addexp 500000",
        };
        /// <summary>
        /// 给自己发一点随机奖励
        /// </summary>
        void RandomReward()
        {
            //for (int i = 0; i < gms.Length; i++)
            //{
            //    var index = rand.Next(0, gms.Length);
            //    var msg = gms[index];
            //    controller.System.RunGMCommand(msg);
            //}            
        }

        /// <summary>
        /// 做一些模拟客户端工作
        /// </summary>
        public void DoSomething()
        {
            if (Now > nextUpdateTime)
            {
                //controller.Player.RefreshPlayer();
                //SelectGameLevel();
                //SelectEvent();
                //SelectGameBox();
                //DoHeroEmploy();
                //UseItem();
                //EatFood();
                //UpgradeWeapon();
                //Awake();
                //Chat();

                ////测试挖矿
                //TestMiningMapDig();

                //nextUpdateTime = Now.AddSeconds(rand.Next(5, 10));
                //nextUpdateTime = Now.AddSeconds(rand.Next(3, 8));
                nextUpdateTime = Now.AddSeconds(rand.Next(10, 20));
            }
        }

        static Random rand = new Random((int)DateTime.Now.Ticks + new Random().Next(1000));

        #endregion


        private int chatIndex;



        #region 对外公布的一些数据

        /// <summary>
        /// 刷新玩家的状态数据
        /// </summary>
        public void RefreshStatus()
        {
            //当前英雄数量 = controller.Model.HeroTeam.Heros.Count;
            //当前出战英雄数量 = controller.Model.HeroTeam.CurrentOutHeroCount;
            //当前关卡 = controller.Model.PlayerGameLevel.CurrentGameLevelId;
        }

        /// <summary>
        /// 各项操作不是返回success的数据
        /// </summary>
        public int 错误计数 { get; set; }

        #endregion
    }

}

