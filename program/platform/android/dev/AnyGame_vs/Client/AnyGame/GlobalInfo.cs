using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;


public class GlobalInfo
{
    #region 全局参数

    /// <summary>
    /// 版本号
    /// </summary>
    public static readonly Version Version = new Version("0.0.0.1");

    //Server
    //public static string SERVER_ROOT_PATH = @"http://192.168.2.84:88/";
    //public static string SERVER_UPDATELIST_PATH = @"http://192.168.2.84:88/updatelist.txt";

    public static string STREAM_ROOT_PATH;
    public static string STREAM_MATCHLIST_PATH;

    //Client
    public static string CLIENT_ROOT_PATH;
    public static string CLIENT_UPDATELIST_PATH;

    public static string RES_ASSEMBLY;
    public static string RES_SCENE;
    public static string RES_IMAGE;
    public static string RES_MATERIAL;
    public static string RES_SHADER;
    public static string RES_DATA;
    public static string RES_GAME_UI;
    public static string RES_GAME_WORLD;
    public static string RES_AUDIO;

    #endregion


    /// <summary>
    /// 每次跳转场景，先跳转到empty场景，然后根据配置加载游戏世界和UI
    /// 该值用于存储即将加载的配置信息（新场景的配置信息）
    /// </summary>
    public static string WAIT_CREATE_UI_WORLD = string.Empty;


    /// <summary>
    /// 游戏的设计width    720
    /// </summary>
    public const float width = 720f;

    /// <summary>
    /// 游戏的设计height   1280
    /// </summary>
    public const float height = 1280;

    /// <summary>
    /// 360
    /// </summary>
    public const float harfWidth = width / 2f;

    /// <summary>
    /// 640
    /// </summary>
    public const float harfHeight = height / 2f;

    /// <summary>
    /// 屏幕自适应 0是以宽为基准，1是已高为基准
    /// </summary>
    public static float CanvasMatch = 1.0f;
    /// <summary>
    /// 屏幕适应后，画布下第一个子孩子宽高
    /// </summary>
    public static Vector2 CanvasParentRect = new Vector2(width, height);


    public static void Init()
    {
        // 设置画质
        QualitySettings.antiAliasing = 0;             //抗锯齿
        QualitySettings.shadowCascades = 0;           //阴影叠加？
        QualitySettings.shadowDistance = 0;           //
        QualitySettings.shadowProjection = 0;         //阴影投射
        QualitySettings.vSyncCount = 0;               //

        // 屏幕不休眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //设置竖屏显示
        Screen.orientation = ScreenOrientation.Portrait;
        //限制帧速率
        Application.targetFrameRate = 60;


        //      16/9     1280/720       1.777777
        //      3/2      1280/853.33    1.5
        //      4/3      1280/960       1.333333

        //屏幕适应
        if (Screen.height * 1.0f / Screen.width >= height / width)
        {
            //更瘦的屏幕
            //其实理论上不会有更瘦的 16/9 应该就是最瘦的
            GlobalInfo.CanvasMatch = 1.0f;
            var tmp = height * Screen.width / Screen.height;
            GlobalInfo.CanvasParentRect = new Vector2(tmp, height);
        }
        else
        {
            //更胖的屏幕
            //最多支持到 4/3
            GlobalInfo.CanvasMatch = 1.0f;
            //var tmp = CanvasDevWidth * Screen.height / Screen.width;
            //GlobalInfo.CanvasParentRect = new Vector2(CanvasDevWidth, tmp);
            var tmp = height * Screen.width / Screen.height;
            GlobalInfo.CanvasParentRect = new Vector2(tmp, height);
        }
        Logs.Info("屏幕分辨率：height/width  {0}/{1}={2}", Screen.height, Screen.width, Screen.height * 1.0f / Screen.width);
        Logs.Info("逻辑分辨率：height/width  {0}/{1}", GlobalInfo.CanvasParentRect.y, GlobalInfo.CanvasParentRect.x);

        //资源目录
        if (Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXEditor)
        {
            GlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.dataPath + "/../../AnyGame_persistentDataPath/");
        }
        else if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer
            || Application.platform == RuntimePlatform.WP8Player)
        {
            GlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.persistentDataPath + "/AnyGame_persistentDataPath/");
        }

        //GlobalInfo.CLIENT_UPDATELIST_PATH = GlobalInfo.CLIENT_ROOT_PATH + @"matchlist.txt";

        GlobalInfo.RES_ASSEMBLY = GlobalInfo.CLIENT_ROOT_PATH + "res/assembly/";
        GlobalInfo.RES_SCENE = GlobalInfo.CLIENT_ROOT_PATH + "res/scene/";
        GlobalInfo.RES_IMAGE = GlobalInfo.CLIENT_ROOT_PATH + "res/image/";
        GlobalInfo.RES_SHADER = GlobalInfo.CLIENT_ROOT_PATH + "res/shader/";
        GlobalInfo.RES_MATERIAL = GlobalInfo.CLIENT_ROOT_PATH + "res/material/";
        GlobalInfo.RES_DATA = GlobalInfo.CLIENT_ROOT_PATH + "res/data/";
        GlobalInfo.RES_GAME_UI = GlobalInfo.CLIENT_ROOT_PATH + "res/gameUI/";
        GlobalInfo.RES_GAME_WORLD = GlobalInfo.CLIENT_ROOT_PATH + "res/gameWorld/";
        GlobalInfo.RES_AUDIO = GlobalInfo.CLIENT_ROOT_PATH + "res/audio/";

        //如果是PC   图片使用本地目录
        if (Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXEditor)
        {
            GlobalInfo.RES_IMAGE = Application.dataPath + "/../../Res_PC/res/image/";
        }
    }
}

