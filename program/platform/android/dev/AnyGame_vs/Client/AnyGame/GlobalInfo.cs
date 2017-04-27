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

    //常用资源路径
    public static string RES_ASSEMBLY;
    public static string RES_SCENE;
    public static string RES_IMAGE;
    public static string RES_MATERIAL;
    public static string RES_SHADER;
    public static string RES_DATA;
    public static string RES_AUDIO;

    #endregion


    /// <summary>
    /// 游戏的设计width    1280
    /// </summary>
    public const float width = 1280f;

    /// <summary>
    /// 游戏的设计height   720
    /// </summary>
    public const float height = 720f;

    /// <summary>
    /// 360
    /// </summary>
    public const float harfWidth = width / 2f;

    /// <summary>
    /// 640
    /// </summary>
    public const float harfHeight = height / 2f;


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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //限制帧速率
        Application.targetFrameRate = 60;


        //      16/9     1280/720       1.777777
        //      3/2      1280/853.33    1.5
        //      4/3      1280/960       1.333333
        /*
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
            var tmp = height * Screen.width / Screen.height;
            GlobalInfo.CanvasParentRect = new Vector2(tmp, height);
        }
        Logs.Info("屏幕分辨率：height/width  {0}/{1}={2}", Screen.height, Screen.width, Screen.height * 1.0f / Screen.width);
        Logs.Info("逻辑分辨率：height/width  {0}/{1}", GlobalInfo.CanvasParentRect.y, GlobalInfo.CanvasParentRect.x);
        */

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

        //常用资源路径
        GlobalInfo.RES_ASSEMBLY = GlobalInfo.CLIENT_ROOT_PATH + "res/assembly/";
        GlobalInfo.RES_SCENE = GlobalInfo.CLIENT_ROOT_PATH + "res/scenes/";
        GlobalInfo.RES_IMAGE = GlobalInfo.CLIENT_ROOT_PATH + "res/images/";
        GlobalInfo.RES_SHADER = GlobalInfo.CLIENT_ROOT_PATH + "res/shaders/";
        GlobalInfo.RES_MATERIAL = GlobalInfo.CLIENT_ROOT_PATH + "res/materials/";
        GlobalInfo.RES_DATA = GlobalInfo.CLIENT_ROOT_PATH + "res/data/";
        GlobalInfo.RES_AUDIO = GlobalInfo.CLIENT_ROOT_PATH + "res/audios/";

        //如果是PC   图片使用本地目录
        if (Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXEditor)
        {
            GlobalInfo.RES_IMAGE = Application.dataPath + "/../../Res_PC/res/images/";
        }
    }
}

