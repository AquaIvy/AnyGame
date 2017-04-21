using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Client.Entity.Bags
{
    /// <summary>
    /// 玩家的资源
    /// </summary>
    /// <remarks>
    /// 在背包中看不到的数据，如：金币
    /// </remarks>
    public class Res 
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
