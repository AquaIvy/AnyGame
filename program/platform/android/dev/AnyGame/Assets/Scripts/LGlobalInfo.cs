using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{

    public class LGlobalInfo
    {
        /// <summary>
        /// 引擎版本号
        /// </summary>
        public static readonly Version EngineVersion = new Version("0.0.0.1");

        #region 配置

        /// <summary>
        /// 启动时是否【清空本地缓存】，如果是，则每次都会copy StreamingAssets
        /// </summary>
        public static readonly bool IsAlwaysClearStream = false;

        /// <summary>
        /// 复制缓存文件到本地时，是否需要对文件名进行【进制转换】
        /// </summary>
        public static readonly bool IsHexBinDecOct = false;

        /// <summary>
        /// 复制缓存文件到本地时，是否需要【解压】
        /// </summary>
        public static readonly bool IsDecompression = false;

        /// <summary>
        /// 客户端大版本号（不重新出包不会改变）
        /// </summary>
        //public static readonly int StaticVersion = 1;

        #endregion

        #region 路径

        //Server
        public static string SERVER_ROOT_PATH = "未设置";
        public static string SERVER_MATCHLIST_PATH = "未设置";

        //版本验证地址
        public static string VERSION_AUTH_PATH = "未设置";

        //StreamingAssets
        public static string STREAM_ROOT_PATH = "未设置";
        public static string STREAM_MATCHLIST_PATH = "未设置";

        //Client
        public static string CLIENT_ROOT_PATH = "未设置";
        public static string CLIENT_MATCHLIST_PATH = "未设置";


        public static string RES_ASSEMBLY;
        public static string RES_SCENE;
        public static string RES_IMAGE;
        public static string RES_DATA;
        public static string RES_GAME_UI;
        public static string RES_GAME_WORLD;
        public static string RES_AUDIO;

        #endregion


        #region 静态方法

        public static void Init()
        {
            //资源下载地址 本地
            //LGlobalInfo.SERVER_ROOT_PATH = "file://" + Path.GetFullPath(Application.dataPath + @"\..\..\SERVER_ROOT_PATH\");

            //资源下载地址 网站
            if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.WindowsEditor)
            {
                LGlobalInfo.SERVER_ROOT_PATH = "http://192.168.2.84:100/AnyGame/";         //公司

                if (System.Net.Dns.GetHostName() == "Ivy")
                {
                    LGlobalInfo.SERVER_ROOT_PATH = "http://192.168.249.204/AnyGame/";           //家里
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                LGlobalInfo.SERVER_ROOT_PATH = "http://192.168.2.84:100/RoyalWar_ios/";
            }

            LGlobalInfo.SERVER_MATCHLIST_PATH = LGlobalInfo.SERVER_ROOT_PATH + @"matchlist.txt";

            //版本验证地址
            LGlobalInfo.VERSION_AUTH_PATH = "http://192.168.2.84:200/AnyGame/VerAuth.aspx?channel={0}&phonePlatformTypes={1}&cver={2}";       //公司

            if (System.Net.Dns.GetHostName() == "Ivy")
            {
                LGlobalInfo.VERSION_AUTH_PATH = "http://192.168.249.204:200/LoginWeb/VerAuth.aspx?channel={0}&phonePlatformTypes={1}&cver={2}";       //家里
            }


            //StreamingAssets
            //if (Application.platform == RuntimePlatform.Android)
            //{
            //    LGlobalInfo.STREAM_ROOT_PATH = "jar:file://" + Application.dataPath + "!/assets/";
            //}
            //else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //{
            //    LGlobalInfo.STREAM_ROOT_PATH = "file://" + Application.dataPath + "/Raw/";
            //}
            //else
            //{
            //    LGlobalInfo.STREAM_ROOT_PATH = "file://" + Application.streamingAssetsPath + "/";
            //}

            LGlobalInfo.STREAM_ROOT_PATH = Application.streamingAssetsPath + "/";
            LGlobalInfo.STREAM_MATCHLIST_PATH = LGlobalInfo.STREAM_ROOT_PATH + "matchlist.txt";



            //本地数据地址
            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                //PC 编辑器
                LGlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.dataPath + "/../../AnyGame_persistentDataPath/");
            }
            else if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.WP8Player)
            {
                //手机 真机
                LGlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.persistentDataPath + "/AnyGame_persistentDataPath/");
            }

            LGlobalInfo.CLIENT_MATCHLIST_PATH = LGlobalInfo.CLIENT_ROOT_PATH + @"matchlist.txt";

            LGlobalInfo.RES_ASSEMBLY = LGlobalInfo.CLIENT_ROOT_PATH + "res/assembly/";
            LGlobalInfo.RES_SCENE = LGlobalInfo.CLIENT_ROOT_PATH + "res/scene/";
            LGlobalInfo.RES_IMAGE = LGlobalInfo.CLIENT_ROOT_PATH + "res/image/";
            LGlobalInfo.RES_DATA = LGlobalInfo.CLIENT_ROOT_PATH + "res/data/";
            LGlobalInfo.RES_GAME_UI = LGlobalInfo.CLIENT_ROOT_PATH + "res/gameUI/";
            LGlobalInfo.RES_GAME_WORLD = LGlobalInfo.CLIENT_ROOT_PATH + "res/gameWorld/";
            LGlobalInfo.RES_AUDIO = LGlobalInfo.CLIENT_ROOT_PATH + "res/audio/";
        }

        #endregion
    }
}
