using AnyGame.Server.Database;
using AnyGame.Server.Entity;
using AnyGame.Server.Entity.Character;
using AnyGame.Server.Entity.GameEvent;
using AnyGame.Server.Interface.Client;
using DogSE.Library.Log;
using DogSE.Server.Core.Net;
using System;
using System.Linq;
using IPlayer = AnyGame.Server.Interface.Server.IPlayer;
using AnyGame.Server.Entity.Guide;

namespace AnyGame.Server.Logic.Bags
{
    /// <summary>
    /// 
    /// </summary>
    class PlayerModule : IPlayer
    {
        #region ILogicModule 成员

        /// <summary>
        /// 模块id
        /// </summary>
        public string ModuleId
        {
            get { return "PlayerModule"; }
        }

        public void Initializationing()
        {
            PlayerEvents.EnterGame += PlayerEvents_EnterGame;
        }

        public void Initializationed()
        {

        }

        public void ReLoadTemplate()
        {

        }

        public void Release()
        {
            PlayerEvents.EnterGame -= PlayerEvents_EnterGame;
        }

        #endregion

        private void PlayerEvents_EnterGame(Player player)
        {
            //同步玩家数据
            ClientProxy.Player.SyncPlayerInfo(player.NetState, player.Name, (int)player.Sex,
                player.CreateTime, player.LastLoginTime, player.LastLogoffTime, player.Level,
                player.Exp, player.ExpSum, player.VipLevel);

            //同步玩家属性
            ClientProxy.Player.SyncPlayerPropertyInfo(player.NetState, player.Property);
        }

        public void UnlockGuideRecord(NetState netstate, GuideTypes type)
        {

        }

        public void UnlockMenu(NetState netstate, MenuTypes menu)
        {

        }

        public void PlayerRename(NetState netstate, string newName)
        {

        }
    }
}
