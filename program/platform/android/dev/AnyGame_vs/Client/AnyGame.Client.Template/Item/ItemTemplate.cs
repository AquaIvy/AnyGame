using DogSE.Client.Core.Config;
using DogSE.Library.Serialize;

namespace AnyGame.Client.Template.Item
{
    /// <summary>
    ///     物品模板
    /// </summary>
    [DynamicCSVConfigRoot(@"Item.csv", "Item")]
    public class ItemTemplate
    {
        /// <summary>
        ///     物品模板id
        /// </summary>
        [CSVColumn("ID")]
        public int Id { get; set; }

        /// <summary>
        ///     物品名字
        /// </summary>
        [CSVColumn("名字")]
        public string Name { get; set; }

        /// <summary>
        ///     物品类型
        /// </summary>
        [CSVColumn("类型")]
        public ItemType Type { get; set; }

        /// <summary>
        /// 扩展类型
        /// </summary>
        [CSVColumn("扩展类型")]
        public ItemTypeEx ExType { get; set; }

        /// <summary>
        ///     出售后获得金币
        /// </summary>
        [CSVColumn("出售价格")]
        public int SellMoney { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [CSVColumn("图标")]
        public int IconId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [CSVColumn("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 商城id
        /// </summary>
        [CSVColumn("商城id")]
        public int ShopId { get; set; }

        /// <summary>
        /// 商城价格
        /// </summary>
        [CSVColumn("商城价格")]
        public int ShopPrice { get; set; }

        /// <summary>
        /// 奖励描述
        /// </summary>
        [CSVColumnAttribute("奖励描述")]
        public string RewardDescription { get; set; }

        /// <summary>
        /// 无类型
        /// </summary>
        [CSVColumnAttribute("物品分类")]
        public ItemType2 ItemType2 { get; set; }

        /// <summary>
        /// 碎片合成数量
        /// </summary>
        [CSVColumnAttribute("合成所需")]
        public int MergeCount { get; set; }

        /// <summary>
        /// 参数1
        /// 在杂物里表示金币
        /// </summary>
        [CSVColumn("参数1")]
        public int Param1 { get; set; }

        /// <summary>
        /// 参数2
        /// 在杂物里表示经验
        /// </summary>
        [CSVColumn("参数2")]
        public int Param2 { get; set; }

    }

    /// <summary>
    /// 物品类型2
    /// </summary>
    public enum ItemType2
    {
        /// <summary>
        /// 消耗品
        /// </summary>
        Consumption = 1,

        /// <summary>
        /// 碎片
        /// </summary>
        Fragment = 2,

        /// <summary>
        /// 杂物
        /// </summary>
        Debris = 3
    }

    /// <summary>
    ///     物品类型
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 属性固定
        /// </summary>
        Fix = 0,

        /// <summary>
        /// 属性随机
        /// </summary>
        Random = 1,
    }

    /// <summary>
    /// 物品类型扩展
    /// </summary>
    public enum ItemTypeEx
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 挖矿工具包
        /// </summary>
        MiningToolkit = 1,
    }
}
