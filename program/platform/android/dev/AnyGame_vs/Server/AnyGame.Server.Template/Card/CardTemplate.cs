using DogSE.Library.Serialize;
using DogSE.Server.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Template.Card
{
    [DynamicCSVConfigRoot(@"..\ConfigData\Card.csv", "Card")]
    public class CardTemplate
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
        /// 显示排序索引
        /// </summary>
        [CSVColumn("排序索引")]
        public int Index { get; set; }

        /// <summary>
        /// 卡牌品质
        /// </summary>
        [CSVColumn("品质")]
        public int Quality { get; set; }

        /// <summary>
        /// 卡牌图片
        /// </summary>
        [CSVColumn("卡牌图片")]
        public string Picture { get; set; }

        /// <summary>
        /// 气血
        /// </summary>
        [CSVColumn("气血")]
        public int Hp { get; set; }

        /// <summary>
        /// 魔法
        /// </summary>
        [CSVColumn("魔法")]
        public int Mp { get; set; }

        #region 属性

        /// <summary>
        /// 命中
        /// </summary>
        [CSVColumn("命中")]
        public int HitRate { get; set; }

        /// <summary>
        /// 伤害
        /// </summary>
        [CSVColumn("伤害")]
        public int Damage { get; set; }

        /// <summary>
        /// 防御
        /// </summary>
        [CSVColumn("防御")]
        public int Defense { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        [CSVColumn("速度")]
        public int Speed { get; set; }

        /// <summary>
        /// 法伤
        /// </summary>
        [CSVColumn("法伤")]
        public int SpellDamage { get; set; }

        /// <summary>
        /// 法防
        /// </summary>
        [CSVColumn("法防")]
        public int SpellDefense { get; set; }

        #endregion

        #region 潜力点

        /// <summary>
        /// 体质
        /// </summary>
        [CSVColumn("体质")]
        public int Physique { get; set; }

        /// <summary>
        /// 魔力
        /// </summary>
        [CSVColumn("魔力")]
        public int Mana { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        [CSVColumn("力量")]
        public int Strength { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        [CSVColumn("耐力")]
        public int Endurance { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        [CSVColumn("敏捷")]
        public int Agility { get; set; }

        #endregion

        #region 技能

        /// <summary>
        /// 技能
        /// </summary>
        [CSVColumn("技能")]
        public List<int> Spells { get; set; }

        /// <summary>
        /// 觉醒技能
        /// </summary>
        [CSVColumn("觉醒技能")]
        public int AwakeSpell { get; set; }

        #endregion

    }
}
