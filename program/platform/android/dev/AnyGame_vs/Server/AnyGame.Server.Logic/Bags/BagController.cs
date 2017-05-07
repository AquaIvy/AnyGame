using AnyGame.Server.Database;
using AnyGame.Server.Entity.Character;
using AnyGame.Server.Interface.Client;
using AnyGame.Server.Template;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Logic.Bags
{
    /// <summary>
    /// 背包控制器
    /// </summary>
    public class BagController
    {

        //金币上限  20亿
        private const int GoldMax = 2000000000;

        #region 给玩家加钱/扣钱
        /// <summary>
        /// 玩家金币变更
        /// 正数为加钱，负数为扣钱
        /// </summary>
        /// <param name="player">玩家</param>
        /// <param name="gold">金币</param>
        /// <param name="changeType">变更类型</param>
        public void GoldChange(Player player, int gold, DataChangeType changeType)
        {
            if (gold == 0)
                return;

            player.Res.Gold += gold;

            if (player.Res.Gold > GoldMax)
                player.Res.Gold = GoldMax;

            if (player.Res.Gold < 0)
                player.Res.Gold = 0;

            if (player.NetState != null && player.NetState.Running)
                ClientProxy.Bag.SyncResouce(player.NetState, ResourceType.Gold, player.Res.Gold);
            DB.GameDB.UpdateEntity<Res>(player.Res);
            //DataWriter.Bag.MoneyChange(player, changeType, money, player.Res.Money);
        }
        #endregion

        #region 给玩家加钻石/扣钻石
        /// <summary>
        /// 玩家钻石变更
        /// 正数为加钻石，负数为扣钻石
        /// </summary>
        /// <param name="player">玩家</param>
        /// <param name="gem">gem</param>
        /// <param name="changeType">变更类型</param>
        public void GemChange(Player player, int gem, DataChangeType changeType)
        {
            if (gem == 0)
                return;

            player.Res.Gem += gem;

            if (player.Res.Gem > GoldMax)
                player.Res.Gem = GoldMax;

            if (player.Res.Gem < 0)
                player.Res.Gem = 0;

            if (player.NetState != null && player.NetState.Running)
                ClientProxy.Bag.SyncResouce(player.NetState, ResourceType.Gem, player.Res.Gem);
            DB.GameDB.UpdateEntity<Res>(player.Res);
            //DataWriter.Bag.MoneyChange(player, changeType, money, player.Res.Money);
        }
        #endregion

        #region 添加物品

        public bool AddItem(Player player, int itemTemplateId, int num, DataChangeType changeType)
        {
            if (num <= 0)
                return false;

            var template = Templates.GetItemTemplate(itemTemplateId);
            if (template == null)
            {
                Logs.Error("not find item {0}. playerId {1}. {2}", itemTemplateId.ToString(), player.Id.ToString(), changeType.ToString());
                return false;
            }

            var item = player.Bag.Items.FirstOrDefault(o => o.TemplateId == itemTemplateId);
            SyncType syncType = SyncType.Update;
            if (item != null)
            {
                item.Num += num;
            }
            else
            {
                item = new GameItem
                {
                    Id = player.Bag.MaxItemId++,
                    TemplateId = itemTemplateId,
                    Num = num
                };

                syncType = SyncType.Add;
                player.Bag.Items.Add(item);
            }

            if (player.NetState != null && player.NetState.Running)
                ClientProxy.Bag.SyncItems(player.NetState, syncType, item);
            DB.GameDB.UpdateEntity<Bag>(player.Bag);

            return true;
        }

        #endregion

        #region 消耗物品

        public bool ConsumeItem(Player player, int itemTemplateId, int num, DataChangeType changeType)
        {
            if (num <= 0)
                return false;

            var item = player.Bag.Items.FirstOrDefault(o => o.TemplateId == itemTemplateId);
            if (item == null)
            {
                Logs.Error("not find item {0}. playerId {1}. {2}", itemTemplateId.ToString(), player.Id.ToString(), changeType.ToString());
                return false;
            }

            SyncType syncType = SyncType.Update;
            if (item.Num > num)
            {
                item.Num -= num;
            }
            else
            {
                syncType = SyncType.Remove;
                item.Num = 0;
                player.Bag.Items.Remove(item);
            }

            if (player.NetState != null && player.NetState.Running)
                ClientProxy.Bag.SyncItems(player.NetState, syncType, item);
            DB.GameDB.UpdateEntity<Bag>(player.Bag);

            return true;
        }

        #endregion

        #region 删除物品

        public bool RemoveItem(Player player, int itemTemplateId, DataChangeType changeType)
        {
            var item = player.Bag.Items.FirstOrDefault(o => o.TemplateId == itemTemplateId);
            if (item == null)
            {
                Logs.Error("not find item {0}. playerId {1}. {2}", itemTemplateId.ToString(), player.Id.ToString(), changeType.ToString());
                return false;
            }

            return ConsumeItem(player, itemTemplateId, item.Num, changeType);
        }

        #endregion
    }
}
