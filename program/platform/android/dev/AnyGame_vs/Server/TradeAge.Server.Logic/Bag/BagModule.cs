using AnyGame.Server.Template;
using DogSE.Library.Log;
using DogSE.Server.Core.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeAge.Server.Database;
using TradeAge.Server.Entity;
using TradeAge.Server.Entity.Bags;
using TradeAge.Server.Entity.Character;
using TradeAge.Server.Entity.GameEvent;
using TradeAge.Server.Interface.Client;
using IBag = TradeAge.Server.Interface.Server.IBag;

namespace TradeAge.Server.Logic.Bags
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
        }

        public void OnUseItem(NetState netstate, int itemId, int useCount)
        {
            Logs.Info("OnUseItem {0} {1}", itemId, useCount);

            //  先瞅瞅内存里玩家数据有没有被缓存着
            var bag = WorldEntityManager.BagCache.GetEntity(netstate.BizId);

            if (bag == null)
            {
                bag = DB.GameDB.LoadEntity<Bag>(netstate.BizId);
                if (bag == null)
                {
                    bag = new Bag
                    {
                        Id = netstate.BizId,
                        MaxCount = 100,
                        CurCount = 100
                    };
                    DB.GameDB.InsertEntity<Bag>(bag);
                }
                WorldEntityManager.BagCache.AddOrReplace(bag);

            }

            Logs.Info("使用之前：" + WorldEntityManager.BagCache.GetEntity(netstate.BizId).CurCount);
            if (bag.CurCount - useCount < 0)
            {
                ClientProxy.Bag.UseItemResult(netstate, UseItemResult.Fail, itemId, bag.CurCount);
                return;
            }

            bag.CurCount -= useCount;
            DB.GameDB.UpdateEntity<Bag>(bag);
            Logs.Info("使用之后：" + WorldEntityManager.BagCache.GetEntity(netstate.BizId).CurCount);

            ClientProxy.Bag.UseItemResult(netstate, UseItemResult.Success, itemId, bag.CurCount);

        }

    }
}
