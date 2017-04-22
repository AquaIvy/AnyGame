using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnyGame.Client.Entity.Character;

namespace AnyGame.Client.Entity
{
    /// <summary>
    /// 和玩家自身相关的模型数据
    /// </summary>
    public class EntityModel
    {
        public EntityModel()
        {
            Player = new Player();
            Bag = new Bag();
            Res = new Res();
        }

        /// <summary>
        /// 玩家自己
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// 背包信息
        /// </summary>
        public Bag Bag { get; set; }

        /// <summary>
        /// 资源信息
        /// </summary>
        public Res Res { get; set; }
    }
}
