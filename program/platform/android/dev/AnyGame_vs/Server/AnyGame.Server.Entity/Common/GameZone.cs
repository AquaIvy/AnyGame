using DogSE.Common;
using DogSE.Library.Log;
using IvyOrm;
using System;
using System.Collections.Generic;

namespace AnyGame.Server.Entity.Common
{
    /// <summary>
    /// 游戏分区
    /// </summary>
    public class GameZone : IDataEntity
    {
        /// <summary>
        /// 分区id
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色分区取余数
        /// </summary>
        public int Mods { get; set; }

        /// <summary>
        /// 分区类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 手机平台类型
        /// </summary>
        public PhonePlatformTypes PhonePlatformType { get; set; }

        /// <summary>
        /// 是否限制激活（限制创建新用户）
        /// </summary>
        public bool LimitActive { get; set; }

        /// <summary>
        /// 渠道黑名单
        /// 当不不限制激活用户的时候用这个做屏蔽
        /// </summary>
        public string ChannelBlacklist { get; set; }

        /// <summary>
        /// 渠道白名单
        /// 当限制激活的时候，这些渠道可以进入游戏
        /// </summary>
        public string ChannelWhitelist { get; set; }

        private List<string> _channelBlacklist;

        /// <summary>
        /// 黑名单渠道列表
        /// </summary>
        public List<string> Blacklist
        {
            get
            {
                if (_channelBlacklist == null)
                {
                    _channelBlacklist = new List<string>();
                    if (!string.IsNullOrEmpty(ChannelBlacklist))
                        _channelBlacklist.AddRange(ChannelBlacklist.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                return _channelBlacklist;
            }
        }

        private List<string> _channelWhilelist;
        /// <summary>
        /// 白名单渠道列表
        /// </summary>
        public List<string> Whitelist
        {
            get
            {
                if (_channelWhilelist == null)
                {
                    _channelWhilelist = new List<string>();
                    if (!string.IsNullOrEmpty(ChannelWhitelist))
                        _channelWhilelist.AddRange(ChannelWhitelist.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                return _channelWhilelist;
            }
        }


        /// <summary>
        /// 版本黑名单
        /// 当不不限制激活用户的时候用这个做屏蔽
        /// </summary>
        public string VersionBlacklist { get; set; }

        /// <summary>
        /// 版本白名单
        /// 当限制激活的时候，这些渠道可以进入游戏
        /// </summary>
        public string VersionWhitelist { get; set; }

        private List<string> _versionBlacklist;

        /// <summary>
        /// 黑名单渠道列表
        /// </summary>
        public List<string> BlacklistVersion
        {
            get
            {
                if (_versionBlacklist == null)
                {
                    _versionBlacklist = new List<string>();
                    if (!string.IsNullOrEmpty(VersionBlacklist))
                        _versionBlacklist.AddRange(VersionBlacklist.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                return _versionBlacklist;
            }
        }

        private List<string> _versionWhilelist;
        /// <summary>
        /// 白名单渠道列表
        /// </summary>
        public List<string> WhitelistVersion
        {
            get
            {
                if (_versionWhilelist == null)
                {
                    _versionWhilelist = new List<string>();
                    if (!string.IsNullOrEmpty(VersionWhitelist))
                        _versionWhilelist.AddRange(VersionWhitelist.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries));
                }
                return _versionWhilelist;
            }
        }


        /// <summary>
        /// 平台黑名单
        /// 
        /// </summary>
        public string PlatformBlacklist { get; set; }

        /// <summary>
        /// 平台白名单
        /// 当限制激活的时候，这些渠道可以进入游戏
        /// </summary>
        public string PlatformWhitelist { get; set; }

        private List<PlatformTypes> _plaoformBlacklist;

        /// <summary>
        /// 平台黑名单列表
        /// </summary>
        public List<PlatformTypes> BlacklistPlatform
        {
            get
            {
                if (_plaoformBlacklist == null)
                {
                    _plaoformBlacklist = ParsePlatformTypesList(PlatformBlacklist);
                }
                return _plaoformBlacklist;
            }
        }

        private List<PlatformTypes> _plaoformWhilelist;

        /// <summary>
        /// 平台白名单列表
        /// </summary>
        public List<PlatformTypes> WhitelistPlatform
        {
            get
            {
                if (_plaoformWhilelist == null)
                {
                    _plaoformWhilelist = ParsePlatformTypesList(PlatformWhitelist);
                }
                return _plaoformWhilelist;
            }
        }


        /// <summary>
        /// 将平台的字符串转换为枚举list
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<PlatformTypes> ParsePlatformTypesList(string str)
        {
            List<PlatformTypes> ret = new List<PlatformTypes>();

            if (string.IsNullOrEmpty(str))
                return ret;

            foreach (var s in str.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int i;
                if (int.TryParse(s, out i))
                {
                    ret.Add((PlatformTypes)i);
                }
                else
                {
                    PlatformTypes o;
                    if (Enum.TryParse(s, out o))
                        ret.Add(o);
                    else
                    {
                        Logs.Error("{0} 不能转换为 PlatformTypes类型", s);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 合服id
        /// </summary>
        public int MergeGameZoneId { get; set; }
    }
}
