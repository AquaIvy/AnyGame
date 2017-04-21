using DogSE.Server.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Logic
{
    /// <summary>
    /// 服务器配置
    /// </summary>
    [StaticXmlConfigRoot(@"..\Server.Config", RootName = "GameConfig")]
    public static class GameConfig
    {
        /// <summary>
        /// 是否GM模式
        /// 开启后全员GM
        /// </summary>
        static public bool IsEnableGM { get; set; }

        /// <summary>
        /// 是否写战斗log
        /// </summary>
        public static bool WriteFightLog { get; set; }

        /// <summary>
        /// 游戏分区id
        /// 注意*1 今后不要直接使用这个值，而是使用  ZoneIds 值，这个值只用在初始化里
        /// </summary>
        public static int ZoneId { get; set; }

        /// <summary>
        /// 游戏合服id
        /// （当这个值不为0的时候，表示游戏服务器是合服的服务器，
        /// 需要从服务器配置里获得对应的合服的分区数据）
        /// </summary>
        public static int MergeZoneId { get; set; }

        /// <summary>
        /// 当前服务器承载的游戏分区id
        /// 注意*1 配置文件里不用配置这个值
        /// 注意*2 服务器端初始化的时候，需要根据当前服务器类型（合服还是非合服，给它赋值）
        /// </summary>
        public static int[] ZoneIds { get; set; }

        /// <summary>
        /// 游戏分区映射
        /// </summary>
        public static readonly Dictionary<int, string> GameZoneMap = new Dictionary<int, string> { };

    }

}
