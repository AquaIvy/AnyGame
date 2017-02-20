using System;
using DogSE.Common;

namespace LoginWeb.Entity
{
    /// <summary>
    ///     服务器信息（仅指物理服务器）
    /// </summary>
    public class ServerInfo : IDataEntity
    {
        /// <summary>
        ///  物理服务器的服务id唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 服务器属于那个分区
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// 服务器在服务器分区里属于那个编号
        /// </summary>
        public int Mod { get; set; }

        /// <summary>
        ///     服务器名字
        /// </summary>
        public string ServerName { get; set; }


        /// <summary>
        ///     游戏服务器地址（ip或者域名）
        /// </summary>
        public string ServerHost { get; set; }

        /// <summary>
        /// 游戏服务器在局域网内的地址
        /// 服务器地址是外网访问
        /// 而局域网的则是对内网访问
        /// </summary>
        public string LanServerHost { get; set; }

        /// <summary>
        ///     服务器的端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        ///     没有元数据文档可用。
        /// </summary>
        public int State { get; set; }

        /// <summary>
        ///     服务器wcf管理端口
        /// </summary>
        public int ManagerPort { get; set; }

        /// <summary>
        ///     游戏数据库地址。
        /// </summary>
        public string DatabaseHost { get; set; }

        /// <summary>
        ///     数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        ///     分析数据库地址。
        /// </summary>
        public string AnalysisDatabaseHost { get; set; }

        /// <summary>
        ///     分析库的名字。
        /// </summary>
        public string AnalysisDatabaseName { get; set; }

        /// <summary>
        ///     商户（平台）
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        ///     服务器域名。
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        ///     开服的时间。
        /// </summary>
        public DateTime StartServer { get; set; }

        /// <summary>
        ///     md5验证key。
        /// </summary>
        public string SecureKey { get; set; }

        /// <summary>
        ///     服务器类型。
        /// </summary>
        public int ServerType { get; set; }
    }
}