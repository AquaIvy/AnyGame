
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AnyGame.Content.Manager
{
    class ContentMgr
    {
        public static T LoadObject<T>(string path, string name) where T : Object
        {
            if (!File.Exists(path))
            {
                Logs.Error("未找文件 " + path);
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            var asset = bundle.LoadAsset<T>(name);

            bundle.Unload(false);
            bundle = null;

            return asset;
        }
    }
}
