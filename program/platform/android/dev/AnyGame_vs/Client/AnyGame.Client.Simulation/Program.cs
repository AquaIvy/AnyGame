using AnyGame.Client.Template;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnyGame.Client.Simulation
{
    class Program
    {
        private static Random rand = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {
            Logs.AddConsoleAppender();
            Logs.SetMessageLevel<ConsoleAppender>(LogMessageType.MSG_ERROR);

            Templates.LoadTemplate();

            RunManageTest();
        }

        static void RunManageTest()
        {
            var sm = new SimulationManager();

            sm.NamePre = Guid.NewGuid().ToString().Substring(0, 6);
            sm.StartId = 1;
            sm.EndId = 1000000;

            sm.LoginBaseUrl = "http://192.168.2.83:81/Login/Api/Fishluv.aspx?uid=";
            sm.StartRun("192.168.2.84", 4601, 1);

            var starTime = DateTime.Now;
            int i = 0;
            while (true)
            {
                Thread.Sleep(100);
                if (i++ % (10 * 30) == 0)   //  30s 刷新一次当前状态
                {
                    Console.WriteLine(sm.ToString());
                }

                if (rand.Next(50000) == 1)
                    break;
            }

            File.AppendAllText("openlog.txt", string.Format("执行时间 {0}", (DateTime.Now - starTime)));

            Console.WriteLine("我要退出了。");
            Process.GetCurrentProcess().Kill();
        }
    }
}
