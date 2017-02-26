using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Template.Spell
{
    enum 常规类型
    {
        /// <summary>
        /// 常规技能
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 觉醒技能
        /// </summary>
        Awake = 2
    }

    enum 物法类型
    {
        /// <summary>
        /// 物理技能
        /// </summary>
        Physics = 1,

        /// <summary>
        /// 法术技能
        /// </summary>
        Spell = 2
    }

    enum 主被类型
    {
        /// <summary>
        /// 主动技能
        /// </summary>
        Initiative = 1,

        /// <summary>
        /// 被动技能
        /// </summary>
        Passive = 2
    }

    enum 伤害类型
    {
        /// <summary>
        /// 物理伤害
        /// </summary>
        Physics = 1,

        /// <summary>
        /// 法术伤害
        /// </summary>
        Spell = 2
    }


}
