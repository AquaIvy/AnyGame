
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using LitJson;
using System.IO;

/// <summary>
/// 语言包支持
/// </summary>
static class Lang
{

    private static Dictionary<string, string> s_dict = new Dictionary<string, string>();

    public static void Init()
    {
        string langPath = GlobalInfo.RES_DATA + "LangClient.txt";


        var bytes = File.ReadAllBytes(langPath);

        JsonReader jr = new JsonReader(Encoding.UTF8.GetString(bytes));

        string key = null;
        string value = null;

        while (jr.Read())
        {
            if (jr.Value != null)
            {
                if (jr.Token == JsonToken.PropertyName)
                {
                    key = jr.Value.ToString();
                }
                else if (jr.Token == JsonToken.String && key != null)
                {
                    value = jr.Value.ToString();

                    if (!s_dict.ContainsKey(key))
                    {
                        s_dict.Add(key, value);
                    }
                    else
                    {
                        Debug.LogError("语言包重复key值  " + key);
                    }

                    key = null;
                    value = null;
                }
            }
        }
    }


    /// <summary>
    /// 将一个语言翻译为目标语言
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string Trans(string source)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;

        string ret;
        if (s_dict.TryGetValue(source, out ret))
            return ret;

        return "※" + source;
    }

    /// <summary>
    /// 将一个语言翻译为目标语言（带格式化输出）
    /// </summary>
    /// <param name="source"></param>
    /// <param name="objParams"></param>
    /// <returns></returns>
    public static string Trans(string source, params object[] objParams)
    {
        return string.Format(Trans(source), objParams);
    }


}

