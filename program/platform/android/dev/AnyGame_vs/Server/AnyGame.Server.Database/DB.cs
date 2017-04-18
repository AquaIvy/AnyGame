using DogSE.Server.Database.MongoDB;
using DogSE.Server.Database.MySQL;
using AnyGame.Server.Database.Account;

namespace AnyGame.Server.Database
{
    /// <summary>
    /// 游戏的数据库访问器
    /// </summary>
    public class DB
    {
        public static AccountService AccountDB { get; private set; }

        public static MongoDBService GameDB { get; private set; }

        public static MySqlService AnalysisDB { get; private set; }

        /// <summary>
        /// 初始化,理论上需要在游戏服务端配置文件加载完成后才能调用
        /// </summary>
        public static void Init()
        {
            AccountDB = new AccountService(DBConfig.AccountMySqlConnectString);
            //GameDB = new MongoDBService("127.0.0.1", "db_1");
            GameDB = new MongoDBService(MongoDBConfig.Host, MongoDBConfig.Database);
        }
    }
}
