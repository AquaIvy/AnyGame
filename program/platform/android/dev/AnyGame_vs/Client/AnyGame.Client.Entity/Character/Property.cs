using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server 
namespace AnyGame.Server.Entity.Character
#else
namespace AnyGame.Client.Entity.Bags
#endif
{
    public class Property
    {
        public int HP { get; set; }
        public int MP { get; set; }

        /// <summary>
        /// 体力
        /// </summary>
        public int Physical { get; set; }

        /// <summary>
        /// 魔力
        /// </summary>
        public int Mana { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        public int Endurance { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agility { get; set; }
    }
}
