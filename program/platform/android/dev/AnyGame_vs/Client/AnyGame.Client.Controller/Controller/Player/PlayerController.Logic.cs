using AnyGame.Client.Entity;
using AnyGame.Client.Entity.Character;
using AnyGame.Client.Entity.Guide;
using DogSE.Client.Core;
using System;

namespace AnyGame.Client.Controller.Player
{

    /// <summary>
    /// Player
    /// </summary>
    public partial class PlayerController : BasePlayerController
    {
        private readonly GameController controller;

        public PlayerController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        private EntityModel Model
        {
            get { return controller.Model; }
        }

        internal override void OnUnlockGuideRecordResult(GuideTypes type, bool isPass)
        {
            UnlockGuideRecordResultEvent?.Invoke(this, new UnlockGuideRecordResultEventArgs
            {
                Type = type,
                IsPass = isPass,
            });
        }

        internal override void OnSyncGuideRecords(GuideTypes[] records)
        {
            SyncGuideRecordsEvent?.Invoke(this, new SyncGuideRecordsEventArgs
            {
                Records = records,
            });
        }

        internal override void OnUnlockMenuResult(MenuTypes menu, bool isUnlock)
        {
            UnlockMenuResultEvent?.Invoke(this, new UnlockMenuResultEventArgs
            {
                Menu = menu,
                IsUnlock = isUnlock,
            });
        }

        internal override void OnSyncUnlockMenus(MenuTypes[] menus)
        {
            SyncUnlockMenusEvent?.Invoke(this, new SyncUnlockMenusEventArgs
            {
                Menus = menus,
            });
        }

        internal override void OnPlayerRenameResult(RenameResultType result, string newName)
        {
            PlayerRenameResultEvent?.Invoke(this, new PlayerRenameResultEventArgs
            {
                Result = result,
                NewName = newName,
            });
        }

        internal override void OnSyncPlayerInfo(string name, int sex, DateTime createTime,
            DateTime lastLoginTime, DateTime lastLogoffTime, int level, int exp, long expSum, int vipLevel)
        {
            Model.Player.Name = name;
            Model.Player.Sex = (Sex)sex;
            Model.Player.CreateTime = createTime;
            Model.Player.LastLoginTime = lastLoginTime;
            Model.Player.LastLogoffTime = lastLogoffTime;
            Model.Player.Level = level;
            Model.Player.Exp = exp;
            Model.Player.ExpSum = expSum;
            Model.Player.VipLevel = vipLevel;
        }

        internal override void OnSyncPlayerPropertyInfo(Property property)
        {
            Model.Player.Property = property;
        }

        /// <summary>
        /// 解锁某个新手引导
        /// </summary>
        public event EventHandler<UnlockGuideRecordResultEventArgs> UnlockGuideRecordResultEvent;

        /// <summary>
        /// 同步新手引导记录
        /// </summary>
        public event EventHandler<SyncGuideRecordsEventArgs> SyncGuideRecordsEvent;

        /// <summary>
        /// 解锁菜单
        /// </summary>
        public event EventHandler<UnlockMenuResultEventArgs> UnlockMenuResultEvent;

        /// <summary>
        /// 同步已解锁菜单列表
        /// </summary>
        public event EventHandler<SyncUnlockMenusEventArgs> SyncUnlockMenusEvent;

        /// <summary>
        /// 玩家改名结果
        /// </summary>
        public event EventHandler<PlayerRenameResultEventArgs> PlayerRenameResultEvent;


    }

    /// <summary>
    /// 解锁某个新手引导 【参数】
    /// </summary>
    public class UnlockGuideRecordResultEventArgs : EventArgs
    {
        /// <summary>
        /// 新手引导类型
        /// </summary>
        public GuideTypes Type { get; internal set; }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; internal set; }
    }

    /// <summary>
    /// 同步新手引导记录 【参数】
    /// </summary>
    public class SyncGuideRecordsEventArgs : EventArgs
    {
        /// <summary>
        /// 记录数据
        /// </summary>
        public GuideTypes[] Records { get; internal set; }
    }

    /// <summary>
    /// 解锁菜单 【参数】
    /// </summary>
    public class UnlockMenuResultEventArgs : EventArgs
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public MenuTypes Menu { get; internal set; }

        /// <summary>
        /// 是否解锁
        /// </summary>
        public bool IsUnlock { get; internal set; }
    }

    /// <summary>
    /// 同步已解锁菜单列表 【参数】
    /// </summary>
    public class SyncUnlockMenusEventArgs : EventArgs
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        public MenuTypes[] Menus { get; internal set; }
    }

    /// <summary>
    /// 玩家改名结果 【参数】
    /// </summary>
    public class PlayerRenameResultEventArgs : EventArgs
    {
        /// <summary>
        /// 结果
        /// </summary>
        public RenameResultType Result { get; internal set; }

        /// <summary>
        /// 新名字
        /// </summary>
        public string NewName { get; internal set; }
    }



}
