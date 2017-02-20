using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeAge.Client.Entity.Bags;

namespace TradeAge.Client.Controller.Bag
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BagController : BaseBagController
    {
        private readonly GameController controller;

        public BagController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        internal override void OnUseItemResult(UseItemResult result, int itemId, int lessCount)
        {
            Logs.Info("UseItemResult  " + result);

            if (result == UseItemResult.Success)
            {
                controller.Game.SyncTime();
            }

            if (UseItemRet != null)
            {
                UseItemRet(this, new UseItemResultEventArgs
                {
                    itemId = itemId,
                    curCount = lessCount
                });
            }
        }

        internal override void OnSyncBag(int MaxCount, int CurCount)
        {
            controller.Model.Bag.CurCount = CurCount;
            controller.Model.Bag.MaxCount = MaxCount;
        }

        public event EventHandler<UseItemResultEventArgs> UseItemRet;
    }


    public class UseItemResultEventArgs : EventArgs
    {
        public UseItemResult Result { get; internal set; }
        public int itemId { get; internal set; }

        public int curCount { get; internal set; }
    }
}
