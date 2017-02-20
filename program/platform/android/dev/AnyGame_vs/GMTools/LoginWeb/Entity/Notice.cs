using DogSE.Common;
using IvyOrm;
using System.Collections.Generic;
using TradeAge.Server.Entity.Common;

namespace LoginWeb.Entity
{
    /// <summary>
    /// 公告
    /// </summary>
    public class Notice : IDataEntity
    {
        [PrimaryKey(PrimaryKeyOptions.ReturnKeyValueOnInsert)]
        public int Id { get; set; }

        /// <summary>
        /// 填充用，无实际意义
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版本内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 平台Id勾选的列表
        /// </summary>
        public string PlatformIds { get; set; }


        /// <summary>
        /// 平台列表
        /// </summary>
        public List<PlatformTypes> PlatformTypes { get; set; }

        /// <summary>
        /// 是否是默认公告
        /// </summary>
        public bool IsDefaultNotice { get; set; }

        /// <summary>
        /// 手机平台
        /// </summary>
        public PhonePlatformTypes PhonePlatform { get; set; }
    }
}