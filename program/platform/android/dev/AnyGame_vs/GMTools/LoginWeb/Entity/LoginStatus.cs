using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogSE.Common;
using IvyOrm;

namespace LoginWeb.Entity
{
    /// <summary>
    /// 登录状态
    /// 暂时先忽略这个状态
    /// </summary>
    public class LoginStatus : IDataEntity
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
        /// 来源id（平台id)
        /// </summary>
        public int SouceId { get; set; }

        /// <summary>
        /// 账户全局唯一id(格式：SouceId_AccountName)
        /// </summary>
        public string AccountUniqueName { get; set; }

        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 最后一个登录关联的账户id
        /// </summary>
        public int LastLoginAccount { get; set; }

        /// <summary>
        /// 最后一次登录的游戏分区
        /// </summary>
        public int LastLoginGameZoneId { get; set; }
    }
}