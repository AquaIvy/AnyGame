using AnyGame;
using AnyGame.Content.Manager;
using AnyGame.LoginPlugin;
using AnyGame.View;
using AnyGame.Utils.TweenLite;
using AnyGame.View.Components;
using AnyGame.View.Forms;
using AnyGame.View.Forms.Login;
using AnyGame.View.Forms.Main;
using DogSE.Client.Core;
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnyGame.View.Forms.Bags;

public class Game : MonoBehaviour
{

    public static Game instance { get; private set; }
    public static Workflow workflow { get; private set; }

    internal static ILoginProxy LoginProxy { get; set; }

    public static InputMgr inputMgr = null;
    public static Stopwatch stopwatch = new Stopwatch();

    private List<FrmBase> m_formList = new List<FrmBase>();
    private string loadedLevelName = string.Empty;


    #region 窗体注册
    internal FrmLogin FrmLogin { get; set; }
    internal FrmMain FrmMain { get; set; }
    internal FrmBag FrmBag { get; set; }

    #endregion

    #region 初始化窗体

    private void InitFroms()
    {
        FrmLogin = null;
        FrmMain = null;
        FrmBag = null;
    }

    #endregion

    #region 窗体加入链表

    public void AddForms<T>(T frm) where T : FrmBase
    {
        m_formList.Add(frm);
    }

    public void SetFormNull<T>(T frm) where T : FrmBase
    {
        switch (frm.Type)
        {
            case FrmType.Login: FrmLogin = null; break;
            //case FrmType.CreateCharacter: FrmBag = null; break;
            case FrmType.Main: FrmMain = null; break;
            case FrmType.Bag: FrmBag = null; break;
        }

        if (m_formList.Contains(frm))
            m_formList.Remove(frm);
    }

    #endregion



    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);

        workflow = new Workflow();

        Logs.AddAppender(new UnityAppender());

        InitBasicSetting();

        GlobalInfo.Init();
        Lang.Init();
        ShaderMgr.Init();
        FontMgr.Init();
        AudioMgr.Init();
        TextureMgr.Init();
        //MaterialMgr.Init();

        workflow.Init();

        InitFroms();
        //AddForms();

        Logs.Warn("好啦   都运行完啦");

        EnterMainScene();
    }

    /// <summary>
    /// 由Loader反射时调用，设置平台
    /// </summary>
    /// <param name="platform"></param>
    public void SetPlatformProxy(string platform)
    {
        UnityEngine.Debug.Log("SetPlatformProxy   " + platform);

        //#if UNITY_ANDROID
        if (platform == "fishluv")
            LoginProxy = new Fishluv();
        if (platform == "aquaivy")
            LoginProxy = new AquaIvy();

        //#endif
    }


    /// <summary>
    /// 进入主场景
    /// </summary>
    public void EnterMainScene()
    {
        AssetBundle.LoadFromFile(GlobalInfo.RES_SCENE + "MainScene.assetbundle");
        SceneManager.LoadScene("MainScene");
    }
    UINode mynode = null;
    void OnLevelWasLoaded(int level)
    {
        string scenename = SceneManager.GetActiveScene().name;
        Logs.Info("OnLevelWasLoaded  " + scenename);

        UIRoot.Init();

        var login = new FrmLogin();
        FrmLogin = login;
        UIRoot.Show(login);

        //var canvas = GameObject.Find("Canvas");

        ////text = new UIElement("123", 10, 10, 20, Color.red);

        //mynode = new UINode();
        //mynode.rt.SetParent(canvas.transform);
        //mynode.rt.anchoredPosition3D = new Vector3(900, -500, 0);
        //mynode.rt.localRotation = Quaternion.Euler(0, 0, 0);
        //mynode.rt.localScale = Vector3.one;
        ////mynode.Pivot = UIUtils.UpperLeft;

        //var a = new UIText("testtt", 5, 3, 30, Color.green);
        //mynode.AddChild(a);

        //var img = new UIImage("aztec/button/button_back.png", 20, 10);
        //mynode.AddChild(img);

    }


    void InitBasicSetting()
    {
        //删除加载器
        var loader = GameObject.Find("/Loader");
        if (loader != null) { GameObject.Destroy(loader); }

        loadedLevelName = SceneManager.GetActiveScene().name;


        gameObject.AddComponent<RuntimeProfiler>();
        inputMgr = gameObject.AddComponent<InputMgr>();


    }


    int elapseTime;
    void Update()
    {
        elapseTime = (int)(Time.smoothDeltaTime * 1000);

        TweenLite.Update(elapseTime);

        NetController.TaskManager.RunAllTask();

        Task.Update(elapseTime);

        Workflow.Update();

        for (int i = 0; i < m_formList.Count; i++)
        {
            if (m_formList[i] != null)
            {
                m_formList[i].RunUpdate(elapseTime);
            }
        }
        //if (FrmMain != null)
        //{
        //    FrmMain.RunUpdate(elapseTime);
        //}


        #region Quit
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        #endregion

        TextureMgr.Update();
    }


    /// <summary>
    /// 系统退出后清理数据
    /// </summary>
    private void OnDisable()
    {
        Logs.Info("OnDisable");

        Task.ReleaseAll();
        TweenLite.ReleaseAll();

        Resources.UnloadUnusedAssets();

        GameCenter.Controller.Net.StopWorld();
        NetController.CloseThread();
    }




    #region Debug时使用

    /// <summary>
    /// 测试时使用
    /// </summary>
    /// <param name="milliseconds"></param>
    public void Sleep(int milliseconds)
    {
        //System.Threading.Thread.Sleep(milliseconds);
    }

    public static void watchStart()
    {
        stopwatch.Reset();
        stopwatch.Start();
    }

    public static string watchStop(string info = "")
    {
        stopwatch.Stop();
        string rtn = info + "  " + new TimeSpan(stopwatch.ElapsedMilliseconds).ToString();
        Logs.Info(rtn);
        return rtn;
    }



    #endregion
}

