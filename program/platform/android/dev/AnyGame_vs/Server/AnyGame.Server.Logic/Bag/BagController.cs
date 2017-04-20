using AnyGame.Server.Entity.Character;
using AnyGame.Server.Interface.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Logic.Bag
{
    /// <summary>
    /// 背包控制器
    /// </summary>
    public class BagController
    {

        private const int MoneyMax = int.MaxValue;

        #region 给玩家加钱/扣钱
        /// <summary>
        /// 玩家金币变更
        /// 正数为加钱，负数为扣钱
        /// </summary>
        /// <param name="player">玩家</param>
        /// <param name="money">
        /// 金币 正数为加钱，负数为扣钱
        /// </param>
        /// <param name="changeType">变更类型</param>
        public void MoneyChange(Player player, int money, ResouceChangeType changeType)
        {
            if (money == 0)
                return;

            player.Res.Gold += money;

            if (player.Res.Gold > MoneyMax || player.Res.Gold < 0)
                player.Res.Gold = MoneyMax;

            if (player.NetState != null && player.NetState.Running)
                ClientProxy.Bag.SyncResouce(player.NetState, ResourceType.Gold, player.Res.Gold);
            //DataWriter.Bag.MoneyChange(player, changeType, money, player.Res.Money);
        }
        #endregion
    }
}
