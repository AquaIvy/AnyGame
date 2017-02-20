using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.LoginProxy;
using UnityEngine;
using Assets.Scripts.Utils;

/// <summary>
/// 
/// </summary>
public class Fishluv : MonoBehaviour, ILoginProxy
{
    /// <summary>
    /// 棱镜登陆代理
    /// </summary>
    public Fishluv()
    {
        Loader.LoginProxy = this;
    }

    public string Name { get { return "fishluv"; } }

    public void InitSdk()
    {
    }

    public bool AutoLogin(string account)
    {
        if (string.IsNullOrEmpty(account))
        {
            return false;
        }

        //  之前有保存过账号，则直接登陆，并进入选服务器状态
        webLogin(account);

        return false;
    }

    public void Login(string account)
    {
        webLogin(account);
    }

    /// <summary>
    /// 注销
    /// </summary>
    public void Logoff()
    {
        PlayerPrefs.SetString("account", "");
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 登陆返回
    /// </summary>
    public event EventHandler<LoginSuccessEventArgs> LoginResult;

    private void webLogin(string account)
    {
        var phoneId = SystemInfo.deviceUniqueIdentifier;
        var loginCheck = string.Format("http://192.168.249.204:200/Api/Fishluv.aspx?accessToken={0}&phonePlatformTypes={1}&cver={2}&phoneid={3}", account, "android", 4, phoneId);

        DownloadTask loginTask = null;
        Task.Invoke(k =>
        {
            if (loginTask == null)
            {
                loginTask = new DownloadTask(loginCheck);
            }

            if (loginTask.IsDone)
            {
                if (string.IsNullOrEmpty(loginTask.ErrorMessage))
                {
                    LGameServerStatus serverStatus = null;
                    try
                    {
                        var result = Encoding.UTF8.GetString(loginTask.Bytes);
                        serverStatus = LGameServerStatus.FromXmlString(result);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.ToString());
                    }

                    if (serverStatus == null)
                    {
                        if (LoginResult != null)
                            LoginResult(this, new LoginSuccessEventArgs { ErrorMsg = "登陆失败", IsSucess = false });

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(serverStatus.Error))
                        {
                            //  登陆服务器验证成功
                            if (LoginResult != null)
                                LoginResult(this, new LoginSuccessEventArgs { IsSucess = true, Status = serverStatus });
                        }
                        else
                        {
                            if (LoginResult != null)
                                LoginResult(this, new LoginSuccessEventArgs { ErrorMsg = serverStatus.Error, IsSucess = false });
                        }
                    }
                }
                else
                {
                    Debug.LogError(loginTask.ErrorMessage);
                    if (LoginResult != null)
                        LoginResult(this, new LoginSuccessEventArgs { ErrorMsg = loginTask.ErrorMessage, IsSucess = false });
                }

                return true;
            }

            return false;
        });
    }


    public void Exit()
    {
        Application.Quit();
    }
}

