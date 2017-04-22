using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Interface.Client
{
    /// <summary>
    /// 客户端接口代理
    /// </summary>
    public static class ClientProxy
    {
        /// <summary>
        /// 登陆代理接口
        /// </summary>
        public static ILogin Login { get; set; }

        /// <summary>
        /// 游戏相关的接口
        /// </summary>
        public static IGame Game { get; set; }

        /// <summary>
        /// GM的一些操作
        /// </summary>
        public static IGameSystem System { get; set; }

        /// <summary>
        /// 玩家角色相关接口
        /// </summary>
        public static IPlayer Player { get; set; }

        /// <summary>
        /// 背包
        /// </summary>
        public static IBag Bag { get; set; }

    }
}
