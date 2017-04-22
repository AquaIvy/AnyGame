using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Common;

#if Server 
using AnyGame.Server.Entity.Character;
namespace AnyGame.Server.Entity.Character
#else
using AnyGame.Client.Entity.Common;
namespace AnyGame.Client.Entity.Bags
#endif
{
    /// <summary>
    /// 简单玩家信息
    /// </summary>
    public class SimplePlayer
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
    }
}
