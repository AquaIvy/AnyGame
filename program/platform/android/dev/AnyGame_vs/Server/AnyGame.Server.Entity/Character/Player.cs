using DogSE.Common;
using DogSE.Server.Core.Net;
using MongoDB.Bson.Serialization.Attributes;
using System;
using AnyGame.Server.Entity.Bags;
using AnyGame.Server.Entity.Common;
using MongoDB.Bson;

namespace AnyGame.Server.Entity.Character
{
    /// <summary>
    /// 玩家角色（存储数据）
    /// </summary>
    public partial class Player : IDataEntity
    {
        public Player()
        {
            Property = new Property();
        }

        /// <summary>
        /// 玩家的唯一标识
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 理论上id和账号id一一对应，这里的目的是为了单个账号对应多角色做的冗余设计
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Representation = BsonType.DateTime, Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        [BsonDateTimeOptions(Representation = BsonType.DateTime, Kind = DateTimeKind.Local)]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 上一次登出时间
        /// </summary>
        [BsonDateTimeOptions(Representation = BsonType.DateTime, Kind = DateTimeKind.Local)]
        public DateTime LastLogoffTime { get; set; }

        /// <summary>
        /// 上一次收到的心跳包
        /// </summary>
        public DateTime LastHeartbeat { get; set; }

        /// <summary>
        /// 累计的在线时间，在离线时修改
        /// 单位：秒
        /// </summary>
        /// <remarks>
        /// 不用精确到毫秒，因为玩家的在线时间是一个很长的时间，
        /// 毫秒的单位对大的数据来说不会产生偏差影响
        /// </remarks>
        public int OnlineTime { get; set; }


        /// <summary>
        /// 是否是GM账号
        /// </summary>
        public bool IsSuperMan { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public Property Property { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 当前等级的经验
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// 累计经验
        /// </summary>
        public long ExpSum { get; set; }

        /// <summary>
        /// 网络对象
        /// </summary>
        [BsonIgnore]
        public NetState NetState { get; set; }

        /// <summary>
        /// 玩家的资源
        /// </summary>
        [BsonIgnore]
        public Res Res { get; set; }

        /// <summary>
        /// 玩家的背包
        /// </summary>
        [BsonIgnore]
        public Bag Bag { get; set; }
    }
}
