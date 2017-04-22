using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DogSE.Client.Core;
using DogSE.Client.Core.Net;
using DogSE.Client.Core.Task;
using AnyGame.Client.Entity.Login;
using AnyGame.Client.Entity;
using AnyGame.Client.Entity.Character;

namespace AnyGame.Client.Controller.Login
{

    /// <summary>
    /// Login
    /// </summary>
    public partial class LoginController : BaseLoginController
    {
        private readonly GameController controller;

        public LoginController(GameController gc, NetController nc)
            : this(nc)
        {
            controller = gc;
        }

        private EntityModel Model
        {
            get { return controller.Model; }
        }

        internal override void OnLoginServerResult(LoginServerResult result, bool isCreatedPlayer)
        {
            if (result == LoginServerResult.Success)
            {
                if (isCreatedPlayer)
                    controller.Game.SyncTime();
            }

            LoginServerResultEvent?.Invoke(this, new LoginServerResultEventArgs
            {
                Result = result,
                IsCreatedPlayer = isCreatedPlayer,
            });
        }

        internal override void OnCreatePlayerResult(CraetePlayerResult result)
        {
            if (result == CraetePlayerResult.Success)
                controller.Game.SyncTime();

            CreatePlayerResultEvent?.Invoke(this, new CreatePlayerResultEventArgs
            {
                Result = result,
            });
        }

        internal override void OnSyncInitDataFinish()
        {
            SyncInitDataFinishEvent?.Invoke(this, new EventArgs());
        }

        internal override void OnKickOfServer(OfflineType type)
        {
            KickOfServerEvent?.Invoke(this, new KickOfServerEventArgs
            {
                Type = type,
            });
        }

        internal override void OnSyncPlayerBaseInfo(int playerId, int gameZoonId, bool isSupperMan, int platformType)
        {
            Model.Player.Id = playerId;
            Model.Player.AccountId = playerId;
            Model.Player.GameZoneId = gameZoonId;
            Model.Player.IsSuperMan = isSupperMan;
            Model.Player.PlatformType = (PlatformTypes)platformType;
        }

        /// <summary>
        /// 登陆返回
        /// </summary>
        public event EventHandler<LoginServerResultEventArgs> LoginServerResultEvent;

        /// <summary>
        /// 创建玩家返回结果
        /// </summary>
        public event EventHandler<CreatePlayerResultEventArgs> CreatePlayerResultEvent;

        /// <summary>
        /// 登陆游戏时的基本数据已同步完成
        /// 客户端可以开始进入游戏了
        /// </summary>
        public event EventHandler<EventArgs> SyncInitDataFinishEvent;

        /// <summary>
        /// 通知玩家被T下线
        /// </summary>
        public event EventHandler<KickOfServerEventArgs> KickOfServerEvent;
    }

    /// <summary>
    /// 登陆返回 【参数】
    /// </summary>
    public class LoginServerResultEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginServerResult Result { get; internal set; }

        /// <summary>
        /// 玩家是否创建过角色，如果没有创建过，则客户端需要调用创建角色代码
        /// </summary>
        public bool IsCreatedPlayer { get; internal set; }
    }

    /// <summary>
    /// 创建玩家返回结果 【参数】
    /// </summary>
    public class CreatePlayerResultEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public CraetePlayerResult Result { get; internal set; }
    }


    /// <summary>
    /// 通知玩家被T下线 【参数】
    /// </summary>
    public class KickOfServerEventArgs : EventArgs
    {
        /// <summary>
        /// 掉线的类型
        /// </summary>
        public OfflineType Type { get; internal set; }
    }

}
