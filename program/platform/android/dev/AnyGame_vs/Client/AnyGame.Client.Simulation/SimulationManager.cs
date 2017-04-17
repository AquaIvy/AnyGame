using AnyGame.Client.Simulation.Simulation;
using DogSE.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnyGame.Client.Simulation
{
    /// <summary>
    /// 模拟器管理类
    /// </summary>
    public class SimulationManager
    {
        public SimulationManager()
        {
            NamePre = "Test";
            StartId = 1;
            EndId = 100000;
        }
        readonly List<SimulationClient> clients = new List<SimulationClient>();

        /// <summary>
        /// 客户端列表
        /// </summary>
        [Browsable(false)]
        public List<SimulationClient> Clients
        {
            get { return clients; }
        }

        [Browsable(false)]
        public string NamePre { get; set; }

        [Browsable(false)]
        public int StartId { get; set; }

        [Browsable(false)]
        public int EndId { get; set; }

        /// <summary>
        /// 登陆的基地址
        /// </summary>
        public string LoginBaseUrl { get; set; }

        public int AiTime;

        private Random rand = new Random();

        private string Host;
        private int Port;

        void RunTask()
        {
            while (isRuning)
            {
                var now = DateTime.Now;
                SimulationClient[] runs;
                lock (clients)
                {
                    runs = clients.ToArray();
                }

                NetController.TaskManager.RunAllTask();
                foreach (var client in runs)
                {
                    NetController.TaskManager.RunAllTask();
                    client.Now = now;
                    client.DoSomething();
                }
                var endTime = DateTime.Now;

                AiTime += (int)(endTime - now).TotalMilliseconds;
                Thread.Sleep(1);
            }
            runTaskThread = null;
        }

        /// <summary>
        /// 用一个线程来处理登陆
        /// </summary>
        void RunLogin()
        {
            var startTime = DateTime.Now;

            canRefreshStatus = false;

            var waitLogin = new List<SimulationClient>();

            for (int i = 0; i < 等待登陆人数; i++)
            {
                var client = new SimulationClient();
                client.Host = Host;
                client.Port = Port;
                client.AccountName = NamePre + rand.Next(StartId, EndId);
                client.LoginBaseUrl = LoginBaseUrl;
                client.GetToken();
                waitLogin.Add(client);
            }

            while (isRuning && 等待登陆人数 > 0)
            {
                SimulationClient client = null;
                if (waitLogin.Count > 0)
                {
                    client = waitLogin[0];
                    waitLogin.RemoveAt(0);
                }

                if (client == null)
                {
                    client = new SimulationClient();
                    client.Host = Host;
                    client.Port = Port;
                    client.AccountName = NamePre + rand.Next(StartId, EndId);
                    client.LoginBaseUrl = LoginBaseUrl;
                }

                client.StartLogin();

                已经登陆人数 += client.IsLoginSuccess ? 1 : 0;
                登陆失败的人数 += client.IsLoginSuccess ? 0 : 1;

                if (client.IsLoginSuccess)
                {
                    lock (clients)
                    {
                        clients.Add(client);
                    }
                    等待登陆人数--;
                }
            }

            runLoginThread = null;
            canRefreshStatus = true;
            Logs.Warn("login finish.");

            int sec = (int)(DateTime.Now - startTime).TotalSeconds;
            登陆用时 = string.Format("{0}sec {1}ms", sec, (DateTime.Now - startTime).TotalMilliseconds / 已经登陆人数);
        }

        /// <summary>
        /// 用一个线程来处理登陆
        /// </summary>
        void TaskRunLogin()
        {
            var startTime = DateTime.Now;
            canRefreshStatus = false;


            List<SimulationClient> runClient = new List<SimulationClient>();

            for (int i = 0; i < 等待登陆人数; i++)
            {
                var client = new SimulationClient();
                client.Host = Host;
                client.Port = Port;
                client.AccountName = NamePre + rand.Next(StartId, EndId);

                if (runClient.FirstOrDefault(o => o.AccountName == client.AccountName) != null)
                {
                    i--;
                    continue;
                }

                client.LoginBaseUrl = LoginBaseUrl;
                client.GetToken();
                runClient.Add(client);
            }

            Console.WriteLine("获得takon时间 {0}sec", (DateTime.Now - startTime).TotalSeconds);

            Parallel.ForEach(runClient, new ParallelOptions { MaxDegreeOfParallelism = 5 }, client =>
            {
                client.StartLogin();

                已经登陆人数 += client.IsLoginSuccess ? 1 : 0;
                登陆失败的人数 += client.IsLoginSuccess ? 0 : 1;

                if (client.IsLoginSuccess)
                {
                    lock (clients)
                    {
                        clients.Add(client);
                    }
                    等待登陆人数--;
                }
            });

            runLoginThread = null;
            canRefreshStatus = true;
            Logs.Warn("login finish.");

            int sec = (int)(DateTime.Now - startTime).TotalSeconds;
            登陆用时 = string.Format("{0}sec {1}ms", sec, (DateTime.Now - startTime).TotalMilliseconds / 已经登陆人数);
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="num"></param>
        public void StartRun(string host, int port, int num)
        {
            isRuning = true;
            测试总人数 += num;
            等待登陆人数 = num;

            Host = host;
            Port = port;

            if (runTaskThread == null)
            {
                runTaskThread = new Thread(RunTask);
                runTaskThread.Start();
            }
            if (runLoginThread == null)
            {
                runLoginThread = new Thread(TaskRunLogin);
                runLoginThread.Start();
            }
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        /// <param name="num"></param>
        public void AppendRun(int num)
        {
            if (num == 0)
                return;

            等待登陆人数 += num;

            if (runLoginThread == null)
            {
                runLoginThread = new Thread(RunLogin);
                runLoginThread.Start();
            }
        }

        private Thread runTaskThread;
        private Thread runLoginThread;

        private bool isRuning;

        /// <summary>
        /// 停止测试
        /// </summary>
        public void Stop()
        {
            try
            {
                isRuning = false;
                clients.ForEach(o => o.Stop());
                clients.Clear();

                if (runTaskThread != null)
                    runTaskThread.Abort();
                if (runLoginThread != null)
                    runLoginThread.Abort();
            }
            catch (Exception ex)
            {
                Logs.Error(ex.ToString());
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendFormat("测试总人数={0}", 测试总人数);
            sb.AppendLine();

            sb.AppendFormat("已经登陆人数={0}", 已经登陆人数);
            sb.AppendLine();
            sb.AppendFormat("登陆失败的人数={0}", 登陆失败的人数);
            sb.AppendLine();
            sb.AppendFormat("等待登陆人数={0}", 等待登陆人数);
            sb.AppendLine();
            sb.AppendFormat("逻辑错误客户端={0}", 逻辑错误客户端);
            sb.AppendLine();
            sb.AppendFormat("网络断开客户端={0}", 网络断开客户端);
            sb.AppendLine();
            sb.AppendFormat("AiTime={0}ms", AiTime);
            sb.AppendLine();
            sb.AppendLine();
            return sb.ToString();
        }

        public int 测试总人数 { get; set; }

        public int 已经登陆人数 { get; set; }

        public int 登陆失败的人数 { get; set; }

        public int 等待登陆人数 { get; set; }

        public int 逻辑错误客户端 { get; set; }

        public int 网络断开客户端 { get; set; }

        public string 登陆用时 { get; set; }

        private DateTime lastUpdateTime = DateTime.Now;
        public string AI处理时间
        {
            get
            {
                return AiTime + "ms";
            }
        }


        public int 当前在线
        {
            get { return clients.Count; }
            set { }
        }

        private bool canRefreshStatus = true;

        /// <summary>
        /// 刷新状态，如果有人离线，或者发生错误，就把人踢下线
        /// </summary>
        public void RefreshStatus()
        {
            if (!canRefreshStatus)
                return;

            foreach (var c in clients.ToArray())
            {
                if (c.IsError || c.错误计数 > 20)
                {
                    c.Stop();
                    lock (clients)
                    {
                        clients.Remove(c);
                    }

                    if (c.IsError)
                        网络断开客户端++;
                    else
                        逻辑错误客户端++;
                }
            }
            if (clients.Count + 等待登陆人数 <= 测试总人数)
            {
                AppendRun(测试总人数 - clients.Count - 等待登陆人数);
            }
        }

    }

}
