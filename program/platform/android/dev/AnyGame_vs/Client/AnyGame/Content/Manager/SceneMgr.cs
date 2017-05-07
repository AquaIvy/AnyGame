
using DogSE.Library.Files;
using DogSE.Library.Log;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AnyGame.Content.Manager
{
    public static class SceneMgr
    {
        public static bool isRunEmptyScene = true;

        /// <summary>
        /// 创建一个UI界面
        /// </summary>
        /// <param name="uiName">名字（区分大小写）</param>
        /// <param name="parent">该物体的父节点</param>
        /// <param name="parentPath">父节点的完整路径</param>
        /// <param name="onUpdate">CreateUI中每创建一个ui触发一次</param>
        /// <param name="onFinish">完成CreateUI后事件</param>
        /// <param name="onAfterFinish">完成BindingEvents后事件(在onFinish完成后触发)</param>
        /// <returns></returns>
        //public static IEnumerator<float> CreateUI(string uiName,
        //    Transform parent,
        //    string parentPath,
        //    Action<int, int> onUpdate = null,
        //    Action<Action> onFinish = null,
        //    Action onAfterFinish = null)
        //{
        //    string relativeMap = RelativeMap(uiName + "_ab");
        //    relativeMap = relativeMap == "" ? "" : relativeMap + "/";
        //    string assetbundlePath = GlobalInfo.RES_GAME_UI + relativeMap + uiName + "_ab.assetbundle";

        //    //创建UI 空架子
        //    var uigo = SceneMgr.CreateUIGameObject(assetbundlePath, parent);
        //    if (uigo == null) { yield break; }

        //    uigo.name = uigo.name.Replace("_ab", "");

        //    //加载 .uiconfig文件
        //    var allJsonData = JsonMgr.LoadUI(uiName);

        //    int curUICount = 0;
        //    int maxUICount = allJsonData.Count;
        //    Logs.Info("UI【{0}】共需处理子节点【{1}】个", uiName, maxUICount);

        //    //填补Image，Font等资源
        //    foreach (var jsonData in allJsonData)
        //    {
        //        //找到需要填补的物体
        //        var child = GameObject.Find(parentPath + jsonData["name"].ToString());
        //        if (child == null)
        //        {
        //            Logs.Warn("GameObject.Find 未找到物体: {0}", parentPath + jsonData["name"].ToString());
        //            continue;
        //        }

        //        //填补Image
        //        //if (JsonMgr.ContainsKey(jsonData, "Atlas"))           //1.使用静态方法（需删掉this）
        //        if (jsonData.ContainsKey("Atlas"))                      //2.使用扩展方法（需包含this）
        //        {
        //            string atlasname = jsonData["Atlas"].ToString();
        //            string spritename = jsonData["SourceImage"].ToString();

        //            if (atlasname != "Unity自带")
        //            {
        //                //PC 平台用原始Png图片资源
        //                if (Application.platform == RuntimePlatform.WindowsEditor
        //                    || Application.platform == RuntimePlatform.OSXEditor)
        //                {
        //                    // 1
        //                    //var atlas = SpriteMgr.LoadAtlas(atlasname);
        //                    //if (atlas == null || !atlas.ContainsKey(spritename))
        //                    //{
        //                    //    Logs.Error("图集【{0}】为空或不包含精灵【{1}】", atlasname, spritename);
        //                    //    continue;
        //                    //}

        //                    //child.GetComponent<Image>().sprite = atlas[spritename];


        //                    // 2
        //                    //var atlas = TextureMgr.Load(atlasname);
        //                    //if (atlas == null || !atlas.sprites.ContainsKey(spritename))
        //                    //{
        //                    //    Logs.Error("图集【{0}】为空或不包含精灵【{1}】", atlasname, spritename);
        //                    //    continue;
        //                    //}
        //                    //child.GetComponent<Image>().sprite = atlas.sprites[spritename];
        //                    //child.GetComponent<Image>().material = atlas.material;


        //                    // 3
        //                }
        //                //手机 平台使用压缩纹理
        //                else if (Application.platform == RuntimePlatform.Android
        //                    || Application.platform == RuntimePlatform.IPhonePlayer
        //                    || Application.platform == RuntimePlatform.WP8Player)
        //                {
        //                    // 1
        //                    //var atlas = SpriteMgr.LoadAtlas(atlasname);
        //                    //if (atlas == null || !atlas.ContainsKey(spritename))
        //                    //{
        //                    //    Logs.Error("图集【{0}】为空或不包含精灵【{1}】", atlasname, spritename);
        //                    //    continue;
        //                    //}

        //                    //child.GetComponent<Image>().sprite = atlas[spritename];
        //                    //child.GetComponent<Image>().material = null;

        //                    // 2
        //                    //var atlas = TextureMgr.Load(atlasname);
        //                    //if (atlas == null || !atlas.sprites.ContainsKey(spritename))
        //                    //{
        //                    //    Logs.Error("图集【{0}】为空或不包含精灵【{1}】", atlasname, spritename);
        //                    //    continue;
        //                    //}
        //                    //child.GetComponent<Image>().sprite = atlas.sprites[spritename];
        //                    //child.GetComponent<Image>().material = atlas.material;

        //                    // 3
        //                }

        //            }

        //            //PC 平台不需要使用材质
        //            if (Application.platform == RuntimePlatform.WindowsEditor
        //                || Application.platform == RuntimePlatform.OSXEditor)
        //            {
        //                child.GetComponent<Image>().material = null;
        //            }

        //        }

        //        //填补Font
        //        if (JsonMgr.ContainsKey(jsonData, "Font"))
        //        {
        //            child.GetComponent<Text>().font = FontMgr.font;
        //            child.GetComponent<Text>().text = jsonData["Font"].ToString();
        //        }

        //        curUICount++;
        //        float percent = (curUICount * 1.0f) / maxUICount;

        //        if (onUpdate != null && curUICount % 20 == 0)
        //        {
        //            onUpdate(curUICount, maxUICount);
        //        }

        //        if (curUICount % 20 == 0)
        //        {
        //            yield return 1.0f;
        //        }
        //    }


        //    if (onUpdate != null)
        //    {
        //        onUpdate(curUICount, maxUICount);
        //    }

        //    if (onFinish != null)
        //    {
        //        onFinish(onAfterFinish);
        //    }
        //}

        ///// <summary>
        ///// 创建游戏世界
        ///// </summary>
        ///// <param name="scene"></param>
        ///// <returns></returns>
        //public static IEnumerator<float> CreateGameWorld(string scene,
        //    Transform parent,
        //    string parentPath,
        //    Action<int, int> onUpdate = null,
        //    Action<Action> onFinish = null,
        //    Action onAfterFinish = null)
        //{
        //    List<JsonData> allJsonData = JsonMgr.LoadGameWorld(scene);
        //    Logs.Info("游戏世界物体 = " + allJsonData.Count);

        //    int curUICount = 0;
        //    int maxUICount = allJsonData.Count;

        //    //根据配置生成场景物体
        //    foreach (var jsonData in allJsonData)
        //    {
        //        //assetbundle完整路径
        //        string assetpath = string.Format(GlobalInfo.RES_GAME_WORLD + @"{0}.assetbundle", jsonData["name"].ToString());

        //        var localPosition = new Vector3(Convert.ToSingle(jsonData["P"]["X"].ToString()), Convert.ToSingle(jsonData["P"]["Y"].ToString()), Convert.ToSingle(jsonData["P"]["Z"].ToString()));
        //        var localRotation = Quaternion.Euler(Convert.ToSingle(jsonData["R"]["X"].ToString()), Convert.ToSingle(jsonData["R"]["Y"].ToString()), Convert.ToSingle(jsonData["R"]["Z"].ToString()));
        //        var localScale = new Vector3(Convert.ToSingle(jsonData["S"]["X"].ToString()), Convert.ToSingle(jsonData["S"]["Y"].ToString()), Convert.ToSingle(jsonData["S"]["Z"].ToString()));

        //        var go = CreateGameObject(assetpath, parent, localPosition, localRotation, localScale);
        //        if (go == null)
        //        {
        //            continue;
        //        }

        //        //挂载脚本
        //        //if (JsonMgr.ContainsKey(jsonData, "Component"))
        //        //{
        //        //    var components = jsonData["Component"].ToString().Split('|');
        //        //    foreach (var comName in components)
        //        //    {
        //        //        if (!string.IsNullOrEmpty(comName))
        //        //        {
        //        //            //var assembly = Game.assembly["AnyGame"];
        //        //            //var type = assembly.GetType(comName);
        //        //            //go.AddComponent(type);

        //        //            Logs.Warn("此处应该添加脚本：{0}", comName);
        //        //        }
        //        //    }
        //        //}

        //        curUICount++;
        //        float percent = (curUICount * 1.0f) / maxUICount;

        //        if (onUpdate != null)
        //        {
        //            onUpdate(curUICount, maxUICount);

        //            float maxSleep = 500.0f / maxUICount;
        //            Game.instance.Sleep((int)UnityEngine.Random.Range(maxSleep / 4, maxSleep));
        //        }

        //        yield return percent;
        //    }

        //    //Game.Instance.Sleep((int)UnityEngine.Random.Range(200, 500));
        //    if (onFinish != null)
        //    {
        //        onFinish(onAfterFinish);
        //    }
        //}


        /// <summary>
        /// 加载并创建游戏物体
        /// </summary>
        /// <param name="path">AssetBundle路径</param>
        /// <param name="p"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        //public static GameObject CreateGameObject(string path, Transform parent, Vector3 p, Quaternion r, Vector3 s)
        //{
        //    if (!File.Exists(path))
        //    {
        //        Logs.Error("未找到文件：{0}", path);
        //        return null;
        //    }

        //    AssetBundle bundle = AssetBundle.LoadFromFile(path);
        //    string name = Path.GetFileNameWithoutExtension(path);
        //    var asset = bundle.LoadAsset(name);

        //    GameObject go = (GameObject)GameObject.Instantiate(asset);
        //    go.name = go.name.Replace("(Clone)", "");
        //    go.transform.parent = parent;

        //    go.transform.localPosition = p;
        //    go.transform.localRotation = r;
        //    go.transform.localScale = s;


        //    bundle.Unload(false);
        //    bundle = null;

        //    return go;
        //}

        ///// <summary>
        ///// 加载并创建UI物体
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="parent"></param>
        ///// <returns></returns>
        //public static GameObject CreateUIGameObject(string path, Transform parent = null)
        //{
        //    if (!File.Exists(path))
        //    {
        //        Logs.Error("未找到文件：{0}", path);
        //        return null;
        //    }

        //    //AssetBundle bundle = AssetBundle.CreateFromFile(path);
        //    //var asset = bundle.LoadAllAssets();

        //    //GameObject go = (GameObject)GameObject.Instantiate(asset[0]);
        //    //go.name = go.name.Replace("(Clone)", "");

        //    AssetBundle bundle = AssetBundle.LoadFromFile(path);
        //    string name = Path.GetFileNameWithoutExtension(path);
        //    var asset = bundle.LoadAsset(name);

        //    GameObject go = (GameObject)GameObject.Instantiate(asset);
        //    go.name = go.name.Replace("(Clone)", "");

        //    go.transform.SetParent(parent);
        //    go.transform.localPosition = Vector3.zero;
        //    go.transform.localRotation = Quaternion.Euler(Vector3.zero);
        //    go.transform.localScale = Vector3.one;

        //    bundle.Unload(false);
        //    bundle = null;

        //    return go;
        //}

        ///// <summary>
        ///// 异步 跳转场景
        ///// </summary>
        ///// <param name="scene"></param>
        ///// <returns></returns>
        //public static IEnumerator<int> SkipSceneAsync(SceneType scene)
        //{
        //    Logs.Warn("准备跳转场景: {0}", scene);
        //    string sceneName = scene.ToString();
        //    string empty = isRunEmptyScene ? "empty" : "empty2";
        //    isRunEmptyScene = !isRunEmptyScene;
        //    string path = string.Format(GlobalInfo.RES_GAME_UI + "{0}.assetbundle", empty);


        //    if (!File.Exists(path))
        //    {
        //        Debug.LogError("未找到场景文件 " + path);
        //        yield break;
        //    }

        //    //GlobalInfo.WAIT_CREATE_UI_WORLD = sceneName;
        //    Logs.Warn("!!!  {0}   {1}", empty, path);
        //    AssetBundle bundle = AssetBundle.LoadFromFile(path);
        //    var asyncOperation = SceneManager.LoadSceneAsync(empty);
        //    //Log.Warning("asyncOperation  {0} {1}", asyncOperation.progress, asyncOperation.isDone);

        //    bundle.Unload(false);
        //    bundle = null;

        //    //Log.Warning("here is run");

        //    for (int i = 0; i < 10; i++)
        //    {
        //        System.Threading.Thread.Sleep(1);
        //        yield return 1;
        //    }
        //}

        ///// <summary>
        ///// 同步 跳转场景
        ///// </summary>
        ///// <param name="scene"></param>
        //public static void SkipSceneSync(string scene)
        //{
        //    Logs.Warn("准备跳转场景: {0}", scene);
        //    string path = string.Format(GlobalInfo.RES_SCENE + "{0}.assetbundle", scene);

        //    if (!File.Exists(path))
        //    {
        //        Debug.LogError("未找到场景文件 " + path);
        //        return;
        //    }

        //    AssetBundle bundle = AssetBundle.LoadFromFile(path);
        //    SceneManager.LoadScene(scene);

        //    //System.Threading.Thread.Sleep(100);


        //    Task.Invoke(t =>
        //    {
        //        if (SceneManager.GetActiveScene().name == scene)
        //        {
        //            bundle.Unload(false);
        //            bundle = null;

        //            return true;
        //        }

        //        return false;
        //    });

        //}

        public static UnityEngine.Object LoadObject(string path, string name)
        {
            if (!File.Exists(path))
            {
                Logs.Error("未找文件 " + path);
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            var asset = bundle.LoadAsset(name);

            bundle.Unload(false);
            bundle = null;

            return asset;
        }

        private static Dictionary<string, string> relativeMap = null;
        //public static string RelativeMap(string key)
        //{
        //    if (relativeMap == null)
        //    {
        //        relativeMap = LoadDictionary(GlobalInfo.RES_GAME_UI + "uiMapData.txt", "uiName", "relativePath");
        //    }

        //    string map = "";
        //    if (relativeMap.ContainsKey(key))
        //    {
        //        map = relativeMap[key];
        //    }

        //    return map;
        //}

        //public static Dictionary<string, string> LoadDictionary(string path, string keyname, string valuename)
        //{
        //    var lst = FileMgr.LoadFile(path);
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    foreach (var item in lst)
        //    {
        //        JsonData jsData = JsonMapper.ToObject(item);
        //        if (jsData.ContainsKey(keyname) && jsData.ContainsKey(valuename))
        //        {
        //            dic[jsData[keyname].ToString()] = jsData[valuename].ToString();
        //        }
        //        else
        //        {
        //            Logs.Error("不包含key {0} 或 {1}", keyname, valuename);
        //        }
        //    }
        //    Logs.Info("Dic 【{0}】 has 【{1}】 items.", path, dic.Count);
        //    foreach (var item in dic)
        //    {
        //        Logs.Info("dic {0} {1}", item.Key, item.Value);
        //    }

        //    return dic;
        //}

        //public static List<string> SaveDictionary(string path, Dictionary<string, string> dic, string keyname, string valuename)
        //{
        //    List<string> lst = new List<string>();
        //    foreach (var item in dic)
        //    {
        //        JsonData jsData = new JsonData();
        //        jsData[keyname] = item.Key;
        //        jsData[valuename] = item.Value;

        //        lst.Add(jsData.ToJson());
        //    }

        //    FileMgr.SaveFile(path, lst);
        //    return lst;
        //}
    }

    //public enum SceneType
    //{
    //    Login = 0,
    //    Update = 1
    //}
}
