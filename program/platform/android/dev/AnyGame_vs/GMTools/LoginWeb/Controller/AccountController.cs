using LoginWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginWeb.Utils;
using System.Text;
using AnyGame.Server.Entity.Common;
using AnyGame.Server.Entity.Bags;

namespace LoginWeb.Controller
{
    public enum BindAccountResult
    {
        Success = 0,
        NotFindAccount,
        IsBindPlayer,
        Fail,
    }

    public class AccountController
    {
        private static readonly Random Rand = new Random();

        #region 获得平台登陆令牌

        /// <summary>
        /// 获得平台登陆令牌
        /// </summary>
        /// <returns></returns>
        internal static string GetLoginToken(PlatformAccount account)
        {
            string baseStr = string.Format("aid={0}&ptype={1}", account.Id, account.PlatformId);

            var token = (baseStr + "!@#abc").GetMd5().Substring(0, 10);

            return EncryptionBase64(string.Format("{0}&token={1}", baseStr, token));
        }

        /// <summary>
        /// 通过数据变换方法加密为base64的字符串
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        internal static string EncryptionBase64(string baseStr)
        {
            var buff = Encoding.UTF8.GetBytes(baseStr);
            var newBuff = new byte[buff.Length + 2];

            newBuff[0] = (byte)(Rand.Next() % 255);
            newBuff[newBuff.Length - 1] = (byte)(Rand.Next() % 255);

            var diff = (byte)(newBuff[0] + newBuff[newBuff.Length - 1]);

            for (int i = 0; i < buff.Length; i++)
            {
                newBuff[i + 1] = (byte)((buff[i] ^ diff));
            }
            var base64 = Convert.ToBase64String(newBuff);
            return base64;
        }

        /// <summary>
        /// 把base64的字符串解密后成为标准的字符串
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        internal static string DecryptFromBase64(string base64)
        {
            var buff = Convert.FromBase64String(base64);

            var outBuff = new byte[buff.Length - 2];

            var diff = buff[0] + buff[buff.Length - 1];
            for (int i = 0; i < outBuff.Length; i++)
            {
                outBuff[i] = (byte)((buff[i + 1] ^ diff));
            }
            return Encoding.UTF8.GetString(outBuff);
        }

        #endregion

        #region 查找、创建 平台账户

        /// <summary>
        /// 创建平台账户
        /// </summary>
        /// <param name="phoneId"></param>
        /// <param name="uId"></param>
        /// <param name="platformTypes"></param>
        /// <returns></returns>
        internal static PlatformAccount CreatePlatformAccount(string phoneId, string uId, PlatformTypes platformTypes)
        {
            var account = new PlatformAccount();

            account.PhoneId = phoneId;
            account.UId = uId;
            account.PlatformId = (int)platformTypes;

            DB.AccountDB.InsertEntity(account);

            return account;
        }

        /// <summary>
        /// 通过第三方平台获得平台账户数据
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PlatformAccount GetPlatformAccount(string uid, PlatformTypes type)
        {
            return DB.AccountDB.QueryEntity<PlatformAccount>(string.Format("UId='{0}' and PlatformId={1}", uid, (int)type));
        }

        /// <summary>
        /// 根据平台id, 查找玩家账户
        /// </summary>
        /// <param name="PlatformAccountId"></param>
        /// <returns></returns>
        internal static GameAccount[] GetGameAccountByPlatformId(int PlatformAccountId)
        {
            return DB.AccountDB.QueryEntitys<GameAccount>(string.Format("PlatformAccountId='{0}'", PlatformAccountId));
        }

        /// <summary>
        /// 根据平台id, 查找玩家账户
        /// </summary>
        /// <param name="PlatformAccountId"></param>
        /// <returns></returns>
        internal static PlatformAccount[] GetGameAccountByPhoneId(string phoneId)
        {
            return DB.AccountDB.QueryEntitys<PlatformAccount>(string.Format("PhoneId='{0}'", phoneId));
        }

        /// <summary>
        /// 通过玩家Id获得平台账号对象
        /// </summary>
        /// <param name="playerId">玩家id（可视为accountId)</param>
        /// <returns></returns>
        internal static PlatformAccount GetPlatformAccountByAccountId(int playerId)
        {
            var ga = DB.AccountDB.LoadEntity<GameAccount>(playerId);
            if (ga == null)
                return null;

            return DB.AccountDB.LoadEntity<PlatformAccount>(ga.PlatformAccountId);
        }

        #endregion

        #region 获得公告

        private static Notice[] s_notice;

        /// <summary>
        /// 游戏内的公告列表
        /// </summary>
        public static Notice[] Notices
        {
            get
            {
                if (s_notice == null)
                {
                    s_notice = DB.GameMasterDB.LoadEntitys<Notice>();
                    s_notice.ToList().ForEach(o => o.PlatformTypes = GameZone2.ParsePlatformTypesList(o.PlatformIds));
                }

                return s_notice;
            }
        }

        /// <summary>
        /// 获得公告
        /// </summary>
        /// <param name="platformId"></param>
        /// <param name="phoneType">手机平台</param>
        /// <returns></returns>
        public static Notice GetNotice(int platformId, PhonePlatformTypes phoneType)
        {
            var n2 = Notices.Where(o => o.PhonePlatform == phoneType).ToArray();
            var notice = n2.FirstOrDefault(o => o.PlatformTypes.Contains((PlatformTypes)platformId));
            if (notice == null && n2.Length > 0)
                return n2.FirstOrDefault(o => o.IsDefaultNotice);

            return Notices.FirstOrDefault();
        }

        /// <summary>
        /// 清理数据缓存
        /// </summary>
        public static void ClearDataCache()
        {
            s_notice = DB.GameMasterDB.LoadEntitys<Notice>();
            s_serverinfo = DB.GameMasterDB.LoadEntitys<ServerInfo>();

            s_notice.ToList().ForEach(o => o.PlatformTypes = GameZone2.ParsePlatformTypesList(o.PlatformIds));

            s_gameZone = null;
        }

        #endregion

        #region 获得游戏分区（服务器）

        private static GameZone2[] s_gameZone;

        /// <summary>
        /// 游戏分区（这里会隐藏未开放的服务器）
        /// </summary>
        private static GameZone2[] GameZone
        {
            get
            {
                if (s_gameZone == null)
                {
                    s_gameZone = DB.GameMasterDB.LoadEntitys<GameZone2>().Where(o => o.Status != -1).ToArray();
                    foreach (var zone in s_gameZone)
                    {
                        zone.GameServers = GameServer.Where(o => o.Id == zone.Id).ToList();
                    }
                }

                return s_gameZone;
            }
        }

        /// <summary>
        /// 游戏分区（这里会隐藏未开放的服务器）
        /// </summary>
        /// <param name="phoneType">手机平台</param>
        /// <param name="type">sdk平台</param>
        /// <param name="version">客户端版本号</param>
        public static GameZone2[] GetLoginGameZone(PhonePlatformTypes phoneType, PlatformTypes type, string version)
        {
            List<GameZone2> ret = new List<GameZone2>();
            if (version == null)
                version = string.Empty;

            foreach (var zone in GameZone)
            {
                if (zone.PhonePlatformType != phoneType)
                    continue;

                if (zone.WhitelistPlatform.Count > 0)
                {
                    //  存在白名单，并且不再白名单里面，则直接返回
                    if (!zone.WhitelistPlatform.Contains(type))
                        continue;
                }

                if (zone.BlacklistPlatform.Contains(type))
                    continue;

                if (zone.WhitelistVersion.Count > 0)
                {
                    //  存在版本白名单，不再白名单里面，则直接返回
                    if (!zone.WhitelistVersion.Contains(version))
                        continue;
                }

                if (zone.BlacklistVersion.Contains(version))
                    continue;

                ret.Add(zone);
            }
            return ret.OrderByDescending(o => o.Id).ToArray();
        }


        #endregion

        #region 游戏物理服务器
        private static ServerInfo[] s_serverinfo;

        /// <summary>
        /// 游戏物理服务器
        /// </summary>
        public static ServerInfo[] GameServer
        {
            get
            {
                if (s_serverinfo == null)
                {
                    s_serverinfo = DB.GameMasterDB.LoadEntitys<ServerInfo>();
                }

                return s_serverinfo;
            }
        }
        #endregion

    }
}