using AnyGame.LoginPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AnyGame.Client.Entity.Character;
using AnyGame.Content;

namespace AnyGame.LoginPlugin
{
    public class Fishluv : ILoginProxy
    {
        public PlatformTypes PlatformType
        {
            get { return PlatformTypes.Fishluv; }
        }

        public event EventHandler<LoginSuccessEventArgs> LoginResult;

        public bool AutoLogin(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return false;
            }

            //  之前有保存过账号，则直接登陆，并进入选服务器状态
            Login(account);

            return false;
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void Login(string account)
        {
            var phoneId = SystemInfo.deviceUniqueIdentifier;
            //公司
            var loginCheck = string.Format("http://192.168.2.84:200/AnyGame/Api/Fishluv.aspx?accessToken={0}&phonePlatformTypes={1}&cver={2}&phoneid={3}", account, "android", 4, phoneId);

            //家里
            if (System.Net.Dns.GetHostName() == "Aqua")
                loginCheck = string.Format("http://192.168.249.204:202/AnyGame/Api/Fishluv.aspx?accessToken={0}&phonePlatformTypes={1}&cver={2}&phoneid={3}", account, "android", 4, phoneId);


            DownloadTask progDownloadTask = null;
            Task.Invoke(k =>
            {
                if (progDownloadTask == null)
                {
                    Debug.LogWarning("服务器访问地址 " + loginCheck);
                    progDownloadTask = new DownloadTask(loginCheck);
                }

                if (progDownloadTask.IsDone)
                {
                    if (string.IsNullOrEmpty(progDownloadTask.ErrorMessage))
                    {
                        var result = Encoding.UTF8.GetString(progDownloadTask.Bytes, 0, progDownloadTask.Bytes.Length);
                        var serverStatus = GameServerStatus.FromXmlString(result);

                        if (string.IsNullOrEmpty(serverStatus.Error))
                        {
                            //  登陆服务器验证成功
                            LoginResult?.Invoke(this, new LoginSuccessEventArgs { IsSucess = true, Status = serverStatus });
                        }
                        else
                        {
                            LoginResult?.Invoke(this, new LoginSuccessEventArgs { ErrorMsg = serverStatus.Error, IsSucess = false });
                        }
                    }
                    else
                    {
                        Debug.LogError(progDownloadTask.ErrorMessage);
                        LoginResult?.Invoke(this, new LoginSuccessEventArgs { ErrorMsg = progDownloadTask.ErrorMessage, IsSucess = false });
                    }

                    return true;
                }

                return false;
            });

        }

        public void Logoff()
        {
            throw new NotImplementedException();
        }

        public void SuccessLogin()
        {
            throw new NotImplementedException();
        }
    }

}