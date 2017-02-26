using AnyGame.View.Components;
using AnyGame.View.Forms.Login;
using AnyGame.View.Forms.Shop;
using DogSE.Library.Log;
using TradeAge.Client.Controller.Login;
using TradeAge.Client.Entity.Login;

namespace AnyGame
{
    public class Workflow
    {
        public Workflow()
        {

        }


        public void Init()
        {
            GameCenter.Controller.Login.LoginServerRet += OnLoginServerRet;
            GameCenter.Controller.Login.SyncDataFinish += OnSyncDataFinish;
        }

        private void OnLoginServerRet(object sender, LoginServerResultEventArgs e)
        {
            if (e.Result == LoginServerResult.Success)
            {
                GameCenter.Controller.Login.LoginServerRet -= OnLoginServerRet;

                if (!e.IsCreatedPlayer)
                {
                    var createCharacter = new FrmCreateCharacter();
                    UIRoot.Show(createCharacter);
                }
            }
            else
            {
                Logs.Error("登陆失败 {0}", e.Result.ToString());
            }
        }

        private void OnSyncDataFinish()
        {
            Logs.Info("同步数据完成，可以进入主界面了   {0}", GameCenter.Entity.Player.Name);

            //var shop = new FrmShop();
            //UIRoot.Show(shop);
        }

        public static void Update()
        {

        }
    }
}
