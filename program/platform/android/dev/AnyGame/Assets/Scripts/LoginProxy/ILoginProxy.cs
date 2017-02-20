using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.LoginProxy
{
    /// <summary>
    /// 登陆代理类
    /// </summary>
    interface ILoginProxy
    {
        /// <summary>
        /// 初始化sdk
        /// 这个初始化是在Loading界面出来之前调用
        /// 如果想在Loading之后调用初始化sdk，则可以再AutoLogin里调用
        /// </summary>
        void InitSdk();

        /// <summary>
        /// 是否自动登陆
        /// </summary>
        /// <returns></returns>
        bool AutoLogin(string account);

        /// <summary>
        /// 手动进行平台登陆楼层
        /// </summary>
        /// <param name="account">账户名，不一定是需要的</param>
        void Login(string account);

        /// <summary>
        /// 登陆返回事件（包含登陆成功和登陆失败
        /// </summary>
        event EventHandler<LoginSuccessEventArgs> LoginResult;

        /// <summary>
        /// 注销
        /// </summary>
        void Logoff();

        /// <summary>
        /// 用户触发退出游戏操作
        /// </summary>
        void Exit();

        /// <summary>
        /// 登陆代理的名字
        /// </summary>
        string Name { get; }
    }

    public class LoginSuccessEventArgs : EventArgs
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucess { get; set; }

        /// <summary>
        /// 错误消息（不一定存在）
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 登陆成功返回的一些状态信息
        /// </summary>
        internal LGameServerStatus Status { get; set; }
    }
}
