using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DogSE.Server.Core.Config;

namespace TradeAge.Server.Database
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [StaticXmlConfigRoot(@"..\Server.Config", RootName = "DBConfig")]
    public static class DBConfig
    {
        /// <summary>
        /// 账户内容的Mysql连接字符串
        /// </summary>
        public static string AccountMySqlConnectString { get; set; }

        /// <summary>
        /// 分析库的mysql地址
        /// </summary>
        public static string AnalysisMysqlConnectString { get; set; }

        /// <summary>
        /// 账户内容的Mysql连接字符串
        /// </summary>
        public static string GameMasterMySqlConnectString { get; set; }
    }
}
