using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyGame.Client.Entity.Character;
using AnyGame.Client.Entity.Bags;

namespace AnyGame.Client.Entity
{
    /// <summary>
    /// 和玩家自身相关的模型数据
    /// </summary>
    public class EntityModel
    {
        public EntityModel()
        {
            Player = new SimplePlayer();
            Bag = new Bag();
        }

        /// <summary>
        /// 玩家自己
        /// </summary>
        public SimplePlayer Player { get; set; }

        /// <summary>
        /// 背包信息
        /// </summary>
        public Bag Bag { get; set; }
    }
}
