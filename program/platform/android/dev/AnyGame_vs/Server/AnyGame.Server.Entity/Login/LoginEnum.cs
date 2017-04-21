using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if Server 
using DogSE.Server.Core.Protocol;
namespace AnyGame.Server.Entity.Login
#else
using DogSE.Client.Core.Protocol;
namespace AnyGame.Client.Entity.Login
#endif
{
    /// <summary>
    /// 登陆服务器返回结果
    /// </summary>
    public enum LoginServerResult
    {
        /// <summary>
        /// 登陆成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 登陆失败
        /// </summary>
        Fail = 1,

        /// <summary>
        /// 密码或者账号错误
        /// </summary>
        PassOrAccountError = 2,
    }

    /// <summary>
    /// 创建玩家返回结果
    /// </summary>
    public enum CraetePlayerResult
    {
        /// <summary>
        /// 创建成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 创建失败
        /// </summary>
        Fail = 1,

        /// <summary>
        /// 名字已经存在
        /// </summary>
        NameExists = 2,
    }

    /// <summary>
    /// 掉线类型
    /// </summary>
    public enum OfflineType
    {
        /// <summary>
        /// 正常掉线
        /// </summary>
        [NetReturnDescription("客户端断开连接")]
        None = 0,

        /// <summary>
        /// 其他地方登陆
        /// </summary>
        [NetReturnDescription("您的账号在其它地方登陆")]
        LoginOther = 1,

        /// <summary>
        /// 服务器维护
        /// </summary>
        [NetReturnDescription("服务器进行维护中，请稍后重连")]
        ServerMaintain = 2,

        /// <summary>
        /// GM踢人
        /// </summary>
        [NetReturnDescription("您的账号涉嫌违规禁止登陆服务器")]
        GMKick = 3,

        /// <summary>
        /// 切换账号
        /// </summary>
        SwitchAccount = 4,
    }
}
