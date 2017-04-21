using DogSE.Common;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

#if Server 
using AnyGame.Server.Entity.Common;
namespace AnyGame.Server.Entity.Bags
#else
using AnyGame.Client.Entity.Common;
namespace AnyGame.Client.Entity.Bags
#endif
{
    /// <summary>
    /// 玩家的整个背包数据
    /// </summary>
    /// <remarks>
    /// 可以在背包中看到的物品
    /// </remarks>
    [BsonIgnoreExtraElements]
    public class Bag : IDataEntity
    {
        public Bag()
        {
            Items = new List<GameItem>();
        }

        /// <summary>
        /// 玩家的唯一标示
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 当前背包等级
        /// </summary>
        public int CurBagLevel { get; set; }

        /// <summary>
        /// 最多格子数量
        /// </summary>
        public int MaxGridCount { get; set; }

        /// <summary>
        /// 物品索引id
        /// </summary>
        public int MaxItemId { get; set; }

        /// <summary>
        /// 玩家的物品列表
        /// </summary>
        public List<GameItem> Items { get; set; }
    }


    /// <summary>
    /// 玩家的游戏物品
    /// </summary>
    public class GameItem
    {
        /// <summary>
        /// 物品唯一id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 物品模板id 
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int Num { get; set; }
    }
}
