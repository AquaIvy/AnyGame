using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyGame.Client.Entity.Bags;
using AnyGame.Client.Entity.Common;

namespace AnyGame.Client.Controller.Bag
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
            UseItemRet?.Invoke(this, new UseItemResultEventArgs
            {
                Result = result,
                ItemId = itemId,
                LessCount = lessCount
            });
        }

        internal override void OnSyncItems(SyncType type, GameItem[] items)
        {
            throw new NotImplementedException();
        }

        internal override void OnSyncBag(int maxGridCount, GameItem[] items)
        {
            throw new NotImplementedException();
        }

        internal override void OnSyncAllResouce(int money, int gem)
        {
            throw new NotImplementedException();
        }

        internal override void OnSyncResouce(int resId, int num)
        {
            throw new NotImplementedException();
        }

        internal override void OnUpgradeBagResult(UpgradeBagResult result)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<UseItemResultEventArgs> UseItemRet;
    }


    public class UseItemResultEventArgs : EventArgs
    {
        public UseItemResult Result { get; internal set; }

        public int ItemId { get; internal set; }

        public int LessCount { get; internal set; }
    }
}
