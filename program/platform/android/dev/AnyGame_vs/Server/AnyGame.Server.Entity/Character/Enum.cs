using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server 
namespace AnyGame.Server.Entity.Character
#else
namespace AnyGame.Client.Entity.Character
#endif
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male = 0,

        /// <summary>
        /// 女性
        /// </summary>
        Female = 1,
    }


    /// <summary>
    /// 菜单id.功能开启
    /// </summary>
    public enum MenuTypes
    {
        /// <summary>
        /// 神殿
        /// </summary>
        Temple = 1,
        /// <summary>
        /// 商行
        /// </summary>
        TradeHouse = 2,
        /// <summary>
        /// 挖矿-工坊
        /// </summary>
        MiningWorkshop = 3,
        /// <summary>
        /// 挖矿
        /// </summary>
        MiningMap = 4,
        /// <summary>
        /// 招募
        /// </summary>
        Recruit = 5,
        /// <summary>
        /// 基地
        /// </summary>
        BaseRoot = 6,
        /// <summary>
        /// 竞技场
        /// </summary>
        Arena = 7,
        /// <summary>
        /// 商城
        /// </summary>
        Shop = 8,
        /// <summary>
        /// 觉醒
        /// </summary>
        Awaken = 9,
        /// <summary>
        /// 武具升级
        /// </summary>
        WeaponUpgrade = 10,
        /// <summary>
        /// 神器
        /// </summary>
        Artifact = 11,

        /// <summary>
        /// 解雇
        /// </summary>
        UnEmploy = 12,

        /// <summary>
        /// 英雄顺序调整
        /// </summary>
        HeroIndex = 13,
        /// <summary>
        /// 封神之阶
        /// </summary>
        FS = 14,

        /// <summary>
        /// 贸易队
        /// </summary>
        TradeTroop = 15,

        /// <summary>
        /// 秘境功能开启
        /// </summary>
        Rift = 16,
    }

    /// <summary>
    /// 菜单的状态
    /// </summary>
    public enum MenuStatus
    {
        /// <summary>
        /// 关闭状态
        /// </summary>
        Close = 0,

        /// <summary>
        /// 刚解锁
        /// </summary>
        New = 1,

        /// <summary>
        /// 已经取消了
        /// </summary>
        Open = 2,
    }

    /// <summary>
    /// 当前的活动状态
    /// </summary>
    public enum ActiveType
    {
        /// <summary>
        /// 正常收益
        /// </summary>
        Full = 0,

        /// <summary>
        /// 收益减半
        /// </summary>
        Half = 1,

        /// <summary>
        /// 无收益
        /// </summary>
        None = 2,
    }


    /// <summary>
    /// 改名返回类型
    /// </summary>
    public enum RenameResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,

        /// <summary>
        /// 名字重复
        /// </summary>
        NameIsExists = 2,

        /// <summary>
        /// 名字超长
        /// </summary>
        NameToLong = 3,

        /// <summary>
        /// 名字非法
        /// </summary>
        NameIllegal = 4,

        /// <summary>
        /// 血钻不足
        /// </summary>
        LessIgnot = 5,
    }
}
