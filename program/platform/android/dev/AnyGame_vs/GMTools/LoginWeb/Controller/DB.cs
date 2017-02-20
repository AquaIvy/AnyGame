using DogSE.Server.Database.MongoDB;
using DogSE.Server.Database.MySQL;
using LoginWeb.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using TradeAge.Server.Database.Account;

namespace LoginWeb.Controller
{
    /// <summary>
    /// Mysql的数据库访问
    /// </summary>
    public class DB : MySqlService
    {
        public DB(string connectStr) :
            base(connectStr)
        {

        }

        static DB s_gamemasterdb;

        /// <summary>
        /// GM库的访问器
        /// </summary>
        public static DB GameMasterDB
        {
            get
            {
                if (s_gamemasterdb == null)
                    s_gamemasterdb = new DB(ConfigurationManager.ConnectionStrings["gamemaster"].ConnectionString);

                return s_gamemasterdb;
            }
        }

        static AccountService s_accountdb;

        /// <summary>
        /// 账号库访问器
        /// </summary>
        public static AccountService AccountDB
        {
            get
            {
                if (s_accountdb == null)
                    s_accountdb = new AccountService(ConfigurationManager.ConnectionStrings["accountdb"].ConnectionString);

                return s_accountdb;
            }
        }


        private static readonly Dictionary<int, DB> AnalysisMap = new Dictionary<int, DB>();


        /// <summary>
        /// 获得某个游戏分区对应的分析库
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static DB GetAnalysisDB(int zoneId)
        {
            DB db;

            if (AnalysisMap.TryGetValue(zoneId, out db))
                return db;

            var conString = ConfigurationManager.ConnectionStrings["analysisdb"].ConnectionString;

            var userRegx = new Regex(@"user=(\w*)");
            var user = userRegx.Match(conString).Value.Replace("user=", "");


            var pwdRegx = new Regex(@"pwd=(\w*)");
            var pwd = pwdRegx.Match(conString).Value.Replace("pwd=", "");


            var server = GameMasterDB.LoadEntity<ServerInfo>(zoneId);

            db = new DB(string.Format("host={0};database={1};user={2};pwd={3}",
                server.AnalysisDatabaseHost, server.AnalysisDatabaseName, user, pwd));

            AnalysisMap[zoneId] = db;

            return db;
        }


        private static readonly Dictionary<int, MongoDBService> GamedbMap = new Dictionary<int, MongoDBService>();

        /// <summary>
        /// 获得某个游戏分区对应的分析库
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static MongoDBService GetGameDB(int zoneId)
        {
            MongoDBService db;

            if (GamedbMap.TryGetValue(zoneId, out db))
                return db;

            var server = GameMasterDB.LoadEntity<ServerInfo>(zoneId);

            db = new MongoDBService(server.DatabaseHost, server.DatabaseName);

            GamedbMap[zoneId] = db;

            return db;
        }
    }
}