using IvyOrm;
using System.Collections.Generic;
using AnyGame.Server.Entity.Character;

namespace LoginWeb.Entity
{
    /// <summary>
    /// 游戏分区   LoginWeb.Entity
    /// </summary>
    [Table("GameZone")]
    public class GameZone2 : GameZone
    {
        /// <summary>
        /// 服务器列表
        /// </summary>
        public List<ServerInfo> GameServers { get; set; }
    }
}