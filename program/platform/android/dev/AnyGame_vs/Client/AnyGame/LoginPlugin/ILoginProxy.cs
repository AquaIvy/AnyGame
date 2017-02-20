using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeAge.Client.Entity.Common;

namespace AnyGame.LoginPlugin
{
    /// <summary>
    /// 登陆代理类
    /// </summary>
    interface ILoginProxy
    {/// <summary>
     /// 当前代理类对应的平台类型
     /// </summary>
        PlatformTypes PlatformType { get; }

        /// <summary>
        /// 是否自动登陆
        /// </summary>
        /// <returns></returns>
        bool AutoLogin(string account);

        /// <summary>
        /// 手动进行平台登陆
        /// </summary>
        /// <param name="account">账户名，不一定是需要的</param>
        void Login(string account);

        /// <summary>
        /// 登陆返回事件（包含登陆成功和登陆失败）
        /// </summary>
        event EventHandler<LoginSuccessEventArgs> LoginResult;

        /// <summary>
        /// 注销账号
        /// </summary>
        void Logoff();


        /// <summary>
        /// 用户触发退出游戏操作
        /// </summary>
        void Exit();


        /// <summary>
        /// 查询余额
        /// </summary>
        //int QueryBalance();

        /// <summary>
        /// 充值
        /// </summary>
        //void Recharge(float price);

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="info"></param>
        /// <param name="price"></param>
        /// <param name="rechargeTemplate"></param>
        //void Pay(string info, RechargeTemplate rechargeTemplate, int price);

        /// <summary>
        /// 字符成功事件， 
        /// OrderId = 0： 充值失败 ;  
        /// OrderId != 0  充值成功
        /// </summary>
        //event EventHandler<PaySuccessEventArgs> PaySuccess;

        /// <summary>
        /// 已经完成登陆
        /// 这个时候各个登陆代理可以根据实际需要向对应服务器发出一些登陆消息
        /// </summary>
        void SuccessLogin();

        /// <summary>
        /// 获得回调脚本对象
        /// </summary>
        /// <returns></returns>
        //Type GetSDKCallBackType();
    }

    /// <summary>
    /// 登陆成功事件
    /// </summary>
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
        internal GameServerStatus Status { get; set; }
    }

    /// <summary>
    /// 字符串成功事件
    /// </summary>
    public class PaySuccessEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 字符金额
        /// </summary>
        public double CurrencyAmount { get; set; }

        /// <summary>
        /// 货币类型人民币应该填写 CNY 美元填写 USD
        /// </summary>
        public string CurrencyType { get; set; }

        /// <summary>
        /// 字符途径 支付途径：如"xxx支付SDK"，"支付宝"等
        /// </summary>
        public string PaymentType { get; set; }
    }
}
