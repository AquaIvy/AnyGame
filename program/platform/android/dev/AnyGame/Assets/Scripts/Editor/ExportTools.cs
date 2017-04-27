using Assets.Scripts.Utils;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class ExportTools : EditorWindow
{
    // 用于记录打包了哪些游戏物体
    private static string assetbundleMapPath = Application.dataPath + "/assetbundleMapData.txt";
    private static string uiMapPath = Application.dataPath + "/uiMapData.txt";

    #region 运行游戏(切换场景)

    [MenuItem("Tools/运行游戏（切换场景） %u")]
    public static void RunGame()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        //if (EditorApplication.isPlaying || EditorApplication.isPaused)
        //{
        //    //退出编辑模式
        //    Application.Quit();
        //    EditorApplication.OpenScene("Assets/Scenes/EnterGame.unity");
        //    EditorApplication.isPlaying = true;
        //}
        bool isEnterGame = EditorSceneManager.GetActiveScene().GetRootGameObjects().Select(o => o.name).FirstOrDefault(o => o == "EnterGame") != null;

        //if (EditorSceneManager.GetActiveScene().name != "Assets/Scenes/EnterGame.unity")
        if (!isEnterGame)
        {
            //这里使用PlayerPrefs保存是防止编译代码后丢失信息
            PlayerPrefs.SetString("lastOpenScene", EditorSceneManager.GetActiveScene().name);
            EditorSceneManager.OpenScene("Assets/Scenes/EnterGame.unity");
            //执行
            EditorApplication.isPlaying = true;
        }
        else
        {
            string lastOpenScene = PlayerPrefs.GetString("lastOpenScene");
            EditorSceneManager.OpenScene(lastOpenScene);
        }

    }

    #endregion

    #region 复制目录结构（Hierarchy）
    [MenuItem("Tools/复制目录结构（Hierarchy） %j")]
    public static void CopyGameObjectStruct()
    {
        UnityEngine.Object[] selects = Selection.objects;
        if (selects.Length != 1)
        {
            Debug.LogWarning("需选中一个文件");
            return;
        }

        string path = GetParentPath(((GameObject)selects[0]).transform);

        path = path + ((GameObject)selects[0]).transform.name;

        //System.Windows.Forms
        //Clipboard.SetText(path);
        string filePath = Path.GetFullPath(Application.dataPath + "/../Export/Clipboard.txt");
        var info = new List<string>();
        info.Add(path);
        FileUtils.SaveFile(filePath, info);
        OpenExploreOrFile(filePath);
    }

    /// <summary>
    /// 获得一个游戏物体的父节点路径，不包含自身
    /// 例如本身路径：/Canvas/Login/btnSign/clickme，传入的是clickme
    /// 则返回：/Canvas/Login/btnSign/
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    private static string GetParentPath(Transform root)
    {
        string path = "";
        while (root.parent != null)
        {
            path = root.parent.name + "/" + path;
            root = root.parent;
        }

        return ("/" + path);
    }

    #endregion

    #region 打开资源映射文件

    [MenuItem("Tools/打开资源映射文件（assetbundleData.txt）")]
    public static void OpenMapFile()
    {
        OpenExploreOrFile(assetbundleMapPath);
    }

    /// <summary>
    /// 打开目录/文件
    /// </summary>
    /// <param name="path"></param>
    public static void OpenExploreOrFile(string path)
    {
        System.Diagnostics.Process.Start(path);
    }

    /// <summary>
    /// 定位文件
    /// </summary>
    /// <param name="fileFullName"></param>
    private static void OpenFolderAndSelectFile(string fileFullName)
    {
        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
        psi.Arguments = "/e,/select," + fileFullName;
        System.Diagnostics.Process.Start(psi);
    }

    #endregion

    #region 打开UI配置目录

    [MenuItem("Tools/打开UI配置目录（GameUI）")]
    public static void OpenUIConfigPaht()
    {
        string path = Path.GetFullPath(Application.dataPath + "/../Export/GameUI/");
        OpenExploreOrFile(path);
    }
    #endregion

    #region 清空Console

    [MenuItem("Tools/ClearConsole %q")]
    public static void ClearConsoleOnTitle()
    {
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
    }

    #endregion

    #region 生成目录

    [MenuItem("Tools/生成目录")]
    static void CreateDirectory()
    {
        string root = Application.dataPath + "/";
        string[] dirs = new string[]
        {
            "Images",
            "Plugins",
            "Prefabs",
            "Resources",
            "Scenes",
            "Scripts",
            "StreamingAssets",
        };
        foreach (var dir in dirs)
        {
            Directory.CreateDirectory(root + dir);
        }
        //刷新一下，不然不能立马看到效果
        AssetDatabase.Refresh();
        Debug.Log("创建完毕");
    }

    #endregion

    #region 清除所有（AssetBundle名称、扩展名）

    [MenuItem("Assets/清除所有（AssetBundle名称、扩展名）")]
    static void ClearBundleName()
    {
        DateTime dtstart = DateTime.Now;

        var all = FileUtils.LoadFile(assetbundleMapPath);
        int update = 0;
        foreach (var item in all)
        {
            AssetImporter asset = AssetImporter.GetAtPath(item);
            if (asset != null)
            {
                update++;
                if (asset.assetBundleVariant != null)
                {
                    asset.assetBundleVariant = "";
                }
                if (asset.assetBundleName != null)
                {
                    asset.assetBundleName = null;
                }
                asset.SaveAndReimport();
            }
        }
        FileUtils.SaveFile(assetbundleMapPath, new List<string>());
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();

        var timespan = DateTime.Now - dtstart;
        Debug.LogFormat("重置：{3}  用时： {0}分{1}秒{2}毫秒", timespan.Minutes, timespan.Seconds, timespan.Milliseconds, update);
    }

    #endregion

    #region 设置单个（AssetBundle名称、扩展名）

    [MenuItem("Assets/设置单个（AssetBundle名称、扩展名）")]
    static void SetBundleName()
    {
        var lstdata = FileUtils.LoadFile(assetbundleMapPath);

        UnityEngine.Object[] selects = Selection.objects;
        foreach (UnityEngine.Object selected in selects)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            var find = lstdata.FirstOrDefault(o => o == path);
            if (find == null)
            {
                lstdata.Add(path);
            }
            else
            {
                Debug.LogErrorFormat("已有相同名字资源： {0}", path);
                return;
            }
            AssetImporter asset = AssetImporter.GetAtPath(path);
            asset.assetBundleName = selected.name;
            asset.assetBundleVariant = "assetbundle";
            asset.SaveAndReimport();

        }

        FileUtils.SaveFile(assetbundleMapPath, lstdata.OrderBy(o => o).ToList());
        AssetDatabase.Refresh();
    }

    #endregion

    #region 清除单个（AssetBundle名称、扩展名）

    [MenuItem("Assets/清除单个（AssetBundle名称、扩展名）")]
    static void RemoveBundleName()
    {
        var lstdata = FileUtils.LoadFile(assetbundleMapPath);

        UnityEngine.Object[] selects = Selection.objects;
        foreach (UnityEngine.Object selected in selects)
        {
            string path = AssetDatabase.GetAssetPath(selected);
            var find = lstdata.FirstOrDefault(o => o == path);
            if (find != null)
            {
                lstdata.Remove(find);
            }
            AssetImporter asset = AssetImporter.GetAtPath(path);
            asset.assetBundleVariant = null;
            asset.assetBundleName = null;
            asset.SaveAndReimport();

        }

        FileUtils.SaveFile(assetbundleMapPath, lstdata.OrderBy(o => o).ToList());
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.Refresh();
    }

    #endregion

    #region Build AssetBundles

    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        //if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        //{
        //    return;
        //}

        string targetPath = Path.GetFullPath(Path.Combine(Application.dataPath, @"../../Res_Android/"));
        Debug.Log(targetPath);

        BuildAssetBundles(EditorUserBuildSettings.activeBuildTarget, targetPath);

        //BuildAssetBundles(BuildTarget.iOS,targetPath);
    }

    static void BuildAssetBundles(BuildTarget targetPlatform, string movePath)
    {
        Directory.CreateDirectory(Application.dataPath + "/../Export/AssetBundle/");

        //根据平台打包资源
        BuildPipeline.BuildAssetBundles("Export/AssetBundle/",
            BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.ForceRebuildAssetBundle,
            targetPlatform);

        //加载映射文件
        var assetbundleMap = FileUtils.LoadFile(assetbundleMapPath);
        var uiMapDic = FileUtils.LoadDictionary(uiMapPath, "uiName", "relativePath");

        //是否有未知文件没有移动，若没有，则最后打开该目录方便操作
        bool sthNotMove = false;

        foreach (var item in assetbundleMap)
        {
            //资源包会被改名成小写，这里再改回原样
            string abName = Path.GetFileNameWithoutExtension(item);
            string bundlePath = Path.GetFullPath(Application.dataPath + "/../Export/AssetBundle/" + abName + ".assetbundle");
            string tmpPath = Path.GetFullPath(Application.dataPath + "/../Export/AssetBundle/" + abName + "_tmp.assetbundle");

            File.Move(bundlePath, tmpPath);
            File.Move(tmpPath, bundlePath);

            //根据映射文件的扩展名来决定打包后的文件移动到哪里
            string extension = Path.GetExtension(item);
            string targetMovePath = string.Empty;

            if (extension == ".png")
            {
                targetMovePath = movePath + "res/images/" + Path.GetFileName(bundlePath);
            }
            else if (extension == ".prefab")
            {
                targetMovePath = movePath + "res/prefabs/" + Path.GetFileName(bundlePath);
            }
            else if (extension == ".unity")
            {
                targetMovePath = movePath + "res/scenes/" + Path.GetFileName(bundlePath);
            }
            else if (extension == ".shader")
            {
                targetMovePath = movePath + "res/shaders/" + Path.GetFileName(bundlePath);
            }
            else if (extension == ".mat")
            {
                targetMovePath = movePath + "res/materials/" + Path.GetFileName(bundlePath);
            }
            else if (extension == ".mp3" || extension == ".wav" || extension == ".ogg")
            {
                targetMovePath = movePath + "res/audios/" + Path.GetFileName(bundlePath);
            }
            else
            {
                sthNotMove = true;
                Debug.LogFormat("不知移动到何处： {0}", bundlePath);
            }

#if UNITY_EDITOR_WIN
            if (File.Exists(targetMovePath))
            {
                File.Delete(targetMovePath);
            }

            if (!string.IsNullOrEmpty(targetMovePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetMovePath));
                File.Move(bundlePath, targetMovePath);
                Debug.LogFormat("移动文件 {0}    {1}", item, targetMovePath);
            }
#endif

        }

        FileUtils.SaveDictionary(uiMapPath, uiMapDic, "uiName", "relativePath");

        //删除掉导出目录下的 *.manifest 文件
        var files = Directory.GetFiles(Application.dataPath + "/../Export/AssetBundle/", "*.manifest");
        foreach (var item in files)
        {
            File.Delete(item);
        }
        var abpath = Application.dataPath + "/../Export/AssetBundle/AssetBundle";
        if (File.Exists(abpath))
        {
            File.Delete(abpath);
        }


        if (sthNotMove)
        {
#if UNITY_EDITOR_WIN
            System.Diagnostics.Process.Start(Application.dataPath + "/../Export/AssetBundle/");
#endif
        }
        else
        {
            Debug.LogFormat("打包成功，并移动 {0} 个文件", assetbundleMap.Count);
        }
    }

    #endregion

    #region 生成游戏世界结构

    [MenuItem("Assets/生成游戏世界结构")]
    static void CreateGameWorldStruct()
    {
        ClearConsoleOnTitle();

        /*说明
        该方法仅适用于生成场景的配置信息，以便程序运行时可以根据该配置动态加载场景物体，
        如下代码会以json格式保存信息，为防止误操作，需先选中“GameWorld”物体再执行

        注意：
        需先将Model保存成预设，通过预设创建物体，【必须保证预设与场景中的物体名称相同】！！
        可存在相同名称的物体，如需脚本控制，
        */
        var selects = Selection.objects;
        if (selects.Length != 1 || selects[0].name != "GameWorld")
        {
            Debug.LogError("selects.Length != 1 || selects[0].name != \"GameWorld\"");
            return;
        }

        List<string> allJsonItems = new List<string>();

        var parent = ((GameObject)selects[0]).transform;
        var cntChilds = parent.childCount;
        for (int i = 0; i < cntChilds; i++)
        {
            //获取一个场景物体
            var child = parent.GetChild(i);
            JsonData jsData = new JsonData();

            jsData["name"] = child.name;

            #region Transform

            var scriptTransform = child.GetComponent<Transform>();

            jsData["P"] = new JsonData();
            jsData["P"]["X"] = scriptTransform.localPosition.x;
            jsData["P"]["Y"] = scriptTransform.localPosition.y;
            jsData["P"]["Z"] = scriptTransform.localPosition.z;

            jsData["R"] = new JsonData();
            jsData["R"]["X"] = scriptTransform.localEulerAngles.x;
            jsData["R"]["Y"] = scriptTransform.localEulerAngles.y;
            jsData["R"]["Z"] = scriptTransform.localEulerAngles.z;

            jsData["S"] = new JsonData();
            jsData["S"]["X"] = scriptTransform.localScale.x;
            jsData["S"]["Y"] = scriptTransform.localScale.y;
            jsData["S"]["Z"] = scriptTransform.localScale.z;

            #endregion

            #region Animation

            //var anim = child.GetComponent<Animation>();
            //if (anim != null)
            //{
            //    anim.
            //}

            #endregion

            #region AddComponent

            var scriptAddCom = child.GetComponent<AddComponent>();
            if (scriptAddCom != null)
            {
                string coms = string.Empty;
                for (int comIndex = 0; comIndex < scriptAddCom.scriptName.Length; comIndex++)
                {
                    if (scriptAddCom.scriptName[comIndex] == null)
                    {
                        Debug.LogWarningFormat("脚本未赋值  {0}", child.name);
                        continue;
                    }

                    //取出所有 脚本
                    string comname = scriptAddCom.scriptName[comIndex].ToString().Replace(scriptAddCom.scriptName[comIndex].name, "");
                    comname = comname.Replace("(", "").Replace(")", "").Trim();

                    coms += comname + "|";
                }

                if (!string.IsNullOrEmpty(coms))
                {
                    jsData["Component"] = coms.Substring(0, coms.Length - 1);
                }
            }

            #endregion

            string oneItem = jsData.ToJson();
            allJsonItems.Add(oneItem);

        }

        string gameworldPath = string.Format(Application.dataPath + "/../Export/GameWorld/{0}.txt", Path.GetFileNameWithoutExtension(EditorSceneManager.GetActiveScene().name));
        FileUtils.CreateDirectory(Path.GetDirectoryName(gameworldPath));
        FileUtils.SaveFile(gameworldPath, allJsonItems);

#if UNITY_EDITOR_WIN
        //将生成的文件复制到服务器目录
        string androidServerPath = @"D:\WebSite\ClientRescources\RoyalWar\res\GameWorld\";
        Directory.CreateDirectory(androidServerPath);
        File.Copy(gameworldPath, androidServerPath + Path.GetFileName(gameworldPath), true);

        string iosServerPath = @"D:\WebSite\ClientRescources\RoyalWar_ios\res\GameWorld\";
        Directory.CreateDirectory(iosServerPath);
        File.Copy(gameworldPath, iosServerPath + Path.GetFileName(gameworldPath), true);
#endif

        Debug.Log("GameWorld 配置文件生成完毕，已复制到服务器目录");
    }

    #endregion

    #region 生成UI结构

    //private static Material mat = AssetDatabase.LoadAssetAtPath("Assets/Material/UIETC.mat", typeof(Material)) as Material;
    private static Material mat = AssetDatabase.LoadAssetAtPath("Assets/Material/Aqua-UIColor.mat", typeof(Material)) as Material;

    [MenuItem("Assets/生成UI结构")]
    public static void CreateUIStruct()
    {
        ClearConsoleOnTitle();

        var selects = Selection.objects;
        if (selects.Length != 1)
        {
            Debug.LogError("selects.Length != 1");
            return;
        }

        var root = ((GameObject)selects[0]).transform;
        if (!root.name.Contains("_ab"))
        {
            Debug.LogError("请选择带\"_ab\"后缀的文件进行操作\n");
            return;
        }
        string selectPath = AssetDatabase.GetAssetPath(selects[0]);
        if (!selectPath.StartsWith("Assets/Prefabs/UI/"))
        {
            Debug.LogError("文件必须放在 \"Assets/Prefabs/UI\" 目录下\n");
            return;
        }

        List<string> uiJsonItems = new List<string>();
        List<string> ctrlJsonItems = new List<string>();

        SearchUIChild(root, uiJsonItems, ctrlJsonItems, root.name);
        AssetDatabase.SaveAssets();

        //导出目录
        string relativePath = Path.GetDirectoryName(selectPath).Replace("Assets/Prefabs/UI/", "");
        string uiPath = string.Format(Application.dataPath + "/../Export/GameUI/{0}/{1}.uiconfig", relativePath, root.name.Replace("_ab", ""));
        string ctrlPath = string.Format(Application.dataPath + "/../Export/GameUI/{0}/{1}.ctrlconfig", relativePath, root.name.Replace("_ab", ""));

        FileUtils.CreateDirectory(Path.GetDirectoryName(uiPath));
        FileUtils.SaveFile(uiPath, uiJsonItems);
        FileUtils.SaveFile(ctrlPath, ctrlJsonItems);

#if UNITY_EDITOR_WIN
        //将生成的文件复制到服务器目录
        string androidServerPath = @"D:\WebSite\ClientRescources\RoyalWar\res\GameUI\" + relativePath + @"\";
        string 主机名 = Dns.GetHostName();
        if (主机名 == "Ivy")
        {
            androidServerPath = @"D:\WebSite\Client\RoyalWar\res\GameUI\" + relativePath + @"\"; ;
        }
        Directory.CreateDirectory(androidServerPath);
        File.Copy(uiPath, androidServerPath + Path.GetFileName(uiPath), true);
        File.Copy(ctrlPath, androidServerPath + Path.GetFileName(ctrlPath), true);

        string iosServerPath = @"D:\WebSite\ClientRescources\RoyalWar_ios\res\GameUI\" + relativePath + @"\"; ;
        if (主机名 == "Ivy")
        {
            iosServerPath = @"D:\WebSite\Client\RoyalWar_ios\res\GameUI\" + relativePath + @"\"; ;
        }
        Directory.CreateDirectory(iosServerPath);
        File.Copy(uiPath, iosServerPath + Path.GetFileName(uiPath), true);
        File.Copy(ctrlPath, iosServerPath + Path.GetFileName(ctrlPath), true);
#endif

        Debug.Log("UI 配置文件生成完毕，已复制到服务器目录");
    }

    private static void SearchUIChild(Transform root, List<string> uiJsonItems, List<string> ctrlJsonItems, string path)
    {
        //已经无力解释为什么这样做了。。。
        //去掉路径中的"_ab"，这样和制作界面的路径完全一致。。。
        int firstLine = path.IndexOf('/');
        string s1 = "";
        string s2 = "";
        if (firstLine < 0)  //物体自己
        {
            s1 = path.Replace("_ab", "");
        }
        else    //包含子孩子
        {
            s1 = path.Substring(0, firstLine).Replace("_ab", "");
            s2 = path.Substring(firstLine, path.Length - firstLine);
        }

        JsonData jsData = new JsonData();
        jsData["name"] = s1 + s2;
        bool isNeedAdd = false;

        var sprite = root.GetComponent<Image>();
        if (sprite != null && sprite.sprite != null)
        {
            string fullname = sprite.sprite.name;
            int index = fullname.IndexOf('_');
            //图片中不含"_"  要么使用了内置图片，要么错误
            if (index < 0)
            {
                if (fullname == "Background"
                    || fullname == "Checkmark"
                    || fullname == "DropdownArrow"
                    || fullname == "InputFieldBackground"
                    || fullname == "Knob"
                    || fullname == "UIMask"
                    || fullname == "UISprite")
                {
                    //Debug.LogWarningFormat("使用了内置图片: {0}  {1}", fullname, path);

                    isNeedAdd = true;

                    jsData["Atlas"] = "Unity自带";
                    jsData["SourceImage"] = fullname;
                }
                else
                {
                    throw new NotSupportedException(string.Format("图片名错误:  位置{0}  名字{1}", path, fullname));
                }
            }
            else
            {
                isNeedAdd = true;

                ////方法一
                ////每张图片的前缀（第一个"_"之前的字母）表示所在图集
                //jsData["Atlas"] = fullname.Substring(0, index);

                ////方法二
                ////图片名字随便起，图片所在文件夹为图集
                var texpath = AssetDatabase.GetAssetPath(sprite.sprite.texture);
                jsData["Atlas"] = Path.GetFileName(Path.GetDirectoryName(texpath));

                jsData["SourceImage"] = fullname;

                sprite.sprite = null;

            }

            sprite.material = mat;
        }

        var label = root.GetComponent<Text>();
        if (label != null)
        {
            isNeedAdd = true;
            jsData["Font"] = root.GetComponent<Text>().text;

            label.font = null;
        }

        if (isNeedAdd)
        {
            foreach (var item in uiJsonItems)
            {
                JsonData tmp = JsonMapper.ToObject(item);
                if (tmp["name"].ToString() == jsData["name"].ToString())
                {
                    throw new NotSupportedException("存在相同路径的文件: " + jsData["name"].ToString());
                }
            }
            string oneItem = jsData.ToJson();
            uiJsonItems.Add(oneItem);
        }

        //找到需要交互的游戏物体
        var control = root.GetComponent<NeedControl>();
        if (control != null)
        {
            var name = control.CtrlName;
            foreach (var item in ctrlJsonItems)
            {
                JsonData tmp = JsonMapper.ToObject(item);
                if (tmp["name"].ToString() == name)
                {
                    throw new NotSupportedException("存在相同名称的需要交互物体: " + tmp["Path"].ToString());
                }
            }

            JsonData ctrl = new JsonData();
            ctrl["name"] = name;
            ctrl["Path"] = jsData["name"].ToString();
            ctrlJsonItems.Add(ctrl.ToJson());

            DestroyImmediate(control, true);
        }

        //删除子孩子
        if (root.GetComponent<NeedDeleteChild>() != null)
        {
            int childCount = root.childCount;
            for (int i = 0; i < childCount; i++)
            {
                DestroyImmediate(root.GetChild(0).gameObject, true);
            }

            DestroyImmediate(root.GetComponent<NeedDeleteChild>(), true);
        }

        //继续遍历下一个物体
        for (int i = 0; i < root.childCount; i++)
        {
            var child = root.GetChild(i);
            SearchUIChild(child, uiJsonItems, ctrlJsonItems, path + "/" + child.name);
        }
    }

    #endregion


    #region 复制"_ab"文件

    [MenuItem("Assets/复制\"_ab\"文件")]
    public static void CopyPrefabFile()
    {
        var selects = Selection.objects;
        foreach (var item in selects)
        {
            //方法一
            //string path = Path.GetFullPath(Application.dataPath + "/../" + AssetDatabase.GetAssetPath(item));
            //if (File.Exists(path))
            //{
            //    string destPath = Path.GetDirectoryName(path) + "/"
            //        + Path.GetFileNameWithoutExtension(path) + "_ab"
            //        + Path.GetExtension(path);

            //    string metaPath = Path.GetDirectoryName(path) + "/"
            //        + Path.GetFileNameWithoutExtension(path) + "_ab"
            //        + ".meta";

            //    File.Delete(destPath);
            //    File.Delete(metaPath);
            //    File.Copy(path, destPath);
            //}

            //方法二
            string sourcepath = AssetDatabase.GetAssetPath(item);
            string destpath = Path.GetDirectoryName(sourcepath) + "/"
                    + Path.GetFileNameWithoutExtension(sourcepath) + "_ab"
                    + Path.GetExtension(sourcepath);

            AssetDatabase.DeleteAsset(destpath);
            AssetDatabase.CopyAsset(sourcepath, destpath);
        }

        AssetDatabase.Refresh();
        Debug.LogFormat("已复制【_ab】 {0} 个.", selects.Length);
    }

    #endregion

    [MenuItem("Assets/根据Sprite获得路径")]
    public static void CopyPrefabFile1()
    {
        var selects = Selection.objects;
        if (selects.Length != 1)
        {
            Debug.LogError("selects.Length != 1");
            return;
        }

        foreach (var item in selects)
        {
            GameObject go = (GameObject)item;
            var image = go.GetComponent<Image>();
            if (image != null)
            {
                var tex = image.sprite.texture;
                var path = AssetDatabase.GetAssetPath(tex);
                Debug.Log("" + item.name + "  " + path);
            }
        }

        AssetDatabase.Refresh();
        Debug.LogFormat("已复制【_ab】 {0} 个.", selects.Length);
    }

}
