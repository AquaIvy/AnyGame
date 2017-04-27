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


        #endregion

        #region 路径

        //Server
        public static string SERVER_ROOT_PATH;
        public static string SERVER_MATCHLIST_PATH;

        //StreamingAssets
        public static string STREAM_ROOT_PATH;
        public static string STREAM_MATCHLIST_PATH;

        //Client
        public static string CLIENT_ROOT_PATH;
        public static string CLIENT_MATCHLIST_PATH;

        //常用资源路径
        public static string RES_ASSEMBLY;
        //public static string RES_SCENE;
        //public static string RES_IMAGE;
        //public static string RES_DATA;
        //public static string RES_AUDIO;

        #endregion


        #region 静态方法

        public static void Init()
        {
            //客户端资源地址 本地
            //LGlobalInfo.SERVER_ROOT_PATH = "file://" + Path.GetFullPath(Application.dataPath + @"\..\..\SERVER_ROOT_PATH\");

            //端口号
            //Client    201
            //LoginWeb  202
            //GMTools   9001起
            //Server    4601起


            //客户端资源地址
            if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //LGlobalInfo.SERVER_ROOT_PATH = "http://www.aquaivy.com:201/AnyGame/";         //Aliyun ECS
                LGlobalInfo.SERVER_ROOT_PATH = "http://192.168.2.84:100/AnyGame/";         //公司

                if (System.Net.Dns.GetHostName() == "Aqua")
                {
                    LGlobalInfo.SERVER_ROOT_PATH = "http://192.168.249.204/AnyGame/";           //家里
                }
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                LGlobalInfo.SERVER_ROOT_PATH = "http://www.aquaivy.com:201/AnyGame/";
            }

            LGlobalInfo.SERVER_MATCHLIST_PATH = LGlobalInfo.SERVER_ROOT_PATH + @"matchlist.txt";

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

            //安装包中Stream流地址
            LGlobalInfo.STREAM_ROOT_PATH = Application.streamingAssetsPath + "/";
            LGlobalInfo.STREAM_MATCHLIST_PATH = LGlobalInfo.STREAM_ROOT_PATH + "matchlist.txt";


            //本地数据地址
            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXEditor)
            {
                LGlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.dataPath + "/../../AnyGame_persistentDataPath/");
            }
            else if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer
                || Application.platform == RuntimePlatform.WP8Player)
            {
                LGlobalInfo.CLIENT_ROOT_PATH = Path.GetFullPath(Application.persistentDataPath + "/AnyGame_persistentDataPath/");
            }

            LGlobalInfo.CLIENT_MATCHLIST_PATH = LGlobalInfo.CLIENT_ROOT_PATH + @"matchlist.txt";

            //常用资源路径
            LGlobalInfo.RES_ASSEMBLY = LGlobalInfo.CLIENT_ROOT_PATH + "res/assembly/";
            //LGlobalInfo.RES_SCENE = LGlobalInfo.CLIENT_ROOT_PATH + "res/scenes/";
            //LGlobalInfo.RES_IMAGE = LGlobalInfo.CLIENT_ROOT_PATH + "res/images/";
            //LGlobalInfo.RES_DATA = LGlobalInfo.CLIENT_ROOT_PATH + "res/data/";
            //LGlobalInfo.RES_AUDIO = LGlobalInfo.CLIENT_ROOT_PATH + "res/audios/";
        }

        #endregion
    }
}
