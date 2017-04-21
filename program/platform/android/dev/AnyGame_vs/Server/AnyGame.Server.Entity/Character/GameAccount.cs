using DogSE.Common;
using IvyOrm;
using System;

namespace AnyGame.Server.Entity.Bags
{
    /// <summary>
    /// 游戏内的账户表
    /// </summary>
    public class GameAccount : IDataEntity
    {
        /// <summary>
        /// 账号id，和玩家id里路上一一对应
        /// </summary>
        [PrimaryKey(PrimaryKeyOptions.ReturnKeyValueOnInsert)]
        public int Id { get; set; }

        /// <summary>
        /// 平台账户id
        /// </summary>
        public int PlatformAccountId { get; set; }

        /// <summary>
        ///  平台id,可以和内部的枚举值一一对应(只用于查询）
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 分配的服务器id
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// 是否是超级账号
        /// </summary>
        public bool IsSuperMan { get; set; }

        /// <summary>
        /// 创建角色之后的角色名，仅用于对外查询
        /// </summary>
        public string CharacterName { get; set; }

        /// <summary>
        /// 玩家从哪个渠道来的
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFrozen { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
