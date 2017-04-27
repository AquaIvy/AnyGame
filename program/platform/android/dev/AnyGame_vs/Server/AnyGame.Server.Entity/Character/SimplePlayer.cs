using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Common;
using AnyGame.Server.Entity.Character;
using MongoDB.Bson.Serialization.Attributes;

namespace AnyGame.Server.Entity.Character
{
    /// <summary>
    /// 简单玩家信息
    /// </summary>
    [BsonIgnoreExtraElements]
    public class SimplePlayer : IDataEntity
    {
        /// <summary>
        /// 玩家的id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 玩家对应的账号id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}
