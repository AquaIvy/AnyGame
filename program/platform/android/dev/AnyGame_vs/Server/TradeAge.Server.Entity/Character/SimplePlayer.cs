﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Common;

#if Server 
using TradeAge.Server.Entity.Common;
namespace TradeAge.Server.Entity.Character
#else
using TradeAge.Client.Entity.Common;
namespace TradeAge.Client.Entity.Character
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
