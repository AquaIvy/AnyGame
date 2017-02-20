using DogSE.Common;

namespace TradeAge.Server.Entity.Bags
{
    /// <summary>
    /// 
    /// </summary>
    public class Bag : IDataEntity
    {
        /// <summary>
        /// 玩家的唯一标示
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 最大数量
        /// </summary>
        public int MaxCount { get; set; }


        /// <summary>
        /// 当前物品数量
        /// </summary>
        public int CurCount { get; set; }
    }
}
