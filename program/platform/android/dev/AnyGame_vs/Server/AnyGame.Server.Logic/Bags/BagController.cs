using AnyGame.Server.Database;
using AnyGame.Server.Entity.Bags;
using AnyGame.Server.Interface.Client;
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
        public void GoldChange(Player player, int gold, ResouceChangeType changeType)
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
        public void GemChange(Player player, int gem, ResouceChangeType changeType)
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
    }
}
