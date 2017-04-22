using AnyGame.Client.Entity;
using AnyGame.Client.Entity.Character;
using AnyGame.Client.Entity.Character;
using DogSE.Client.Core;
using System;
using System.Collections.Generic;
using AnyGame.Client.Template.Item;
using AnyGame.Client.Template;
using System.Linq;
using DogSE.Library.Log;

namespace AnyGame.Client.Controller.Bag
{

    /// <summary>
    /// Bag
    /// </summary>
    public partial class BagController : BaseBagController
    {
        private readonly GameController controller;

        public BagController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        private EntityModel Model
        {
            get { return controller.Model; }
        }

        internal override void OnUseItemResult(UseItemResult result, int itemId, int lessCount)
        {
            UseItemResultEvent?.Invoke(this, new UseItemResultEventArgs
            {
                Result = result,
                ItemId = itemId,
                LessCount = lessCount,
            });
        }

        internal override void OnSyncItems(SyncType type, GameItem[] items)
        {
            SyncItemsEvent?.Invoke(this, new SyncItemsEventArgs
            {
                Type = type,
                Items = items,
            });
        }

        /// <summary>
        ///     更新物品模板数据
        ///     注意，如果对应模板不存在，这里为了客户端不出异常，会在的数组里过滤掉错误的物品
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private GameItem[] UpdateItemTemplate(GameItem[] items)
        {
            var ret = new List<GameItem>();

            foreach (GameItem item in items)
            {
                ItemTemplate template = Templates.ItemTemplate.FirstOrDefault(o => o.Id == item.TemplateId);
                if (template == null)
                {
                    Logs.Error("not find item templateId={0}", item.TemplateId);
                }
                else
                {
                    item.Template = template;
                    ret.Add(item);
                }
            }
            return ret.ToArray();
        }

        internal override void OnSyncBag(int maxGridCount, GameItem[] items)
        {
            Model.Bag.CurBagLevel = 1;
            Model.Bag.MaxGridCount = maxGridCount;
            Model.Bag.Items.Clear();
            Model.Bag.Items.AddRange(UpdateItemTemplate(items));

            SyncBagEvent?.Invoke(this, new SyncBagEventArgs
            {
                MaxGridCount = maxGridCount,
                Items = items,
            });
        }

        internal override void OnSyncAllResouce(int money, int gem)
        {
            SyncAllResouceEvent?.Invoke(this, new SyncAllResouceEventArgs
            {
                Money = money,
                Gem = gem,
            });
        }

        internal override void OnSyncResouce(int resId, int num)
        {
            if (resId == ResourceType.Gold)
            {
                Model.Res.Gold = num;
            }
            else if (resId == ResourceType.Gem)
            {
                Model.Res.Gem = num;
            }


            SyncResouceEvent?.Invoke(this, new SyncResouceEventArgs
            {
                ResId = resId,
                Num = num,
            });
        }

        internal override void OnUpgradeBagResult(UpgradeBagResult result)
        {
            UpgradeBagResultEvent?.Invoke(this, new UpgradeBagResultEventArgs
            {
                Result = result,
            });
        }

        /// <summary>
        /// 使用背包物品结果
        /// </summary>
        public event EventHandler<UseItemResultEventArgs> UseItemResultEvent;

        /// <summary>
        /// 同步物品
        /// </summary>
        public event EventHandler<SyncItemsEventArgs> SyncItemsEvent;

        /// <summary>
        /// 同步背包信息
        /// </summary>
        public event EventHandler<SyncBagEventArgs> SyncBagEvent;

        /// <summary>
        /// 同步所有的资源
        /// </summary>
        public event EventHandler<SyncAllResouceEventArgs> SyncAllResouceEvent;

        /// <summary>
        /// 同步资源
        /// </summary>
        public event EventHandler<SyncResouceEventArgs> SyncResouceEvent;

        /// <summary>
        /// 升级背包的结果
        /// </summary>
        public event EventHandler<UpgradeBagResultEventArgs> UpgradeBagResultEvent;


    }

    /// <summary>
    /// 使用背包物品结果 【参数】
    /// </summary>
    public class UseItemResultEventArgs : EventArgs
    {
        /// <summary>
        /// 结果枚举
        /// </summary>
        public UseItemResult Result { get; internal set; }

        /// <summary>
        /// 物品id
        /// </summary>
        public int ItemId { get; internal set; }

        /// <summary>
        /// 物品剩余数量
        /// </summary>
        public int LessCount { get; internal set; }
    }

    /// <summary>
    /// 同步物品 【参数】
    /// </summary>
    public class SyncItemsEventArgs : EventArgs
    {
        /// <summary>
        /// 同步类型枚举
        /// </summary>
        public SyncType Type { get; internal set; }

        /// <summary>
        /// 物品列表
        /// </summary>
        public GameItem[] Items { get; internal set; }
    }

    /// <summary>
    /// 同步背包信息 【参数】
    /// </summary>
    public class SyncBagEventArgs : EventArgs
    {
        /// <summary>
        /// 最大格子数量
        /// </summary>
        public int MaxGridCount { get; internal set; }

        /// <summary>
        /// 物品列表
        /// </summary>
        public GameItem[] Items { get; internal set; }
    }

    /// <summary>
    /// 同步所有的资源 【参数】
    /// </summary>
    public class SyncAllResouceEventArgs : EventArgs
    {
        /// <summary>
        /// 金币
        /// </summary>
        public int Money { get; internal set; }

        /// <summary>
        /// 钻石
        /// </summary>
        public int Gem { get; internal set; }
    }

    /// <summary>
    /// 同步资源 【参数】
    /// </summary>
    public class SyncResouceEventArgs : EventArgs
    {
        /// <summary>
        /// 资源id
        /// </summary>
        public int ResId { get; internal set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; internal set; }
    }

    /// <summary>
    /// 升级背包的结果 【参数】
    /// </summary>
    public class UpgradeBagResultEventArgs : EventArgs
    {
        /// <summary>
        /// 结果枚举
        /// </summary>
        public UpgradeBagResult Result { get; internal set; }
    }



}
