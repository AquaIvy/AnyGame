
using DogSE.Library.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace AnyGame.Content.Manager
{

    public class SpriteMgr
    {
        /// <summary>
        /// 所有的图集
        /// </summary>
        public static Dictionary<string, Dictionary<string, Sprite>> allAtlas = new Dictionary<string, Dictionary<string, Sprite>>();

        /// <summary>
        /// 图集
        /// </summary>
        /// <param name="name">图集名称</param>
        /// <returns></returns>
        public static Dictionary<string, Sprite> LoadAtlas(string name)
        {
            if (allAtlas.ContainsKey(name))
            {
                return allAtlas[name];
            }

            string ab_path = Path.GetFullPath(GlobalInfo.RES_IMAGE + name + @".assetbundle");

            Texture2D tex = LoadTexture2D(ab_path, name);                               //AssetBundle加载方式，比下面的快将近100倍 0.0
            //Texture2D tex = LoadTexture2D(GlobalInfo.RES_ATLAS + name + @".png");     //纯C#加载方式

            string tp_path = Path.GetFullPath(GlobalInfo.RES_IMAGE + name + @".tpsheet");
            List<SpriteItem> tp = LoadTpsheet(tp_path);
            if (tex == null || tp == null)
            {
                return null;
            }

            Dictionary<string, Sprite> atlas = new Dictionary<string, Sprite>();
            //Vector2 pivot = new Vector2(0.5f, 0.5f);
            foreach (var item in tp)
            {
                if (item.isSquared)
                {
                    Sprite s = Sprite.Create(tex,
                        new Rect(item.rect.x, item.rect.y, item.rect.width, item.rect.height),
                        new Vector2(item.pivot.x, item.pivot.y),
                        100.0f,
                        0,
                        SpriteMeshType.Tight,
                        new Vector4(item.border.x, item.border.y, item.border.z, item.border.w)
                        );
                    atlas[item.name] = s;
                    //Logs.Info("添加Sprite： {0}", item.name);
                }
                else
                {
                    Sprite s = Sprite.Create(tex,
                        new Rect(item.rect.x, item.rect.y, item.rect.width, item.rect.height),
                        new Vector2(item.pivot.x, item.pivot.y)
                        //Vector2.zero
                        );
                    atlas[item.name] = s;
                    //Logs.Info("添加Sprite： {0}", item.name);
                }
            }
            allAtlas[name] = atlas;
            Logs.Info("图集【{0}】共包含精灵【{1}】个", name, atlas.Count);

            return atlas;
        }

        public static bool RemoveAtlas(string name)
        {
            if (!allAtlas.ContainsKey(name))
            {
                return true;
            }

            return allAtlas.Remove(name);
        }

        public static void ClearAtlas()
        {
            allAtlas.Clear();
        }

        /// <summary>
        /// 图集配置
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<SpriteItem> LoadTpsheet(string path)
        {
            if (!File.Exists(path))
            {
                Logs.Error("未找到文件: {0}", path);
                return null;
            }

            List<SpriteItem> si = new List<SpriteItem>();
            using (StreamReader sr = new StreamReader(path))
            {
                //加载切片信息
                while (sr.Peek() >= 0)
                {
                    string str = sr.ReadLine();
                    var s = str.Split(';');

                    if (string.IsNullOrEmpty(str)
                        || str[0] == '#'
                        || str[0] == ':')
                    {
                        continue;
                    }

                    if (s.Length != 7 && s.Length != 11)
                    {
                        Logs.Error("s.Length != 7 || s.Length != 11   Tpsheet:{0}   {1}", Path.GetFileNameWithoutExtension(path), str);
                        continue;
                    }


                    SpriteItem spriteitem = new SpriteItem()
                    {
                        name = s[0],
                        rect = new Rect
                        {
                            x = Convert.ToInt32(s[1]),
                            y = Convert.ToInt32(s[2]),
                            width = Convert.ToInt32(s[3]),
                            height = Convert.ToInt32(s[4])
                        },
                        pivot = new Vector2
                        {
                            x = Convert.ToSingle(s[5]),
                            y = Convert.ToSingle(s[6]),
                        }
                    };

                    //11位表示是九宫格
                    if (s.Length == 11)
                    {
                        spriteitem.border = new Vector4()
                        {
                            x = Convert.ToInt32(s[7]),
                            y = Convert.ToInt32(s[8]),
                            z = Convert.ToInt32(s[9]),
                            w = Convert.ToInt32(s[10])
                        };

                        spriteitem.isSquared = true;
                    }

                    si.Add(spriteitem);
                }
            }

            return si;
        }

        /// <summary>
        /// 从AssetBundle中加载名为name的资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture2D(string path, string name)
        {
            if (!File.Exists(path))
            {
                Logs.Error("未找到文件: {0}", Path.GetFullPath(path));
                return null;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            //UnityEngine.Object obj = bundle.LoadAsset("icon");
            Texture2D tex = bundle.LoadAsset(name, typeof(Texture2D)) as Texture2D;
            //var obj = bundle.LoadAsset(name, type);
            bundle.Unload(false);
            bundle = null;

            return tex;
        }

        //public static Texture2D LoadTexture2D(string path)
        //{
        //    //方法一 加载图片
        //    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        //    System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
        //    //方法二 加载图片
        //    //System.Drawing.Image img = System.Drawing.Image.FromFile(path); 

        //    MemoryStream ms = new MemoryStream();
        //    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

        //    Texture2D texture = new Texture2D(img.Width, img.Height);
        //    texture.LoadImage(ms.ToArray());

        //    return texture;
        //}
    }


    /// <summary>
    /// 精灵切片信息
    /// </summary>
    public class SpriteItem
    {
        public string name;

        public string textureName;

        public Rect rect = new Rect();

        public Vector2 pivot = new Vector2();

        public Vector4 border = new Vector4();

        /// <summary>
        /// 是否为九宫格
        /// </summary>
        public bool isSquared = false;
    }
}
