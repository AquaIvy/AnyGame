namespace AnyGame.Server.Entity.NetCode
{
    /// <summary>
    /// 游戏的消息码
    /// </summary>
    public enum OpCode : ushort
    {
        #region 0 - 1000 游戏系统保留消息码

        /// <summary>
        /// 心跳包
        /// </summary>
        Heart = 1,

        /// <summary>
        /// 服务器时间
        /// </summary>
        ServerTime = 2,

        /// <summary>
        /// 客户端请求服务器执行一条GM指令
        /// 只针对自己的
        /// </summary>
        RunGMCommand = 2,

        /// <summary>
        /// 请求同步服务端时间
        /// </summary>
        GetSystemTime = 3,

        /// <summary>
        /// 请求同步服务端时间结果
        /// </summary>
        GetSystemTimeResult = 4,

        /// <summary>
        /// 客户端日志
        /// </summary>
        ClientLog = 7,

        /// <summary>
        /// 手机信息
        /// </summary>
        PhoneInfo = 8,

        /// <summary>
        /// 客户端异常
        /// </summary>
        ClientException = 9,

        /// <summary>
        /// 公告
        /// </summary>
        Notice = 10,

        /// <summary>
        /// 登陆前的公告
        /// </summary>
        ServerStatus = 11,

        /// <summary>
        /// 客户端的暂停状态
        /// </summary>
        ClinetPauseStatus = 12,

        /// <summary>
        /// 心跳包
        /// </summary>
        Heart2 = 13,

        #endregion

        #region 1000  - 1099 登陆相关消息码

        /// <summary>
        /// 客户端登陆服务器
        /// </summary>
        LoginServer = 1000,

        /// <summary>
        /// 登陆服务器返回结果
        /// </summary>
        LoginServerResult = 1001,

        /// <summary>
        /// 创建玩家
        /// </summary>
        CreatePlayer = 1002,

        /// <summary>
        /// 创建玩家结果
        /// </summary>
        CreatePlayerResult = 1003,

        /// <summary>
        /// 被服务器踢下线
        /// </summary>
        KickOfServer = 1004,

        /// <summary>
        /// 同步离线奖励信息
        /// </summary>
        SyncOfflineReward = 1005,

        /// <summary>
        /// 数据同步完成
        /// </summary>
        SyncInitDataFinish = 1006,

        /// <summary>
        /// 判断名字是否存在
        /// </summary>
        NameExists = 1009,

        /// <summary>
        /// 判断名字是否存在的返回
        /// </summary>
        NameExistsResult = 1010,

        #endregion

        #region 1100  - 1199 移动相关的代码

        /// <summary>
        /// 玩家发起的移动请求
        /// </summary>
        OnMove = 1100,

        /// <summary>
        /// 玩家进入场景时返回的基本信息
        /// </summary>
        EnterSceneInfo = 1101,


        /// <summary>
        /// 场景里有其他精灵（玩家）进入
        /// </summary>
        SpriteEnter = 1102,

        /// <summary>
        /// 场景里有其他精灵（玩家）进行移动
        /// </summary>
        SpriteMove = 1103,


        /// <summary>
        /// 场景里有其他精灵（玩家）进行离开
        /// </summary>
        SpriteLeave = 1104,

        #endregion

        #region 1200  - 1299 Bag

        /// <summary>
        /// 使用背包物品
        /// </summary>
        UseItem = 1200,

        /// <summary>
        /// 使用结果返回
        /// </summary>
        UseItemResult = 1201,

        /// <summary>
        /// 同步物品
        /// </summary>
        SyncItem = 1202,

        /// <summary>
        /// 同步背包
        /// </summary>
        SyncBag = 1203,

        /// <summary>
        /// 同步所有的资源
        /// </summary>
        SyncAllResouce = 1204,

        /// <summary>
        /// 同步资源
        /// </summary>
        SyncResouce = 1205,

        /// <summary>
        /// 加物品
        /// </summary>
        AddItem = 1206,

        /// <summary>
        /// 加物品的结果
        /// </summary>
        AddItemResult = 1207,

        /// <summary>
        /// 加资源
        /// </summary>
        AddResource = 1208,

        /// <summary>
        /// 加资源的结果
        /// </summary>
        AddResourceResult = 1209,

        /// <summary>
        /// 升级背包
        /// </summary>
        UpgradeBag = 1241,

        /// <summary>
        /// 开启背包格子结果
        /// </summary>
        UpgradeBagResult = 1242,


        #endregion
    }
}
