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
        /// 卡牌模版ID
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
        /// 竞技场解锁等级
        /// </summary>
        [CSVColumn("解锁等级")]
        public int UnlockLevel { get; set; }

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
        /// 卡牌等级
        /// </summary>
        [CSVColumn("等级")]
        public List<int> Level { get; set; }

        /// <summary>
        /// 血量
        /// </summary>
        [CSVColumn("血量")]
        public List<int> Hp { get; set; }

        /// <summary>
        /// 攻击伤害
        /// </summary>
        [CSVColumn("攻击伤害")]
        public List<float> Damage { get; set; }

        /// <summary>
        /// 对塔伤害
        /// </summary>
        [CSVColumn("对塔伤害")]
        public List<float> TowerDamage { get; set; }

        /// <summary>
        /// 攻击CD
        /// </summary>
        [CSVColumn("攻击CD")]
        public int AttackCD { get; set; }

        /// <summary>
        /// 移动速度
        /// </summary>
        [CSVColumn("移动速度")]
        public int Speed { get; set; }

    }
}
