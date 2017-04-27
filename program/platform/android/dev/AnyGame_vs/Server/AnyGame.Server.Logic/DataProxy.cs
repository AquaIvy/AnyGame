using AnyGame.Server.Database;
using AnyGame.Server.Entity;
using AnyGame.Server.Entity.Character;
using DogSE.Library.Log;
using DogSE.Library.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnyGame.Server.Logic
{
    /// <summary>
    /// 数据访问代理
    /// </summary>
    public static class DataProxy
    {
        /// <summary>
        /// 获得一个玩家
        /// 先从内存缓存里获取，然后再从数据库里加载
        /// </summary>
        /// <param name="id">玩家id</param>
        /// <returns>可能会出现null， 需要注意判断返回值</returns>
        public static Player GetPlayer(int id)
        {
            var player = WorldEntityManager.Players.GetEntity(id);
            if (player == null)
            {
                player = DB.GameDB.LoadEntity<Player>(id);
                if (player != null)
                {
                    WorldEntityManager.Players.AddOrReplace(player);
                }
            }

            return player;
        }

        /// <summary>
        /// 获得玩家的所有数据
        /// 这个方法会加载玩家的所有逻辑模块数据
        /// 因此，此方法只适合于游戏登陆时调用
        /// 竞技场，好友等模块只需要获得玩家基本信息情况下
        /// 请调用 GetFightPlayer 方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Player GetPlayerAllData(int id)
        {
            var player = GetPlayer(id);
            if (player != null)
            {
                player.Bag = GetPlayerBag(id);
                player.Res = GetPlayerResource(id);

            }

            return player;
        }

        /// <summary>
        /// czx added
        /// 获得玩家的简单数据
        /// 这个方法只加载玩家简单数据
        /// 在服务器启动时候时，加载所有玩家的简单数据
        /// 此数据只用作显示，不用来作为判断依据
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static SimplePlayer GetPlayerSimpleData(int playerId)
        {
            var simplePlayer = WorldEntityManager.SimplePlayers.GetValue(playerId);
            if (simplePlayer == null)
            {
                simplePlayer = DB.GameDB.LoadEntity<SimplePlayer>(playerId);
                if (simplePlayer == null)
                {
                    simplePlayer = new SimplePlayer
                    {
                        Id = playerId,
                        LastLoginTime = OneServer.NowTime,
                    };
                }

                WorldEntityManager.SimplePlayers.SetValue(playerId, simplePlayer);
            }

            return simplePlayer;
        }

        /// <summary>
        /// 保存玩家的基本数据
        /// 数据包括：
        /// 玩家基本对象
        /// 英雄
        /// 背包
        /// 资源
        /// 游戏关卡
        /// 
        /// 剩下的数据，各个业务逻辑模块自己更新自己的数据
        /// 离线时调用的是 SyncPlayerAllData 里面会把所有业务逻辑模块都做一次保存同步
        /// 所以，加模块时需要注意把模块的保存数据方式加入是 SyncPlayerAllData 方法里 
        /// </summary>
        /// <param name="plaeyrId"></param>
        public static void SyncPlayerBaseData(int plaeyrId)
        {
            var player = WorldEntityManager.Players.GetEntity(plaeyrId);
            if (player == null)
            {
                Logs.Error("Not find player in entity chache {0}", plaeyrId);
                return;
            }

            DB.GameDB.SyncUpdateEntity(player);
            DB.GameDB.SyncUpdateEntity(player.Bag);
            DB.GameDB.SyncUpdateEntity(player.Res);
        }

        /// <summary>
        /// 全保存玩家的数据
        /// </summary>
        /// <param name="playerId"></param>
        public static void SyncPlayerAllData(int playerId)
        {
            var player = WorldEntityManager.Players.GetEntity(playerId);
            if (player == null)
            {
                Logs.Error("Not find player in entity chache {0}", playerId);
                return;
            }

            DB.GameDB.SyncUpdateEntity(player);
            DB.GameDB.SyncUpdateEntity(player.Bag);
            DB.GameDB.SyncUpdateEntity(player.Res);

            //if (player.PlayerGameRift != null)
            //    DBService.GameDBService.SyncUpdateEntity(player.PlayerGameRift);  // czx added;

            //var arena = EntityCache.Arena.GetEntity(playerId);
            //if (arena != null)
            //    DBService.GameDBService.SyncUpdateEntity(arena);


            //var artifact = EntityCache.Artifacts.GetEntity(playerId);
            //if (artifact != null)
            //    DBService.GameDBService.SyncUpdateEntity(artifact);

            //var temple = EntityCache.Temple.GetEntity(playerId);
            //if (temple != null)
            //    DBService.GameDBService.SyncUpdateEntity(temple);

            //var miningMap = EntityCache.MiningMap.GetEntity(playerId);
            //if (miningMap != null)
            //    DBService.GameDBService.SyncUpdateEntity(miningMap);

            //var active = EntityCache.PlayerActives.GetEntity(playerId);
            //if (active != null)
            //    DBService.GameDBService.SyncUpdateEntity(active);

            //var playerReward = EntityCache.PlayerRewards.GetEntity(playerId);
            //if (playerReward != null)
            //    DBService.GameDBService.SyncUpdateEntity(playerReward);

            //var achievent = EntityCache.PlayerAchievements.GetEntity(playerId);
            //if (achievent != null)
            //    DBService.GameDBService.SyncUpdateEntity(achievent);

            //var shop = EntityCache.PlayerShops.GetEntity(playerId);
            //if (shop != null)
            //    DBService.GameDBService.SyncUpdateEntity(shop);

            //var tradetroop = EntityCache.TradeTroops.GetEntity(playerId);
            //if (tradetroop != null)
            //    DBService.GameDBService.SyncUpdateEntity(tradetroop);
        }

        /// <summary>
        /// 获得一个玩家的资源数据
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static Res GetPlayerResource(int playerId)
        {
            var resource = WorldEntityManager.Res.GetEntity(playerId);
            if (resource == null)
            {
                resource = DB.GameDB.LoadEntity<Res>(playerId);
                if (resource != null)
                {
                    WorldEntityManager.Res.AddOrReplace(resource);
                }
            }

            return resource;
        }

        /// <summary>
        /// 获得一个玩家的背包数据
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public static Bag GetPlayerBag(int playerId)
        {
            var bag = WorldEntityManager.Bag.GetEntity(playerId);
            if (bag == null)
            {
                bag = DB.GameDB.LoadEntity<Bag>(playerId);
                if (bag != null)
                {
                    WorldEntityManager.Bag.AddOrReplace(bag);
                }
            }

            return bag;
        }
    }
}
