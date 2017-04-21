using AnyGame.Server.Template;
using DogSE.Library.Log;
using DogSE.Server.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyGame.Server.Database;
using AnyGame.Server.Entity;
using AnyGame.Server.Entity.Bags;
using AnyGame.Server.Entity.Character;
using AnyGame.Server.Entity.GameEvent;
using AnyGame.Server.Interface.Client;
using IBag = AnyGame.Server.Interface.Server.IBag;

namespace AnyGame.Server.Logic.Bags
{
    /// <summary>
    /// 
    /// </summary>
    class BagModule : IBag
    {
        #region ILogicModule 成员

        /// <summary>
        /// 模块id
        /// </summary>
        public string ModuleId
        {
            get { return "BagModule"; }
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

        }

        #endregion

        private void PlayerEvents_EnterGame(Player player)
        {
            try
            {
                var res = player.Res;
                ClientProxy.Bag.SyncAllResouce(player.NetState, res.Gold, res.Gem);

                var bag = player.Bag;
                ClientProxy.Bag.SyncBag(player.NetState, bag.MaxGridCount, bag.Items.ToArray());

                //同步客户端已使用过的物品列表
                //SyncPlayerUsedItem(player);
            }
            catch (Exception ex)
            {
                Logs.Error("Bag Sync data fail.", ex);
            }
        }

        public void OnUseItem(NetState netstate, int itemId, int useCount)
        {
            //  先瞅瞅内存里玩家数据有没有被缓存着
            var bag = WorldEntityManager.BagCache.GetEntity(netstate.BizId);
            GameItem item = null;

            if (bag == null)
            {
                bag = DB.GameDB.LoadEntity<Bag>(netstate.BizId);
                if (bag == null)
                {
                    ClientProxy.Bag.UseItemResult(netstate, UseItemResult.NotFind, itemId, 0);
                    return;
                }
            }

            item = bag.Items.FirstOrDefault(o => o.Id == itemId);
            if (item == null)
            {
                ClientProxy.Bag.UseItemResult(netstate, UseItemResult.NotFind, itemId, 0);
                return;
            }

            if (item.Num - useCount < 0)
            {
                ClientProxy.Bag.UseItemResult(netstate, UseItemResult.NotEnough, itemId, 0);
                return;
            }

            item.Num -= useCount;
            DB.GameDB.UpdateEntity<Bag>(bag);

            ClientProxy.Bag.UseItemResult(netstate, UseItemResult.Success, itemId, item.Num);

        }

        public void OnUpgradeBag(NetState netstate)
        {
            var player = (Player)netstate.Player;
            var bag = player.Bag;
            var res = player.Res;

            if (bag.CurBagLevel > 6)
            {
                ClientProxy.Bag.UpgradeBagResult(netstate, UpgradeBagResult.HasOverTop);
                return;
            }

            if (res.Gold < 100)
            {
                ClientProxy.Bag.UpgradeBagResult(netstate, UpgradeBagResult.LessRes);
                return;
            }

            res.Gold -= 100;
            DB.GameDB.UpdateEntity<Res>(res);

            ClientProxy.Bag.UpgradeBagResult(netstate, UpgradeBagResult.Success);
        }
    }
}
