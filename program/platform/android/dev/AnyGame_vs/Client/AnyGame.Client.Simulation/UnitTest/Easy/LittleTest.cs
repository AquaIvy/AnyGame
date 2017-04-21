using AnyGame.Client.Controller.Bag;
using DogSE.Library.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyGame.Client.Simulation.UnitTest.Easy
{
    [TestClass]
    public class LittleTest : BaseTest
    {
        [TestMethod]
        public async Task Test()
        {
            bool isLogin = await LoginServerUseRandomName();
            Assert.IsTrue(isLogin, "登陆失败");

            controller.System.RunGMCommand("addgold 178");
            controller.Bag.SyncResouceEvent += Bag_SyncResouceEvent;

            var ret = await WaitIsTrue(() => false, 100);
            Assert.IsTrue(ret, "没有触发事件");
        }

        private void Bag_SyncResouceEvent(object sender, SyncResouceEventArgs e)
        {
            Logs.Debug("{0}  {1}", e.ResId, e.Num);
            Logs.Debug("{0}  {1}", controller.Model.Res.Gold);
        }
    }

}
