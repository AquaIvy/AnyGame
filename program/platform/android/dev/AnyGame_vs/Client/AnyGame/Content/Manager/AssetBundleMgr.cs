using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Manager
{
    public class AssetBundleMgr
    {
        public static T Load<T>(string path, string name) where T : UnityEngine.Object
        {
            if (!File.Exists(path))
            {
                Logs.Error("no file " + path);
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            var asset = bundle.LoadAsset<T>(name);

            bundle.Unload(false);
            bundle = null;

            return asset;
        }

        public static GameObject Load(string path, string name)
        {
            var obj = Load<GameObject>(path, name);
            var ins = (GameObject)GameObject.Instantiate(obj);

            return ins;
        }
    }
}
