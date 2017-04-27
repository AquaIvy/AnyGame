
using DogSE.Library.Files;
using LitJson;
using System.Collections;
using System.Collections.Generic;

namespace AnyGame.Content.Manager
{
    public static class JsonMgr
    {
        /// <summary>
        /// 所有已经加载了的ui配置信息
        /// </summary>
        public static Dictionary<string, List<JsonData>> uiJsonData = new Dictionary<string, List<JsonData>>();

        /// <summary>
        /// 所有已经加载了的gameworld配置信息
        /// </summary>
        public static Dictionary<string, List<JsonData>> gameworldJsonData = new Dictionary<string, List<JsonData>>();

        /// <summary>
        /// 返回JsonData对象中是否有指定key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKey(this JsonData data, string key)
        {
            if (data == null || !data.IsObject)
            {
                return false;
            }

            IDictionary tdictionary = data as IDictionary;
            if (tdictionary == null)
            {
                return false;
            }

            return tdictionary.Contains(key);
        }

        /// <summary>
        /// 返回JsonData对象列表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<JsonData> Load(string path)
        {
            var info = FileMgr.LoadFile(path);
            List<JsonData> allJsonData = new List<JsonData>();

            foreach (var item in info)
            {
                var data = JsonMapper.ToObject(item);
                allJsonData.Add(data);
            }

            return allJsonData;
        }



        //public static List<JsonData> LoadUI(string name)
        //{
        //    if (uiJsonData.ContainsKey(name))
        //    {
        //        return uiJsonData[name];
        //    }

        //    string relativeMap = SceneMgr.RelativeMap(name + "_ab");
        //    relativeMap = relativeMap == "" ? "" : relativeMap + "/";
        //    string path = GlobalInfo.RES_GAME_UI + relativeMap + name + ".uiconfig";
        //    var config = Load(path);
        //    uiJsonData[name] = config;

        //    return uiJsonData[name];
        //}

        //public static List<JsonData> LoadGameWorld(string name)
        //{
        //    if (gameworldJsonData.ContainsKey(name))
        //    {
        //        return gameworldJsonData[name];
        //    }

        //    string path = GlobalInfo.RES_GAME_WORLD + name + ".txt";
        //    var config = Load(path);
        //    gameworldJsonData[name] = config;

        //    return gameworldJsonData[name];
        //}
    }
}
