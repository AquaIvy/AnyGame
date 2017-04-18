﻿using DogSE.Server.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Database
{
    /// <summary>
    /// MongoDB的客户端配置文件
    /// </summary>
    [StaticXmlConfigRoot(@"..\Server.Config", RootName = "MongoDB")]
    public static class MongoDBConfig
    {
        /// <summary>
        /// 数据库地址
        /// </summary>
        public static string Host { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static string Database { get; set; }

        /// <summary>
        /// 是否使用IO缓存
        /// 默认开启
        /// </summary>
        public static bool IOCache { get; set; }

        /// <summary>
        /// Cache每隔多少秒写一次数据库
        /// 单位：秒
        /// </summary>
        public static int CacheWaitTime { get; set; }
    }
}
