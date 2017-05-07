using System.IO;
using System.Net;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Efficiency : EditorWindow
{

    [MenuItem("TooEfficiency/删除所有dll")]
    public static void DeleteDll()
    {
        string dllpath = Application.dataPath + "/vslib";
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
