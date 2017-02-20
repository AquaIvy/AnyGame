using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DogSE.Library.Log;

namespace LoginWeb.Entity
{
    /// <summary>
    /// 服务器状态信息
    /// </summary>
    public class LoginResult
    {
        public LoginResult()
        {
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 登录令牌(通用于登录所有服务器）
        /// </summary>
        public string LoginToken { get; set; }

        /// <summary>
        /// 公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 公告版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 平台昵称（不一定所有平台都会有的）
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 平台用户id
        /// </summary>
        public string PlatformAccountId { get; set; }

        /// <summary>
        /// 最后一次登录的游戏分区
        /// </summary>
        public int LastLoginGameZoneId { get; set; }

        /// <summary>
        /// 服务器列表
        /// </summary>
        public List<GameServer> GameServers { get; set; }
    }

    /// <summary>
    /// 游戏服务器
    /// </summary>
    public class GameServer
    {
        /// <summary>
        /// 服务器id
        /// </summary>
        public int GameZoneId { get; set; }

        /// <summary>
        /// 服务器名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务器状态
        /// </summary>
        public ServerStatus Status { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 玩家在服务器里的角色名
        /// </summary>
        public string CharacterName { get; set; }

#if DEBUG
        public override string ToString()
        {
            return string.Format("GameZoneId={0}  Name={1}    Status={2}  ServerAddress={3}   Port={4}", GameZoneId, Name, Status, Host, Port);
        }
#endif
    }

    /// <summary>
    /// 服务器状态
    /// </summary>
    public enum ServerStatus
    {
        /// <summary>
        /// 维护中
        /// </summary>
        Maintain = 0,

        /// <summary>
        /// 开放中
        /// </summary>
        Open = 1,

        /// <summary>
        /// 繁忙
        /// </summary>
        Busy = 2,

        /// <summary>
        /// 新服
        /// </summary>
        New = 3,
    }
}