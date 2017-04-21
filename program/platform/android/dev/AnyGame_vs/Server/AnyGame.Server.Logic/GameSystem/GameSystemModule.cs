using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyGame.Server.Entity.Character;
using AnyGame.Server.Interface.Client;
using DogSE.Server.Core.Net;
using IGameSystem = AnyGame.Server.Interface.Server.IGameSystem;
using AnyGame.Server.Entity.Login;
using DogSE.Library.Log;
using DogSE.Server.Core.Common;

namespace AnyGame.Server.Logic.GameSystem
{
    class GameSystemModule : IGameSystem
    {
        #region ILogicModule 成员

        /// <summary>
        /// 模块id
        /// </summary>
        public string ModuleId
        {
            get { return "GameSystemModule"; }
        }

        public void Initializationing()
        {
            GMCommand.AddNetCommand("addgold", AddGold);
            GMCommand.AddNetCommand("addgem", AddGem);
            //GMCommand.AddNetCommand("additem", AddItem);
        }

        public void Initializationed()
        {

        }

        public void ReLoadTemplate()
        {

        }

        public void Release()
        {

        }

        #endregion

        #region GM 指令的具体操作

        bool AddGold(NetState netstate, string[] param)
        {
            var player = (Player)netstate.Player;

            if (param == null || param.Length == 0)
            {
                Logs.Error("addmoney gm command format is \"addmoney 1000\"");
                return false;
            }

            int gold = 0;
            if (!int.TryParse(param[0], out gold))
            {
                Logs.Error("addmoney gm command param {0} is not number", param[0]);
                return false;
            }

            GameController.Bag.GoldChange(player, gold, ResouceChangeType.GM_客户端发送命令行添加资源);

            return true;
        }

        bool AddGem(NetState netstate, string[] param)
        {
            var player = (Player)netstate.Player;

            if (param == null || param.Length == 0)
            {
                Logs.Error("addgem gm command format is \"addgem 1000\"");
                return false;
            }

            int gem = 0;
            if (!int.TryParse(param[0], out gem))
            {
                Logs.Error("addgem gm command param {0} is not number", param[0]);
                return false;
            }

            GameController.Bag.GemChange(player, gem, ResouceChangeType.GM_客户端发送命令行添加资源);

            return true;
        }

        #endregion

        public void RunGMCommand(NetState netstate, string command)
        {
            var player = netstate.Player as Player;
            if (player == null || !player.IsSuperMan)
            {
                //  非GM用户不能执行GM指令，发现后直接T人
                ClientProxy.Login.KickOfServer(netstate, OfflineType.GMKick);
                return;
            }

            var s1 = command.Split(' ');
            if (s1.Length > 0)
            {
                var name = s1[0].Trim();
                var fun = GMCommand.GetCommandFun(name);
                if (fun != null)
                {
                    var ret = fun(netstate,
                        s1.Length > 1 ? s1[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) : new string[0]);
                    if (!ret)
                    {
                        Logs.Error("gm {0} command run fail.", name);
                    }
                }
            }
        }

        public void GetSystemTime(NetState netstate)
        {

        }

        public void ClientLog(NetState netstate, string type, string context)
        {

        }

        public void PhoneInfo(NetState netstate, string context)
        {

        }

        public void ClientException(NetState netstate, string context)
        {

        }

        public void ClinetPauseStatus(NetState netsate, bool isPause, DateTime time)
        {

        }

        public void Heart(NetState netstate)
        {

        }
    }
}
