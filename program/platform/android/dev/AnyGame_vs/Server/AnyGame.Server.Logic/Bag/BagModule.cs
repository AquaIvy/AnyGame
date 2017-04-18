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
            //玩家进入游戏 同步所有数据
            //var bag = WorldEntityManager.BagCache.GetEntity(player.Id);

            //if (bag == null)
            //{
            //    bag = DB.GameDB.LoadEntity<Bag>(player.Id);
            //    if (bag == null)
            //    {
            //        bag = new Bag
            //        {
            //            Id = player.Id,
            //            MaxCount = 100,
            //            CurCount = 100
            //        };
            //        DB.GameDB.InsertEntity<Bag>(bag);
            //    }
            //    WorldEntityManager.BagCache.AddOrReplace(bag);
            //}

            //var bag = player.Bag;

            //ClientProxy.Bag.SyncBag(player.NetState, bag.MaxCount, bag.CurCount);

            try
            {
                var res = player.Res;
                ClientProxy.Bag.SyncBag(player.NetState, res.Money, res.Ingot, res.Fragments, res.Statue, res.Strength, res.Detonator, res.MiningToolkit,
                    res.Ores.ToArray(), res.Foods.ToArray(), res.Badges.ToArray(), res.UnlockResIds.ToArray());

                var bag = player.Bag;
                ClientProxy.Bag.SyncBag(player.NetState, 0, bag.UnlockLevel, bag.Items.ToArray());

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

            if (bag == null)
            {
                bag = DB.GameDB.LoadEntity<Bag>(netstate.BizId);
                if (bag == null)
                {
                    bag = new Bag { Id = netstate.BizId };
                    DB.GameDB.InsertEntity<Bag>(bag);
                }
                WorldEntityManager.BagCache.AddOrReplace(bag);
            }

            if (bag.CurCount - useCount < 0)
            {
                ClientProxy.Bag.UseItemResult(netstate, UseItemResult.Fail, itemId, bag.CurCount);
                return;
            }

            bag.CurCount -= useCount;
            DB.GameDB.UpdateEntity<Bag>(bag);

            ClientProxy.Bag.UseItemResult(netstate, UseItemResult.Success, itemId, bag.CurCount);

        }

    }
}
