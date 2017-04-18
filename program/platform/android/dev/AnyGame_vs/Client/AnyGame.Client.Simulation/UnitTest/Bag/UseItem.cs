using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyGame.Client.Entity.Bags;

namespace AnyGame.Client.Simulation.UnitTest.Bag
{
    [TestClass]
    public class UseItem : BaseTest
    {
        [TestMethod]
        public async Task TestUseItem()
        {
            bool isLogin = await LoginServerUseRandomName();
            Assert.IsTrue(isLogin, "登陆失败");

            bool isSuccessUseItem = false;
            

            controller.Bag.UseItemRet += (s, e) =>
            {
                if (e.Result == UseItemResult.Success)
                {
                    Assert.AreEqual(e.Result, UseItemResult.Success);
                    isSuccessUseItem = true;
                }
            };

            controller.Bag.UseItem(101, 1);

            var ret = await WaitIsTrue(() => isSuccessUseItem, 10);
            Assert.IsTrue(ret, "没有触发事件");
        }

    }
}
