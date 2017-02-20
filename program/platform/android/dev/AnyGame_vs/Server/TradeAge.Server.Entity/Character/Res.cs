using DogSE.Common;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeAge.Server.Entity.Character
{
    /// <summary>
    /// 玩家的资源
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Res : IDataEntity
    {
        public Res()
        {

        }

        /// <summary>
        /// 玩家的id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 金币
        /// </summary>
        public int Gold { get; set; }

        /// <summary>
        /// 钻石
        /// </summary>
        public int Gem { get; set; }
    }
}
