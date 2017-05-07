using Assets.Script.LoginProxy;
using Assets.Scripts;
using Assets.Scripts.Utils;
using Assets.Scripts.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class Loader : MonoBehaviour
{
    /// <summary>
    /// 登陆代理
    /// </summary>
    internal static ILoginProxy LoginProxy { get; set; }

    internal FrmUpdate FrmUpdate { get; set; }
    internal FrmPopup FrmPopup { get; set; }


    void Start()
    {
        if (LoginProxy == null)
        {
            Debug.LogError("未挂载平台代理类");
            return;
        }

        LoaderCenter.SetLoader(this);

        Debug.LogFormat("平台: {0}    版本: {1}", Application.platform, Application.version);
        DontDestroyOnLoad(this);

        LGlobalInfo.Init();

        FrmUpdate = new FrmUpdate();
        FrmUpdate.Show();
    }


    /// <summary>
    /// 所有资源下载均已结束，开始准备真正的进入游戏
    /// </summary>
    public void StartGame()
    {
        ReflectionAssembly();
    }


    private int elapseTime;
    void Update()
    {
        elapseTime = (int)(Time.smoothDeltaTime * 1000);

        Task.Update(elapseTime);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// 反射程序集
    /// </summary>
    private void ReflectionAssembly()
    {
#if UNITY_ANDROID || UNITY_EDITOR_WIN

        //加载程序集
        var assembly = LoadAssemblies();
        var Game = assembly.GetType("Game");

        //创建主游戏物体
        GameObject go = new GameObject("Game");

        //挂载代码
        Component comp = go.AddComponent(Game);

        comp.GetType().GetMethod("SetPlatformProxy").Invoke(comp, new object[] { LoginProxy.Name });

        Destroy(this);

#elif UNITY_IPHONE

            //创建主游戏物体
            GameObject go = new GameObject("Game");

            //挂载代码
            go.AddComponent<DontDestroyThisGameObject>();
            go.AddComponent<Game>();

            Destroy(this);
#endif


    }

    /// <summary>
    /// 加载程序集，并返回叫做AnyGame.dll的那个
    /// </summary>
    /// <returns></returns>
    public static Assembly LoadAssemblies()
    {
        //下面的顺序不能随意改动
        var assemblyNames = new string[] {
            "DogSE.Library.net35.dll",
            "DogSE.Client.Core.dll",
            "AnyGame.Client.Entity.dll",
            "AnyGame.Client.Template.dll",
            "AnyGame.Client.Controller.dll",
            "AnyGame.dll",
        };

        List<Assembly> assembly = new List<Assembly>();
        for (int i = 0; i < assemblyNames.Length; i++)
        {
            var ass = LoadAssembly(LGlobalInfo.RES_ASSEMBLY + assemblyNames[i]);
            assembly.Add(ass);

            if (ass == null)
            {
                Debug.LogErrorFormat("LoadAssemblies()    {0} == null", assemblyNames[i]);
            }
        }

        return assembly[assemblyNames.Length - 1];
    }

    /// <summary>
    /// 加载程序集
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Assembly LoadAssembly(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        var fs = new FileStream(path, FileMode.Open);
        var bytes = new byte[fs.Length];
        fs.Read(bytes, 0, bytes.Length);
        fs.Close();
        fs.Dispose();
        Assembly assembly = Assembly.Load(bytes);

        return assembly;
    }




}
