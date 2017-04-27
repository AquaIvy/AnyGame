using DogSE.Library.Log;
using DogSE.Library.Serialize;
using LoginWeb.Controller;
using LoginWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using AnyGame.Server.Entity.Character;

namespace LoginWeb.Api
{
    public partial class Fishluv : System.Web.UI.Page
    {
        private static PlatformTypes PlatformType = PlatformTypes.Fishluv;

        protected void Page_Load(object sender, EventArgs e)
        {
            //@"http://127.0.0.1:200/LoginWeb/Api/Fishluv.aspx?accessToken=test001&phonePlatformTypes=android&cver=4&phoneid=10001"
            string token = Request.QueryString["accessToken"];
            string phoneType = Request.QueryString["phonePlatformTypes"];
            string phoneId = Request.QueryString["phoneid"];
            string clientVersion = Request.QueryString["cver"];

            //业务处理
            //登陆成功，返回登陆信息
            LoginResult ret = new LoginResult();

            if (string.IsNullOrEmpty(token))
            {
                ret.Error = "account is null";
            }
            else
            {
                try
                {
                    var account = AccountController.GetPlatformAccount(token, PlatformType);
                    if (account == null)
                        account = AccountController.CreatePlatformAccount(phoneId, token, PlatformType);

                    //  获得所有的游戏账户信息
                    var gameAccounts = DB.AccountDB.QueryEntitys<GameAccount>("PlatformAccountId ={0}", account.Id);

                    PhonePlatformTypes phonePlatformType = PhonePlatformTypes.Andriod;
                    if (phoneType.Equals("ios"))
                    {
                        phonePlatformType = PhonePlatformTypes.IOS;
                    }

                    ret.LoginToken = AccountController.GetLoginToken(account);
                    var notice = AccountController.GetNotice(account.PlatformId, phonePlatformType);
                    ret.Notice = notice.Content;
                    ret.Version = notice.Version;
                    ret.GameServers = new List<GameServer>();
                    ret.NickName = account.UId;

                    foreach (var zone in AccountController.GetLoginGameZone(phonePlatformType, PlatformType, clientVersion))
                    {
                        var server = zone.GameServers.FirstOrDefault();
                        var s = new GameServer
                        {
                            GameZoneId = zone.Id,
                            Host = server.ServerHost,
                            Port = server.ServerPort,
                            Name = zone.Name,
                            Status = (ServerStatus)zone.Status
                        };

                        //  如果玩家激活过账号，这里可以把玩家激活的角色信息发给玩家
                        var ga = gameAccounts.FirstOrDefault(o => o.ZoneId == zone.Id);
                        if (ga != null && !string.IsNullOrEmpty(ga.CharacterName))
                            s.CharacterName = ga.CharacterName;

                        ret.GameServers.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    ret.Error = ex.Message;
                    Logs.Error(ex.ToString());
                }
            }

            try
            {
                var retMsg = ret.XmlSerialize();
                Response.Write(retMsg);
            }
            catch (Exception ex)
            {
                Logs.Error(ex.ToString());
            }
            Response.End();
        }
    }
}