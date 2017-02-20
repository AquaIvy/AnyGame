using DogSE.Server.Database;
using DogSE.Server.Database.MySQL;
using IvyOrm;
using MySql.Data.MySqlClient;
using System.Data;

namespace TradeAge.Server.Database.Account
{
    /// <summary>
    /// 账号相关的Service
    /// </summary>
    public class AccountService : MySqlService
    {
        public AccountService(string connectStr) : base(connectStr)
        {
        }

        /// <summary>
        /// 根据账号名来获得账号对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public T GetAccountByName<T>(string accountName) where T : class, new()
        {
            var profile = DBEntityProfile<AccountService>.Instance;
            profile.Query.Watch.Restart();
            var proMySql = MySQL.Instance;

            try
            {
                profile.Query.TotalCount++;
                proMySql.Query.TotalCount++;

                var con = ConPool.GetConnection();
                var sql = string.Format("select * from {0} where `Name` = '{1}'", typeof(T).Name, accountName);
                var ret = con.RecordSingleOrDefault<T>(sql);
                ConPool.ReleaseContent(con);
                return ret;
            }
            catch
            {
                profile.Query.ErrorCount++;
                proMySql.Query.ErrorCount++;

                throw;
            }
            finally
            {
                profile.Query.Watch.Stop();
                profile.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
                proMySql.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
            }
        }

        /// <summary>
        /// 根据账号名来获得账号对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accountId">账号id</param>
        /// <param name="zoneId">游戏分区id</param>
        /// <returns></returns>
        public T GetAccountByAccountId<T>(int accountId, int zoneId) where T : class, new()
        {
            var profile = DBEntityProfile<AccountService>.Instance;
            profile.Query.Watch.Restart();
            var proMySql = MySQL.Instance;

            try
            {
                profile.Query.TotalCount++;
                proMySql.Query.TotalCount++;

                var con = ConPool.GetConnection();
                var sql = string.Format("select * from {0} where `PlatformAccountId` = {1} and `ZoneId`={2}", typeof(T).Name, accountId, zoneId);
                var ret = con.RecordSingleOrDefault<T>(sql);
                ConPool.ReleaseContent(con);
                return ret;
            }
            catch
            {
                profile.Query.ErrorCount++;
                proMySql.Query.ErrorCount++;

                throw;
            }
            finally
            {
                profile.Query.Watch.Stop();
                profile.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
                proMySql.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
            }
        }

        /// <summary>
        /// 根据账号名来获得账号对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accountId">账号id</param>
        /// <returns></returns>
        public T[] QueryAccountByAccountId<T>(int accountId) where T : class, new()
        {
            var profile = DBEntityProfile<AccountService>.Instance;
            profile.Query.Watch.Restart();
            var proMySql = MySQL.Instance;

            try
            {
                profile.Query.TotalCount++;
                proMySql.Query.TotalCount++;

                var con = ConPool.GetConnection();
                var sql = string.Format("select * from {0} where `PlatformAccountId` = {1}", typeof(T).Name, accountId);
                var ret = con.RecordQuery<T>(sql);
                ConPool.ReleaseContent(con);
                return ret;
            }
            catch
            {
                profile.Query.ErrorCount++;
                proMySql.Query.ErrorCount++;

                throw;
            }
            finally
            {
                profile.Query.Watch.Stop();
                profile.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
                proMySql.Query.TotalTime += profile.Load.Watch.ElapsedTicks;
            }
        }
    }
}
