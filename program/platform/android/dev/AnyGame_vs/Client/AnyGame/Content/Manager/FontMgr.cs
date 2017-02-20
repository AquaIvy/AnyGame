
using DogSE.Library.Log;
using System.IO;
using UnityEngine;

namespace AnyGame.Content.Manager
{
    public class FontMgr
    {
        public static Font font { get; private set; }

        public static void Init()
        {
            string fontPath = Path.GetFullPath(GlobalInfo.CLIENT_ROOT_PATH + "res/Font/font.assetbundle");
            Font font = FontMgr.LoadFont(fontPath);
        }


        /// <summary>
        /// 加载字体
        /// </summary>
        /// <param name="path">AssetBundle的路径</param>
        /// <returns></returns>
        public static Font LoadFont(string path)
        {
            if (!File.Exists(path))
            {
                Logs.Error("未找到文件: {0}", Path.GetFullPath(path));
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(path);

            font = bundle.LoadAsset("font", typeof(Font)) as Font;
            bundle.Unload(false);
            bundle = null;

            Logs.Info("字体加载成功: {0}", path);

            return font;
        }
    }
}
