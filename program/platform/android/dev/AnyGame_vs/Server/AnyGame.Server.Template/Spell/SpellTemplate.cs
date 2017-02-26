using DogSE.Library.Serialize;
using DogSE.Server.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Template.Spell
{
    [DynamicCSVConfigRoot(@"..\ConfigData\Spell.csv", "Spell")]
    public class SpellTemplate
    {
        /// <summary>
        /// 模版ID
        /// </summary>
        [CSVColumn("ID")]
        public int Id { get; set; }

        /// <summary>
        /// 卡牌名字
        /// </summary>
        [CSVColumn("名字")]
        public string Name { get; set; }

        /// <summary>
        /// 卡牌描述
        /// </summary>
        [CSVColumn("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 物法类型
        /// </summary>
        [CSVColumn("物法类型")]
        public int 物法类型 { get; set; }

        /// <summary>
        /// 主被类型
        /// </summary>
        [CSVColumn("主被类型")]
        public int 主被类型 { get; set; }

        /// <summary>
        /// 伤害类型
        /// </summary>
        [CSVColumn("伤害类型")]
        public int 伤害类型 { get; set; }

        /// <summary>
        /// 常规类型
        /// </summary>
        [CSVColumn("常规类型")]
        public int 常规类型 { get; set; }

        /// <summary>
        /// 消耗气血
        /// </summary>
        [CSVColumn("消耗气血")]
        public int CostHp { get; set; }

        /// <summary>
        /// 消耗魔法
        /// </summary>
        [CSVColumn("消耗魔法")]
        public int CostMp { get; set; }

        /// <summary>
        /// 气血影响
        /// </summary>
        [CSVColumn("气血影响")]
        public int HpChange { get; set; }

        /// <summary>
        /// 魔法影响
        /// </summary>
        [CSVColumn("魔法影响")]
        public int MpChange { get; set; }
    }
}
