using DogSE.Common;
using IvyOrm;

namespace AnyGame.Server.Entity.Bags
{
    /// <summary>
    /// 平台账户表
    /// </summary>
    public class PlatformAccount : IDataEntity
    {
        /// <summary>
        /// 数据库Id
        /// </summary>
        [PrimaryKey(PrimaryKeyOptions.ReturnKeyValueOnInsert)]
        public int Id { get; set; }

        /// <summary>
        /// 手机Id（u3d给的）
        /// </summary>
        public string PhoneId { get; set; }

        /// <summary>
        /// 账户名（平台对应的账户id)
        /// </summary>
        public string UId { get; set; }

        /// <summary>
        /// 来源id（平台id)
        /// </summary>
        public int PlatformId { get; set; }

        /// <summary>
        /// 第三方平台id
        /// </summary>
        public string ThreeUId { get; set; }

        /// <summary>
        /// 第三方平台来源id
        /// 如果 大于0 表示是由第三方平台创建的账号
        /// </summary>
        public int ThreePlatformId { get; set; }

        /// <summary>
        /// 玩家已激活的游戏分区
        /// </summary>
        public string GameZoneIds { get; set; }

        /// <summary>
        /// GM帐号
        /// </summary>
        public bool IsSuperMan { get; set; }
    }
}