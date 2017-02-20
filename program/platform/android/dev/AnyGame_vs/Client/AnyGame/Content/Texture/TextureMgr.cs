using AnyGame.Content.Texture;
using DogSE.Library.Log;
using LitJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AnyGame.Content.Manager
{


    public class TextureMgr
    {
        public static readonly string ext_texture = ".png";
        public static readonly string ext_tpsheet = ".txt";

        /// <summary>
        /// 所有的纹理信息
        /// </summary>
        public static Dictionary<string, TextureWrap> dicTextureWrap = new Dictionary<string, TextureWrap>();

        /// <summary>
        /// 所有的 精灵-纹理 映射
        /// </summary>
        private static Dictionary<string, string> dicMap = new Dictionary<string, string>();

        /// <summary>
        /// 上次GC时的帧数
        /// </summary>
        private static int lastGCTime = 0;

        #region 加载资源

        /// <summary>
        /// 初始化（扫描所有纹理资源，加载裁切信息，但不加载纹理）
        /// </summary>
        public static void Init()
        {
            var paths = Directory.GetFiles(GlobalInfo.RES_IMAGE, "*" + ext_tpsheet, SearchOption.AllDirectories);
            for (int i = 0; i < paths.Length; i++)
            {
                TextureWrap tex = new TextureWrap(paths[i]);
                dicTextureWrap[tex.name] = tex;
            }
            Logs.Info("扫描到纹理   " + dicTextureWrap.Count.ToString());
            Logs.Info("扫描到映射   " + dicMap.Count.ToString());
        }

        /// <summary>
        /// 创建一个精灵（此为唯一入口，其他类的CreateSprite方法不要调用）
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static SpriteWrap CreateSprite(string imgPath, Vector4 border)
        {
            if (!dicMap.ContainsKey(imgPath))
            {
                Logs.Error("图片路径错误. {0}", imgPath);
                return null;
            }

            string textureName = dicMap[imgPath];

            if (!dicTextureWrap.ContainsKey(textureName))
            {
                return null;
            }

            SpriteWrap sprite = dicTextureWrap[textureName].CreateSprite(imgPath, border);

            return sprite;
        }

        /// <summary>
        /// 添加一条 【精灵 - 纹理】 映射
        /// </summary>
        public static void AddSpriteMap(string spriteName, string textureName)
        {
            dicMap[spriteName] = textureName;
        }

        #endregion

        #region 释放资源

        public static void Update()
        {
            if (Time.frameCount - lastGCTime < 10)
            {
                return;
            }

            foreach (var tex in dicTextureWrap)
            {
                if (tex.Value.quote == 0 && tex.Value.isLoaded)
                {
                    tex.Value.Dispose();
                    lastGCTime = Time.frameCount;
                }
            }

        }

        #endregion
    }
}
