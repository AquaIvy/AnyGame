using AnyGame.LoginPlugin;
using AnyGame.View.Components;
using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnyGame.View.Forms.Login
{
    partial class FrmLogin : FrmBase
    {
        enum LoginState
        {
            /// <summary>
            /// 还没有weblogin时
            /// </summary>
            WaitWebLogin = 0,

            /// <summary>
            /// weblogin成功，正在选服，或者还没点login【连接服务器】
            /// </summary>
            WebLoginSuccess = 1,

            /// <summary>
            /// 已与Server建立socket连接，但还没有【登录服务器】
            /// </summary>
            ConnectServerSuccess = 2,

            /// <summary>
            /// 成功验证服务器，登录成功，玩家进入游戏。（但此时可能还没创建过角色）
            /// </summary>
            LoginServerSuccess = 3
        }


        public override FrmType Type { get { return FrmType.Background; } }

        private LoginState loginState = LoginState.WaitWebLogin;



        private string m_accountName;
        private string m_accountPwd;

        public FrmLogin() : base("FrmLogin")
        {
            InitForm();
            //WebLogin返回事件
            Game.LoginProxy.LoginResult += LoginProxy_LoginResult;
            //网络连接（在WebLogin之后）
            GameCenter.Controller.Net.NetStateConnect += Net_NetStateConnect;

            btnLogin.OnClick += BtnLogin_OnClick;
            btnLogoff.OnClick += BtnLogoff_OnClick;

            m_accountName = PlayerPrefs.GetString("account");
            m_accountPwd = PlayerPrefs.GetString("password");

            inputAccount.text = m_accountName;
            inputPwd.text = m_accountPwd;

            //Game.LoginProxy.AutoLogin(m_accountName);
        }

        private void BtnLogoff_OnClick(UIElement sender, EventArgs e)
        {
            loginState = LoginState.WaitWebLogin;

            node1.visible = true;
            if (node2 != null) node2.visible = false;
            if (node3 != null) node3.visible = false;

            btnLogoff.visible = false;
        }

        public void SetLoginSuccess()
        {
            loginState = LoginState.LoginServerSuccess;
        }

        private void BtnLogin_OnClick(UIElement sender, EventArgs e)
        {
            #region 去WebLogin进行登录验证

            if (loginState == LoginState.WaitWebLogin)
            {
                m_accountName = inputAccount.text;
                m_accountPwd = inputPwd.text;

                if (string.IsNullOrEmpty(m_accountName))
                {
                    Logs.Error("accountName == null");
                    return;
                }

                PlayerPrefs.SetString("account", m_accountName);
                PlayerPrefs.SetString("password", m_accountPwd);
                PlayerPrefs.Save();

                Game.LoginProxy.Login(m_accountName);
            }

            #endregion

            #region WebLoginSuccess

            else if (loginState == LoginState.WebLoginSuccess)
            {
                Logs.Info("准备连接服务器  {0}  {1}", curServer.Host, curServer.Port);

                GameCenter.Controller.Net.StartWorld();
                GameCenter.Controller.Net.ConnectServer(curServer.Host, curServer.Port);

                btnLogin.image.color = new Color(1, 1, 1, 0.5f);
                btnLogin.enable = false;
            }

            #endregion
        }


        private void LastLoginServer_OnClick(UIElement sender, System.EventArgs e)
        {
            InitNode3();
            node2.visible = false;
            node3.visible = true;
        }

        List<GameServer> allServers = null;
        GameServer curServer = null;
        /// <summary>
        /// WebLogin返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginProxy_LoginResult(object sender, LoginPlugin.LoginSuccessEventArgs e)
        {
            if (e.IsSucess)
            {
                loginState = LoginState.WebLoginSuccess;

                Logs.Info("webLogin success");

                node1.visible = false;
                btnLogoff.visible = true;

                allServers = e.Status.GameServers;
                var find = allServers.FirstOrDefault(o => o.Recommend);
                if (find == null)
                {
                    find = allServers.First();
                }

                curServer = find;
                InitNode2();
                node2.visible = true;
            }
            else
            {
                Logs.Error(e.ErrorMsg);
            }
        }



        private void SelectServer(GameServer data)
        {
            curServer = data;

            curSelectServer.SetData(data);
        }

        private void Net_NetStateConnect(object sender, NetStateConnectEventArgs e)
        {
            if (e.IsConnected)
            {
                loginState = LoginState.ConnectServerSuccess;
                //test vs git
                Logs.Info("服务器连接成功");

                GameCenter.Controller.Login.LoginServer(m_accountName, m_accountPwd, 0);
            }
            else
            {
                Logs.Error("服务器连接失败。");
            }
        }

        protected override void OnClosed()
        {
            Game.LoginProxy.LoginResult -= LoginProxy_LoginResult;
            GameCenter.Controller.Net.NetStateConnect -= Net_NetStateConnect;

            Game.FrmLogin = null;

            base.OnClosed();
        }

    }
}
