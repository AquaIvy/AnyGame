using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Efficiency : EditorWindow
{

    [MenuItem("TooEfficiency/复制dll到WebSite")]
    public static void CopyDll()
    {
        string dllpath = Application.dataPath + "/dll_vs";
        string destination;

        if (Dns.GetHostName() == "LvJunxiao")
        {
            destination = @"D:\WebSite\ClientRescources\RoyalWar\res\Assembly\";
        }
        else
        {
            destination = @"D:\WebSite\Client\RoyalWar\res\Assembly\";
        }

        //string patchFilePath = @"D:\WebSite\ClientRescources\RoyalWar\文件列表自动更新工具.exe";

        var files = Directory.GetFiles(dllpath, "*.dll", SearchOption.TopDirectoryOnly);
        foreach (var item in files)
        {
            File.Copy(item, destination + Path.GetFileName(item), true);
        }
        Debug.LogFormat("复制【{0}】个dll文件", files.Length);

        //ExportTools.OpenExploreOrFile(patchFilePath);
    }


    [MenuItem("TooEfficiency/删除所有dll")]
    public static void DeleteDll()
    {
        string dllpath = Application.dataPath + "/dll_vs";
        var files = Directory.GetFiles(dllpath, "*.*", SearchOption.AllDirectories);
        foreach (var item in files)
        {
            File.Delete(item);
        }

        AssetDatabase.Refresh();

    }


    [MenuItem("TooEfficiency/子孩子个数")]
    public static void ChildCount()
    {
        var selects = Selection.objects;
        if (selects.Length != 1)
        {
            Debug.Log("selects.Length != 1 ");
            return;
        }

        var parent = ((GameObject)selects[0]).transform;
        int count = 0;
        ChildCount(parent, ref count);
        Debug.LogFormat("{0} 共有子孩子 {1} 个", parent.name, count);
    }

    public static void ChildCount(Transform root, ref int count)
    {
        int cnt = root.childCount;
        for (int i = 0; i < cnt; i++)
        {
            count++;
            var child = root.GetChild(i);
            ChildCount(child, ref count);
        }
    }
}
